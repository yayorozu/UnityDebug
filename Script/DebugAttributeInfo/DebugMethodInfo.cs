using System;
using System.Reflection;
using UnityEngine;
using Object = System.Object;

namespace UniLib.UniDebug
{
	internal class DebugMethodInfo : DebugAttrInfoAbstract
	{
		private readonly MethodInfo _methodInfo;
		private readonly ParameterInfo[] _parameterInfos;
		private readonly string[] _args;
		
		public DebugMethodInfo(int id, Type type, DebugAttribute attr, MethodInfo methodInfo) : base(id, type, attr)
		{
			Name = string.IsNullOrEmpty(attr.Name) ? methodInfo.Name : attr.Name;
			
			_methodInfo = methodInfo;
			_args = (attr as DebugMethodAttribute).Args;
			_parameterInfos = methodInfo.GetParameters();
			Parameters = new object[_parameterInfos.Length];
			for (var i = 0; i < _parameterInfos.Length; i++)
			{
				var paramType = _parameterInfos[i].ParameterType;
				Parameters[i] = paramType.IsValueType ? Activator.CreateInstance(paramType) : null;
			}
		}

		public override void Invoke(Object target, params Object[] parameters)
		{
			_methodInfo.Invoke(target, parameters);
		}
		
#if UNITY_EDITOR

		public override void EditorDraw()
		{
			foreach (var target in DebugObserver.GetObjects(Type))
			{
				if (target.GetInstanceID() != Id)
					continue;

				for (var i = 0; i < _parameterInfos.Length; i++)
					Parameters[i] = DrawField(
						_args.Length > i ? _args[i] : _parameterInfos[i].Name,
						_parameterInfos[i].ParameterType, 
						Parameters[i]
					);

				if (GUILayout.Button(_methodInfo.Name))
					DebugObserver.Invoke(Path, Name, Parameters);
			}
		}

#endif
	}
}