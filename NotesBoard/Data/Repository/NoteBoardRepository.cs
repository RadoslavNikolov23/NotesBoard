using MongoDB.Driver;
using NotesBoard.Data.Repository.Contract;
using NotesBoard.Models;

namespace NotesBoard.Data.Repository
{
    public class NoteBoardRepository : INoteBoardRepository
    {
        private readonly IMongoCollection<Note> notes;

        public NoteBoardRepository(BoardDbContext dbContext)
        {
            notes = dbContext.Notes;
        }


        public async Task<List<Note>> GetAllNotesAsync()
        {
            return await notes.Find(note => true).ToListAsync();
        }

        public async Task CreateNoteAsync(Note note)
        {
            await notes.InsertOneAsync(note);
        }

        public async Task DeleteNoteAsync(string id)
        {
            await notes.DeleteOneAsync(n => n.Id == id);
        }

        public async Task UpdateNoteAsync(Note note)
        {
            await notes.ReplaceOneAsync(x => x.Id == note.Id, note);
        }
    }
}
