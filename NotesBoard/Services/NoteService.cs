using NotesBoard.Data.Repository.Contract;
using NotesBoard.Models;
using NotesBoard.Services.Contract;

namespace NotesBoard.Services
{
    public class NoteService : INoteService
    {

        private readonly INoteBoardRepository notesBoardRepo;

        public NoteService(INoteBoardRepository notesBoardRepo)
        {
            this.notesBoardRepo = notesBoardRepo;
        }

        public async Task<List<Note>> GetAllAsync()
        {
            return await notesBoardRepo.GetAllNotesAsync();
        }

        public async Task CreateAsync(Note note)
        {
            await notesBoardRepo.CreateNoteAsync(note);

        }

        public async Task DeleteAsync(string id)
        {
            await notesBoardRepo.DeleteNoteAsync(id);
        }

        public async Task UpdateAsync(Note note)
        {
            await notesBoardRepo.UpdateNoteAsync(note);
        }
    
    }

}
