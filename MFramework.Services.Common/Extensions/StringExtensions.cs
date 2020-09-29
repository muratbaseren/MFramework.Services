using MongoDB.Bson;

namespace MFramework.Services.Common.Extensions
{
    public static class StringExtensions
    {
        public static ObjectId ToObjectId(this string s)
        {
            return ObjectId.Parse(s);
        }
    }
}
