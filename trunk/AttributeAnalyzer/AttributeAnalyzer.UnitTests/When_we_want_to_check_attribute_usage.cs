using System;
using AttributeAnalyzer.UnitTests.Fakes;
using NUnit.Framework;
using UnitTests.Framework;

namespace AttributeAnalyzer.UnitTests
{
    [Category("Attribute analyzer")]
    public class When_we_want_to_check_attribute_usage
        : InstanceSpecification<IAmAttributeAnalyser>
    {
        private FakeClassWithManyAttributes _instance;
        private bool _result;

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
            _result = SUT.IsDefined<OrderAttribute>().In(_instance);
        }

        [Test]
        public void It_should_return_true_if_class_is_marked_by_attribute()
        {
            Assert.That(_result, Is.True);
        }

        [Test]
        public void Then_after_checking_non_existing_attribute_it_should_return_false()
        {
            var result = SUT.IsDefined<SerializableAttribute>().In(_instance);
            Assert.That(result, Is.False);
        }
    }
}