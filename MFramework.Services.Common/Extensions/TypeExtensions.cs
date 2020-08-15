using System;
using System.Linq;
using System.Reflection;

namespace MFramework.Services.Common.Extensions
{
    public static class TypeExtensions
    {
        public static PropertyInfo GetProperty<T>(this Type type)
        {
            return type.GetProperties().Where(x => x.PropertyType == typeof(T)).FirstOrDefault();
        }

        public static PropertyInfo[] GetProperties<T>(this Type type)
        {
            return type.GetProperties().Where(x => x.PropertyType == typeof(T)).ToArray();
        }
    }
}
