using System;
using System.Collections.Generic;
using AttributeAnalyzer.UnitTests.Fakes;
using NUnit.Framework;
using UnitTests.Framework;

namespace AttributeAnalyzer.UnitTests
{
    [Category("Attribute analyzer")]
    public class When_we_want_to_find_all_attributes
        : InstanceSpecification<IAmAttributeAnalyser>
    {
        private FakeClassWithManyAttributes _instance;
        private IList<Attribute> _result;

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
            _result = SUT.FindAll().In(_instance);
        }

        [Test]
        public void It_should_return_all_attributes_defined_in_reflected_instance()
        {
            Assert.That(_result, Has.Count.EqualTo(_instance.AllAttrCount));
        }
    }
}