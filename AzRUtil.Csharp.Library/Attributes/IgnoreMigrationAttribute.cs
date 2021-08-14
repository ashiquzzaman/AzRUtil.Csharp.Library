using System;

namespace AzRUtil.Csharp.Library.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class IgnoreMigrationAttribute : Attribute
    {
    }
}