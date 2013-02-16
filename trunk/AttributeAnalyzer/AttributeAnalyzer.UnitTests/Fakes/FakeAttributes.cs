using System;

namespace AttributeAnalyzer.UnitTests.Fakes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class SimpleAttribute : Attribute
    {
        private readonly string _name;

        public SimpleAttribute(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    internal class OrderAttribute : Attribute
    {
        private readonly int _index;

        public OrderAttribute(int index)
        {
            _index = index;
        }

        public int Index
        {
            get { return _index; }
        }
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    internal class LogEntryAttribute : Attribute
    {
        private readonly string _methodName;

        public LogEntryAttribute(string methodName)
        {
            _methodName = methodName;
        }

        public string MethodName
        {
            get { return _methodName; }
        }
    }
}