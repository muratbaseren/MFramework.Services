using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MFramework.Services.Entities.Abstract
{
    public abstract class EntityBase<TKey> : IEntity
    {
        [Key, BsonId]
        public TKey Id { get; set; }

        public EntityBase()
        {
            if (typeof(TKey) == typeof(ObjectId))
                this.GetType().GetProperty(nameof(Id)).SetValue(this, ObjectId.GenerateNewId());
        }
    }
}
