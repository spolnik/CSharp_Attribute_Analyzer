using NUnit.Framework;

namespace UnitTests.Framework
{
    public abstract class Specification
    {
        [SetUp]
        public virtual void BaseSetUp()
        {
            Establish_context();
            Initialize_subject_under_test();
            Because();
        }

        [TearDown]
        public virtual void BaseTearDown()
        {
            Dispose_context();
        }

        protected virtual void Establish_context() { }
        protected virtual void Initialize_subject_under_test() { }
        protected virtual void Because() { }
        protected virtual void Dispose_context() { }
    }
}