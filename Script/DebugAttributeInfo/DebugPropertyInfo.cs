using System;
using System.Reflection;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UniLib.UniDebug
{
	internal class DebugPropertyInfo : DebugAttrInfoAbstract
	{
		private readonly PropertyInfo _propertyInfo;
		
		public DebugPropertyInfo(int id, Type type, DebugAttribute attr, PropertyInfo propertyInfo) : base(id, type, attr)
		{
			_propertyInfo = propertyInfo;
			Name = string.IsNullOrEmpty(attr.Name) ? propertyInfo.Name : attr.Name;
		}

#if UNITY_EDITOR
		
		public override void EditorDraw()
		{
			foreach (var target in DebugObserver.GetObjects(Type))
			{
				if (target.GetInstanceID() != Id)
					continue;
				
				using (new EditorGUI.DisabledScope(_propertyInfo.SetMethod == null))
				{
					EditorGUI.BeginChangeCheck();
					var obj = DrawField(Name, _propertyInfo.PropertyType, _propertyInfo.GetValue(target));
					if (EditorGUI.EndChangeCheck())
						_propertyInfo.SetValue(target, obj);
				}
			}
		}
		
#endif
		
	}
}