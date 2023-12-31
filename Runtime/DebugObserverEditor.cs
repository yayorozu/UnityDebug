#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Yorozu.UniDebug
{
	public static partial class DebugObserver
	{
		private static readonly Dictionary<string, bool> Foldout = new Dictionary<string, bool>();

		public static void EditorDraw()
		{
			foreach (var pair in _dic)
			{
				if (!Foldout.ContainsKey(pair.Key))
					Foldout.Add(pair.Key, false);

				Foldout[pair.Key] = Styles.Foldout(pair.Key, Foldout[pair.Key]);

				if (!Foldout[pair.Key])
					continue;

				foreach (var info in pair.Value)
					info.EditorDraw();
			}
		}

		private static class Styles
		{
			private static readonly GUIStyle FoldoutStyle;

			static Styles()
			{
				FoldoutStyle = new GUIStyle("ShurikenModuleTitle")
				{
					border = new RectOffset(15, 7, 4, 4),
					fixedHeight = 22,
					contentOffset = new Vector2(20f, -2f)
				};
			}

			public static bool Foldout(string title, bool fold)
			{
				var rect = GUILayoutUtility.GetRect(16f, 22f, FoldoutStyle);
				GUI.Box(rect, title, FoldoutStyle);

				var e = Event.current;

				var toggleRect = new Rect(rect.x + 4f, rect.y + 2f, 13f, 13f);
				if (e.type == EventType.Repaint)
					EditorStyles.foldout.Draw(toggleRect, false, false, fold, false);

				if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
				{
					fold = !fold;
					e.Use();
				}

				return fold;
			}
		}
	}
}

#endif
