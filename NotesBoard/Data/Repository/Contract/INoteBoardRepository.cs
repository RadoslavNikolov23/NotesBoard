using NotesBoard.Models;

namespace NotesBoard.Data.Repository.Contract
{
    public interface INoteBoardRepository
    {
        public Task<List<Note>> GetAllNotesAsync();

        public Task CreateNoteAsync(Note note);
        
        public Task DeleteNoteAsync(string id);

        public Task UpdateNoteAsync(Note note);
    }
}
