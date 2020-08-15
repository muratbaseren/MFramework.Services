using System;

namespace MFramework.Services.DataAccess.Mongo.Attributes
{
    //
    // Summary:
    //     Specifies the database collection that a class is mapped to.
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CollectionAttribute : Attribute
    {
        public string Name { get; protected set; }

        public CollectionAttribute(string name)
        {
            Name = name;
        }
    }
}
