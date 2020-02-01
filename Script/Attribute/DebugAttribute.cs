using System;

namespace UniLib.UniDebug
{
	[AttributeUsage(AttributeTargets.All)]
	public class DebugAttribute : Attribute
	{
		public readonly string Path;
		public readonly string Name;

		public DebugAttribute(string path = "", string name = "")
		{
			Path = path;
			Name = name;
		}
	}
}