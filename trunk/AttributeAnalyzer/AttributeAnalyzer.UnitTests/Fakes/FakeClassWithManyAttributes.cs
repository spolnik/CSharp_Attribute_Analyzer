using System;

namespace AttributeAnalyzer.UnitTests.Fakes
{
    [Simple(WithSimpleAttribute.SimpleName)]
    public class FakeClassWithManyAttributes
    {
        internal const int ValuePropIndex = 1;
        internal const int DescriptionPropIndex = 2;
        internal const string StopMethodName = "Stop";
        internal const string StartMethodName = "Start";
        internal const string CancelMethodName = "Cancel";

        [Order(DescriptionPropIndex)]
        public string Description { get; set; }

        [Order(ValuePropIndex)]
        public int Value { get; set; }

        [LogEntry(StartMethodName)]
        public void Start()
        {
            Console.WriteLine("Starting");
        }

        [LogEntry(CancelMethodName)]
        public void Cancel()
        {
            Console.WriteLine("Canceling");
        }

        [LogEntry(StopMethodName)]
        public void Stop()
        {
            Console.WriteLine("Stopping");
        }

        public int AllAttrCount
        {
            get { return 6; }
        }
    }
}