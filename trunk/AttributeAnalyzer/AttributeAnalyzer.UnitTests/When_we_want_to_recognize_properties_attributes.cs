using System.Collections.Generic;
using AttributeAnalyzer.UnitTests.Fakes;
using NUnit.Framework;
using UnitTests.Framework;
using System.Linq;

namespace AttributeAnalyzer.UnitTests
{
    [Category("Attribute analyzer")]
    public class When_we_want_to_recognize_properties_attributes
        : InstanceSpecification<IAmAttributeAnalyser>
    {
        private FakeClassWithManyAttributes _instance;
        private IList<OrderAttribute> _result;

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
            _result = SUT.FindPropertyAttributes<OrderAttribute>().In(_instance);
        }

        [Test]
        public void It_should_have_only_requested_attributes()
        {
            Assert.That(_result, Has.Count.EqualTo(2));
        }

        [Test]
        public void It_should_have_all_inserted_indexes()
        {
            var indexes = _result.Select(attr => attr.Index).ToList();

            Assert.That(indexes, Contains.Item(FakeClassWithManyAttributes.DescriptionPropIndex));
            Assert.That(indexes, Contains.Item(FakeClassWithManyAttributes.ValuePropIndex));
        }
    }
}