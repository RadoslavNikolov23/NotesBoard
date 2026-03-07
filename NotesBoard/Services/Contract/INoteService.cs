using NotesBoard.Models;

namespace NotesBoard.Services.Contract
{
    public interface INoteService
    {
        public Task<List<Note>> GetAllAsync();

        public Task CreateAsync(Note note);

        public Task DeleteAsync(string id);

        public Task UpdateAsync(Note note);
    }
}
