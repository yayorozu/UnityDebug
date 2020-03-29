using UnityEditor;
using UnityEngine;

namespace Yorozu.UniDebug
{
	public class UniDebugWindow : EditorWindow
	{
		[MenuItem("Tools/DebugWindow")]
		private static void ShowWindow()
		{
			var window = GetWindow<UniDebugWindow>();
			window.titleContent = new GUIContent("DebugWindow");
			window.Show();
		}

		private void OnGUI()
		{
			if (!EditorApplication.isPlaying)
			{
				EditorGUILayout.HelpBox("Valid Editor Playing.", MessageType.Info);
				return;
			}

			DebugObserver.EditorDraw();
		}
	}
}