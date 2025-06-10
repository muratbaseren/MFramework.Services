
using MFramework.Services.Entities.Abstract;
using MongoDB.Bson;

namespace WebApplicationTest.Entities;

public class Album : EntityBase<ObjectId>
{
	public string Name { get; set; }
	public string Description { get; set; }
}