using System.Collections.Generic;
using System.Linq;
using AttributeAnalyzer.UnitTests.Fakes;
using NUnit.Framework;
using UnitTests.Framework;

namespace AttributeAnalyzer.UnitTests
{
    [Category("Attribute analyzer")]
    public class When_we_want_to_recognize_methods_attributes
        : InstanceSpecification<IAmAttributeAnalyser>
    {
        private FakeClassWithManyAttributes _instance;
        private IList<LogEntryAttribute> _result;

        protected override void Establish_context()
        {
            _instance = new FakeClassWithManyAttributes();
        }

        protected override IAmAttributeAnalyser Create_subject_under_test()
        {
            return new AttributeAnalyzer();
        }

        protected override void Because()
        {
            _result = SUT.FindMethodAttributes<LogEntryAttribute>().In(_instance);
        }

        [Test]
        public void It_should_have_only_requested_attributes()
        {
            Assert.That(_result, Has.Count.EqualTo(3));
        }

        [Test]
        public void It_should_return_all_requested_method_names()
        {
            var methodNames = _result.Select(attr => attr.MethodName).ToList();

            Assert.That(methodNames, Contains.Item(FakeClassWithManyAttributes.StartMethodName));
            Assert.That(methodNames, Contains.Item(FakeClassWithManyAttributes.StopMethodName));
            Assert.That(methodNames, Contains.Item(FakeClassWithManyAttributes.CancelMethodName));
        }
    }
}