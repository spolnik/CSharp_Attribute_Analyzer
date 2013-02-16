using System.Collections.Generic;
using AttributeAnalyzer.UnitTests.Fakes;
using NUnit.Framework;
using UnitTests.Framework;
using System.Linq;

namespace AttributeAnalyzer.UnitTests
{
    [Category("Attribute analyzer")]
    public class When_we_want_to_recoginze_simple_attribute_by_attribute_analyzer
        : InstanceSpecification<IAmAttributeAnalyser>
    {
        private WithSimpleAttribute _instanceWithSimpleAttr;
        private IList<SimpleAttribute> _result;
        
        protected override void Establish_context()
        {
            _instanceWithSimpleAttr = new WithSimpleAttribute();
        }

        protected override IAmAttributeAnalyser Create_subject_under_test()
        {
            return new AttributeAnalyzer();
        }

        protected override void Because()
        {
            _result = SUT.FindClassAttribute<SimpleAttribute>().In(_instanceWithSimpleAttr);
        }

        [Test]
        public void It_should_have_non_empty_result()
        {
            Assert.That(_result.FirstOrDefault(), Is.Not.Null);
        }

        [Test]
        public void It_should_contain_the_same_name_inserted_during_creation()
        {
            Assert.That(_result.First().Name, Is.EqualTo(WithSimpleAttribute.SimpleName));
        }
    }
}
