using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace MFramework.Services.Entities.Abstract
{
    public interface IEntity
    {

    }

    public class EntityBase<TKey> : IEntity
    {
        [Key, BsonId]
        public TKey Id { get; set; }

        public EntityBase()
        {
            if (typeof(TKey) == typeof(Guid))
                this.GetType().GetProperty(nameof(Id)).SetValue(this, Guid.NewGuid());

            if (typeof(TKey) == typeof(ObjectId))
                this.GetType().GetProperty(nameof(Id)).SetValue(this, ObjectId.GenerateNewId());
        }
    }

    public class Testo
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
        public string MyProperty { get; set; }
        public string MyProperty1 { get; set; }
        public string MyProperty2 { get; set; }
    }
}
