using System;
using System.Collections.Generic;

namespace AttributeAnalyzer
{
    public interface IAmAttributeAnalyser
    {
        IAmAttributeFinder<TAttribute> FindClassAttribute<TAttribute>() where TAttribute : Attribute;
        IAmAttributeFinder<TAttribute> FindPropertyAttributes<TAttribute>() where TAttribute : Attribute;
        IAmAttributeFinder<TAttribute> FindMethodAttributes<TAttribute>() where TAttribute : Attribute;

        IAmAttributeFinder<Attribute> FindAll();

        IAmAttributeDefinitionChecker IsDefined<TAttribute>() where TAttribute : Attribute;
    }

    public interface IAmAttributeFinder<TAttribute> where TAttribute : Attribute
    {
        IList<TAttribute> In<TClassToAnalyse>(TClassToAnalyse instance);
    }

    public interface IAmAttributeDefinitionChecker
    {
        bool In<TClassToAnalyse>(TClassToAnalyse instance);
    }
}
