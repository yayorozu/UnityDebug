using System;
using Object = System.Object;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace UniLib.UniDebug
{
	internal abstract class DebugAttrInfoAbstract
	{
		public readonly Type Type;
		public readonly string Path;
		public readonly bool IsReadOnly;
		public string Name;
		protected object[] Parameters = new object[0];
		protected int Id;

		public DebugAttrInfoAbstract(int id, Type type, DebugAttribute attr)
		{
			Id = id;
			Type = type;
			Path = attr.Path;
			IsReadOnly = attr.IsReadonly;
		}

		public virtual void Invoke(object target, params object[] parameters)
		{
		}

#if UNITY_EDITOR
		public abstract void EditorDraw();

		protected object DrawField(string label, Type type, object value)
		{
			if (type == typeof(int))
				return EditorGUILayout.IntField(label, (int) value);
			if (type == typeof(float))
				return EditorGUILayout.FloatField(label, (float) value);
			if (type == typeof(string))
				return EditorGUILayout.TextField(label, (string) value);
			if (type == typeof(bool))
				return EditorGUILayout.Toggle(label, (bool) value);
			if (type == typeof(Vector2))
				return EditorGUILayout.Vector2Field(label, (Vector2) value);
			if (type == typeof(Vector3))
				return EditorGUILayout.Vector3Field(label, (Vector3) value);
			if (type == typeof(Color))
				return EditorGUILayout.ColorField(label, (Color) value);

			if (type.IsEnum)
			{
				return EditorGUILayout.IntPopup(
					label, 
					(int) value, 
					Enum.GetNames(type), 
					Enum.GetValues(type) as int[]
				);
			}
			
			EditorGUILayout.LabelField(type + "is invalid");

			return value;
		}

#endif
	}
}