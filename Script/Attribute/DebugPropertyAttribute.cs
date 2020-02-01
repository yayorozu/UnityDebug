using System;

namespace UniLib.UniDebug
{
	[AttributeUsage(AttributeTargets.Property)]
	public class DebugPropertyAttribute : DebugAttribute
	{
		public DebugPropertyAttribute(string path = "", string name = "") : base(path, name)
		{
		}
	}
}