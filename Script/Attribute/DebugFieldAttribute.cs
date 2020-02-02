using System;

namespace UniLib.UniDebug
{
	[AttributeUsage(AttributeTargets.Field)]
	public class DebugFieldAttribute : DebugAttribute
	{
		public DebugFieldAttribute(string path = "", string name = "", bool isReadOnly = false) : base(path, name, isReadOnly)
		{
		}
	}
}