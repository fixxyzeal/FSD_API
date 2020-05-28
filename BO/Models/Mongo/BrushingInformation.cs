using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BO.Models.Mongo
{
    public class BrushingInformation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public int BrushingSet { get; set; }

        public int BrushingRemain { get; set; }
        public DateTime BrushingDate { get; set; }
    }
}