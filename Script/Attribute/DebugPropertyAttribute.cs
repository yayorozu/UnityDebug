using System;

namespace Yorozu.UniDebug
{
	[AttributeUsage(AttributeTargets.Property)]
	public class DebugPropertyAttribute : DebugAttribute
	{
		public DebugPropertyAttribute(string path = "", string name = "", bool isReadOnly = false) : base(path, name, isReadOnly)
		{
		}
	}
}