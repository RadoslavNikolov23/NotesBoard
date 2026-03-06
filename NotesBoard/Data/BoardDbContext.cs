using MongoDB.Driver;
using NotesBoard.Models;

namespace NotesBoard.Data
{
    public class BoardDbContext
    {
        private readonly IMongoDatabase boardDatabase;

        public BoardDbContext()
        {
            MongoClient client = new MongoClient("mongodb://admin:customs6151@localhost:27017/");

            boardDatabase = client.GetDatabase("NotesDB");
        }

        public IMongoCollection<Note> Notes => boardDatabase.GetCollection<Note>("Notes");
    }
}
