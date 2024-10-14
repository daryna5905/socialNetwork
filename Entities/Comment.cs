using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopMongo.Entities
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("body")]
        public string CommentBody { get; set; }

        [BsonElement("likesComment")]
        public int Likes { get; set; }

        [BsonElement("likesUsers")]
        public List<string> LikesUsers { get; set; }

    }
}
