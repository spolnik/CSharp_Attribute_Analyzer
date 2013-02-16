using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AttributeAnalyzer
{
    public sealed class AttributeAnalyzer : IAmAttributeAnalyser
    {
        #region IAmAttributeFinder Members

        public IAmAttributeFinder<TAttribute> FindClassAttribute<TAttribute>() where TAttribute : Attribute
        {
            return new ClassAttributeFinder<TAttribute>();
        }

        public IAmAttributeFinder<TAttribute> FindPropertyAttributes<TAttribute>() where TAttribute : Attribute
        {
            return new PropertiesAttributesFinder<TAttribute>();
        }

        public IAmAttributeFinder<TAttribute> FindMethodAttributes<TAttribute>() where TAttribute : Attribute
        {
            return new MethodAttributesFinder<TAttribute>();
        }

        public IAmAttributeFinder<Attribute> FindAll()
        {
            return new AllAttributesFinder<Attribute>();
        }

        public IAmAttributeDefinitionChecker IsDefined<TAttribute>() where TAttribute : Attribute
        {
            return new FindAttributeDefinition<TAttribute>();
        }

        #endregion

        #region Nested type: AllAttributesFinder

        private sealed class AllAttributesFinder<TAttribute> : IAmAttributeFinder<TAttribute> where TAttribute : Attribute
        {
            #region IAmAttributeFinder<TAttribute> Members

            public IList<TAttribute> In<TClassToAnalyse>(TClassToAnalyse instance)
            {
                var attributeAnalyzer = new AttributeAnalyzer();
                var attributes = new List<TAttribute>();

                var classAttributes = attributeAnalyzer.FindClassAttribute<TAttribute>().In(instance);
                attributes.AddRange(classAttributes);

                var methodAttributes = attributeAnalyzer.FindMethodAttributes<TAttribute>().In(instance);
                attributes.AddRange(methodAttributes);

                var propertyAttributes = attributeAnalyzer.FindPropertyAttributes<TAttribute>().In(instance);
                attributes.AddRange(propertyAttributes);

                return attributes;
            }

            #endregion
        }

        #endregion

        #region Nested type: ClassAttributeFinder

        private sealed class ClassAttributeFinder<TAttribute> : IAmAttributeFinder<TAttribute> where TAttribute : Attribute
        {
            #region IAmAttributeFinder<TAttribute> Members

            public IList<TAttribute> In<TClassToAnalyse>(TClassToAnalyse instance)
            {
                return instance.GetType().GetAttributes<TAttribute>().ToList();
            }

            #endregion
        }

        #endregion

        #region Nested type: FindAttributeDefinition

        private sealed class FindAttributeDefinition<TAttribute> : IAmAttributeDefinitionChecker where TAttribute : Attribute
        {
            #region IAmAttributeDefinitionChecker Members

            public bool In<TClassToAnalyse>(TClassToAnalyse instance)
            {
                var allAttributesFinder = new AllAttributesFinder<TAttribute>();
                var attributes = allAttributesFinder.In(instance);

                return attributes.Count > 0;
            }

            #endregion
        }

        #endregion

        #region Nested type: MethodAttributesFinder

        private sealed class MethodAttributesFinder<TAttribute> : IAmAttributeFinder<TAttribute> where TAttribute : Attribute
        {
            #region IAmAttributeFinder<TAttribute> Members

            public IList<TAttribute> In<TClassToAnalyse>(TClassToAnalyse instance)
            {
                var instanceType = instance.GetType();

                var properties = instanceType.GetMethods();

                return properties.SelectMany(MemberInfoExtensions.GetAttributes<TAttribute>).ToList();
            }

            #endregion
        }

        #endregion

        #region Nested type: PropertiesAttributesFinder

        private sealed class PropertiesAttributesFinder<TAttribute> : IAmAttributeFinder<TAttribute> where TAttribute : Attribute
        {
            #region IAmAttributeFinder<TAttribute> Members

            public IList<TAttribute> In<TClassToAnalyse>(TClassToAnalyse instance)
            {
                var instanceType = instance.GetType();

                var properties = instanceType.GetProperties();

                return properties.SelectMany(MemberInfoExtensions.GetAttributes<TAttribute>).ToList();
            }

            #endregion
        }

        #endregion
    }

    internal static class MemberInfoExtensions
    {
        internal static IEnumerable<TAttribute> GetAttributes<TAttribute>(this MemberInfo that) where TAttribute : Attribute
        {
            return
                that.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>().Where(
                    attr => !(attr.GetType().FullName.StartsWith("System.")));
        }
    }
}
