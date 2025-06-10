
using MFramework.Services.Entities.Abstract;
using MongoDB.Bson;

namespace WebApplicationTest.Entities;

public class Migration : EntityBase<ObjectId>
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
}