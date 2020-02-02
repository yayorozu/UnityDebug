using System;

namespace UniLib.UniDebug
{
	[AttributeUsage(AttributeTargets.All)]
	public class DebugAttribute : Attribute
	{
		public readonly string Path;
		public readonly string Name;
		public readonly bool IsReadonly;

		public DebugAttribute(string path = "", string name = "", bool isReadonly = false)
		{
			Path = path;
			Name = name;
			IsReadonly = isReadonly;
		}
	}
}