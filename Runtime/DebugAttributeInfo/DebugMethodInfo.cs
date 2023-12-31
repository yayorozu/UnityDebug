using System;
using System.Reflection;
using UnityEngine;

namespace Yorozu.UniDebug
{
	internal class DebugMethodInfo : DebugAttrInfoAbstract
	{
		private readonly MethodInfo _methodInfo;
		private readonly ParameterInfo[] _parameterInfos;
		private readonly string[] _args;
		private readonly object[] _defineParameters;

		public DebugMethodInfo(int id, Type type, DebugAttribute attr, MethodInfo methodInfo) : base(id, type, attr)
		{
			Name = string.IsNullOrEmpty(attr.Name) ? methodInfo.Name : attr.Name;

			_methodInfo = methodInfo;
			var methodAttr = attr as DebugMethodAttribute;
			_args = methodAttr.Args;
			_defineParameters = methodAttr.Parameters;

			_parameterInfos = methodInfo.GetParameters();
			Parameters = new object[_parameterInfos.Length];
			for (var i = 0; i < _parameterInfos.Length; i++)
			{
				var paramType = _parameterInfos[i].ParameterType;
				Parameters[i] = paramType.IsValueType ? Activator.CreateInstance(paramType) : null;
			}
		}

		public override void Invoke(object target, params object[] parameters)
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

				if (_parameterInfos.Length > 0 && _defineParameters.Length == _parameterInfos.Length)
				{
					if (GUILayout.Button(Name))
						DebugObserver.Invoke(Path, Name, _defineParameters);

					continue;
				}

				for (var i = 0; i < _parameterInfos.Length; i++)
					Parameters[i] = DrawField(
						_args.Length > i ? _args[i] : _parameterInfos[i].Name,
						_parameterInfos[i].ParameterType,
						Parameters[i]
					);

				if (GUILayout.Button(Name))
					DebugObserver.Invoke(Path, Name, Parameters);
			}
		}

#endif
	}
}