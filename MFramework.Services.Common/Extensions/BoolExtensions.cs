namespace MFramework.Services.Common.Extensions
{
    public static class BoolExtensions
    {
        public static string Text(this bool value, string trueString, string falseString)
        {
            return value ? trueString : falseString;
        }
    }
}
