using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaCrawl.Models
{
    public class Profile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Discription { get; set; }

        public ProfileStatus ProfileStatus { get; set; }

        public DateTime CreatedDay { get; set; }

        public DateTime UpdatedDay { get; set; }
    }

    public enum ProfileStatus
    {
        NEW = 0,
        CRAWLED = 1,
        INACTIVATED = 2
    }
}
