using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MFramework.Services.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDisplayName(this Enum value)
        {
            return value.GetType().GetField(value.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name;
        }
        public static int GetEnumValue(this Enum value)
        {
            return (int)value.GetType().GetField(value.ToString()).GetValue(value);
        }
    }
}
