using System;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace UniLib.UniDebug
{
	internal class DebugFieldInfo : DebugAttrInfoAbstract
	{
		private readonly FieldInfo _fieldInfo;

		public DebugFieldInfo(int id, Type type, DebugAttribute attr, FieldInfo fieldInfo) : base(id, type, attr)
		{
			_fieldInfo = fieldInfo;
			Name = string.IsNullOrEmpty(attr.Name) ? fieldInfo.Name : attr.Name;
		}

#if UNITY_EDITOR

		public override void EditorDraw()
		{
			foreach (var target in DebugObserver.GetObjects(Type))
			{
				if (target.GetInstanceID() != Id)
					continue;

				if (IsReadOnly)
				{
					using (new EditorGUI.DisabledScope(IsReadOnly))
					{
						DrawField(Name, _fieldInfo.FieldType, _fieldInfo.GetValue(target));
					}

					continue;
				}

				EditorGUI.BeginChangeCheck();
				var obj = DrawField(Name, _fieldInfo.FieldType, _fieldInfo.GetValue(target));
				if (EditorGUI.EndChangeCheck())
					_fieldInfo.SetValue(target, obj);
			}
		}

#endif
	}
}