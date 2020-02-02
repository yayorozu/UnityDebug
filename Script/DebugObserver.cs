using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UniLib.UniDebug
{
	public static partial class DebugObserver
	{
		internal static BindingFlags Flags = BindingFlags.Public | 
		                                    BindingFlags.NonPublic | 
		                                    BindingFlags.Static |
		                                    BindingFlags.Instance;

		private static readonly Dictionary<string, List<DebugAttrInfoAbstract>> _dic = new Dictionary<string, List<DebugAttrInfoAbstract>>();
		
		private static readonly List<Object> _registerObjects = new List<Object>();
		
		/// <summary>
		/// 登録
		/// </summary>
		public static void Register(Object obj)
		{
			var id = obj.GetInstanceID();
			var type = obj.GetType();
			var count = 0;
			foreach (var method in type.GetMethods(Flags).Where(m => m.GetCustomAttributes<DebugMethodAttribute>().Any()))
			{
				foreach (var attr in method.GetCustomAttributes<DebugMethodAttribute>())
				{
					count++;
					AddDic(attr.Path, new DebugMethodInfo(id, type, attr, method));
				}
			}
			foreach (var field in type.GetFields(Flags).Where(m => m.GetCustomAttributes<DebugFieldAttribute>().Any()))
			{
				foreach (var attr in field.GetCustomAttributes<DebugFieldAttribute>())
				{
					count++;
					AddDic(attr.Path, new DebugFieldInfo(id, type, attr, field));
				}
			}
			foreach (var property in type.GetProperties(Flags).Where(m => m.GetCustomAttributes<DebugPropertyAttribute>().Any()))
			{
				foreach (var attr in property.GetCustomAttributes<DebugPropertyAttribute>())
				{
					count++;
					AddDic(attr.Path, new DebugPropertyInfo(id, type, attr, property));
				}
			}
			
			if (count > 0)
				_registerObjects.Add(obj);
		}


		/// <summary>
		/// 登録解除
		/// </summary>
		public static void Release(Object obj)
		{
			if (!_registerObjects.Contains(obj))
				return;

			var type = obj.GetType();
			_registerObjects.RemoveAll(o => o.GetType() == type);
			foreach (var pair in _dic)
				pair.Value.RemoveAll(da => da.Type == type);

			// 空のものを削除
			var emptyKeys = _dic.Keys.Where(k => _dic[k].Count == 0).ToArray();
			for (var i = 0; i < emptyKeys.Length; i++)
				_dic.Remove(emptyKeys[i]);
		}

		/// <summary>
		/// 追加
		/// </summary>
		private static void AddDic(string path, DebugAttrInfoAbstract attrInfo)
		{
			// 追加した場合ソートする
			if (!_dic.ContainsKey(path))
			{
				_dic.Add(path, new List<DebugAttrInfoAbstract>());
				_dic.Keys.OrderBy(k => k);
			}
			
			_dic[path].Add(attrInfo);
		}
		
		/// <summary>
		/// メソッド実行
		/// </summary>
		public static void Invoke(string path, string name, params System.Object[] parameters)
		{
			if (!_dic.ContainsKey(path))
				return;

			foreach (var attrInfo in _dic[path].FindAll(da => da.Name == name))
				foreach (var target in _registerObjects.FindAll(o => o.GetType() == attrInfo.Type))
					attrInfo.Invoke(target, parameters);
		}

		internal static IEnumerable<Object> GetObjects(Type type)
		{
			return _registerObjects.FindAll(o => o.GetType() == type);
		}
	}
}
