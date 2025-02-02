# UnityDebug

<img src="https://github.com/yawaLib/ImageUploader/blob/master/UniDebug/Top.png" width="400">

UnityEditor で利用できるデバッグメニューツール  
※UIElement が runtime UI に対応したら実機にも表示できるように拡張を行う予定です

* Field
* Property
* Method  
に Attribute を追加し登録処理を呼び出すことで EditorWindow に表示されるようになります

パスを指定することができるので機能ごとにまとめて表示させることができます

## 登録解除

インスタンス生成時に Register を呼び出し 削除時に Release を呼び出すことで  
EditorWindow に表示・非表示することができます

```cs
using UniLib.UniDebug;
using UnityEngine;

public class SampleDebugBehaviour : MonoBehaviour
{
	private void Awake()
	{
  	// 登録
		DebugObserver.Register(this);
	}

	private void OnDestroy()
	{
  	// 登録解除
		DebugObserver.Release(this);
	}
}
```

## Field
DebugField の Attribute を追加することで利用できます

`public DebugFieldAttribute(string path = "", string name = "")`

となっており、パスと表示名が指定できます

```cs
[DebugField()]
private int _debugIntValue;
		
[DebugField("Field")]
private string _debugStringValue;
		
[DebugField("Field", "bool")]
private bool _debugBoolValue;
		
[DebugField("Field", "enum")]
private TextAnchor _textAnchor;
		
[DebugField("Field/Struct", "color")]
private Color _color;
		
[DebugField("Field/Struct", "Vector2")]
private Vector2 _debugVector2Value;
		
[DebugField("Field/Struct", "Vector3")]
private Vector3 _debugVector3Value;
```

## Property
DebugProperty の Attribute を追加することで利用できます

`public DebugPropertyAttribute(string path = "", string name = "")`

Field と同じくパスと表示名を指定できます

```cs
private float _debugFloatValue;
private bool _debugBoolValue;

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
```

## Method
DebugMethod の Attribute を追加することで利用できます

`public DebugMethodAttribute(string path = "", string name = "", params string[] args)`

Field や Property に追加して引数の表示名も変更できます

```cs
[DebugMethod()]
private void DebugMethod1()
{
	Debug.LogError("Click");
}

[DebugMethod("Method", "Test")]
private void DebugMethod2()
{
	Debug.LogError("Click2");
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
```
