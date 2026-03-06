using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotesBoard.Models
{
    public class Note
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string? Title { get; set; } 

        public string? Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        
    }
}
