using Yorozu.UniDebug;
using UnityEngine;

namespace Sample
{
	public class SampleDebugBehaviour : MonoBehaviour
	{
		private void Awake()
		{
			DebugObserver.Register(this);
		}

		private void OnDestroy()
		{
			DebugObserver.Release(this);
		}

		[DebugField("Field", "Float")]
		private float _debugFloatValue;
		
		[DebugField("Field", "int")]
		private int _debugIntValue;
		
		[DebugField("Field", "string")]
		private string _debugStringValue;
		
		[DebugField("Field", "bool")]
		private bool _debugBoolValue;
		
		[DebugField("Field", "enum")]
		private TextAnchor _textAnchor;
		
		[DebugField("Field", "color")]
		private Color _color;
		
		[DebugField("Field/Struct", "Vector2")]
		private Vector2 _debugVector2Value;
		
		[DebugField("Field/Struct", "Vector3")]
		private Vector3 _debugVector3Value;

		[DebugProperty("Property")]
		private float _debugFloatProperty
		{
			get { return _debugFloatValue; }
			set { _debugFloatValue = value; }
		}
		
		[DebugProperty("Property/Disable")]
		private bool _debugBoolProperty
		{
			get { return _debugBoolValue; }
		}
		
		[DebugMethod("Method", "Test")]
		private void DebugMethod()
		{
			Debug.LogError("Click");
		}
		
		[DebugMethod("Method/Args", "IntValue", "int")]
		private void DebugMethodArgsInt(int intValue)
		{
			Debug.Log(intValue);
		}
		
		[DebugMethod("Method/Args", "IntValue2", "int1", "int2")]
		private void DebugMethodArgsInt(int intValue1, int intValue2)
		{
			Debug.Log(intValue1);
			Debug.Log(intValue2);
		}

		[DebugMethod("TimeScale", "TimeScale1", 1f)]
		[DebugMethod("TimeScale", "TimeScale2", 2f)]
		private void TimeScale(float scale)
		{
			Time.timeScale = scale;
			Debug.Log(scale);
		}
	}
}

