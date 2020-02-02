using System;

namespace UniLib.UniDebug
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class DebugMethodAttribute : DebugAttribute
	{
		public readonly string[] Args = new string[0];
		public readonly object[] Parameters = new object[0];

		public DebugMethodAttribute(string path = "", string name = "") : base(path, name)
		{
		}

		public DebugMethodAttribute(string path = "", string name = "", params string[] args) : base(path, name)
		{
			Args = args;
		}

		public DebugMethodAttribute(string path = "", string name = "", params object[] parameters) : base(path, name)
		{
			Parameters = parameters;
		}
	}
}