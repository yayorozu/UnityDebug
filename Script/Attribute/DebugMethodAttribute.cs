using System;

namespace UniLib.UniDebug
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DebugMethodAttribute : DebugAttribute
    {
        public string[] Args;
        
        public DebugMethodAttribute(string path = "", string name = "", params string[] args) : base(path, name)
        {
            Args = args;
        }
    }
}
