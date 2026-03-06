using MongoDB.Driver;
using NotesBoard.Data;
using NotesBoard.Models;

namespace NotesBoard.Services
{
    public class NoteService
    {

        private readonly IMongoCollection<Note> notes;

        public NoteService(BoardDbContext boardContex)
        {
            notes = boardContex.Notes;
        }

        public async Task<List<Note>> GetAllAsync()
        {
            return await notes.Find(note => true).ToListAsync();
        }

        public async Task CreateAsync(Note note)
        {
            await notes.InsertOneAsync(note);

        }

        public async Task DeleteAsync(string id)
        {
            await notes.DeleteOneAsync(n => n.Id == id);
        }

        public async Task UpdateAsync(Note note)
        {
            await notes.ReplaceOneAsync(x => x.Id == note.Id, note);
        }
    
    }

}
