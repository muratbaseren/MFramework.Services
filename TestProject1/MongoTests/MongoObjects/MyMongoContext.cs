﻿using MFramework.Services.DataAccess.Mongo.Context;

namespace TestProject1.MongoTests.MongoObjects
{
    public class MyMongoContext : MongoDBContextBase
    {
        public MyMongoContext() : base(null, "mongodb://localhost:27017", "mframeworktestdb")
        {

        }
    }
}