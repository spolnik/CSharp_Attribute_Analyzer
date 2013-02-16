namespace UnitTests.Framework
{
    public abstract class InstanceSpecification<TSubjectUnderTest>
        : Specification
    {
        protected override void Initialize_subject_under_test()
        {
            SUT = Create_subject_under_test();
        }

        protected abstract TSubjectUnderTest Create_subject_under_test();

        protected TSubjectUnderTest SUT { get; private set; }
    }
}