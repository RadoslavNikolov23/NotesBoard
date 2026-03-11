using NotesBoard.Data.Repository.Contract;
using NotesBoard.Models;
using NotesBoard.Services.Contract;

namespace NotesBoard.Services
{
    public class NoteService : INoteService
    {

        private readonly INoteBoardRepository notesBoardRepo;
        private readonly ILogger<NoteService> logger;

        public NoteService(INoteBoardRepository notesBoardRepo, ILogger<NoteService> logger)
        {
            this.notesBoardRepo = notesBoardRepo;
            this.logger = logger;
        }

        public async Task<List<Note>> GetAllAsync()
        {
            try
            {
                this.logger.LogInformation("Getting all notes from the repository.");
                return await notesBoardRepo.GetAllNotesAsync();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while getting all notes.");
                throw;
            }
        }

        public async Task CreateAsync(Note note)
        {
            try
            {
                this.logger.LogInformation($"Creating a new note with title: {note.Title}", note.Title);
                await notesBoardRepo.CreateNoteAsync(note);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"An error occurred while creating a new note with title: {note.Title}");
                throw;
            }

        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                this.logger.LogInformation($"Deleting note with id: {id}");
                await notesBoardRepo.DeleteNoteAsync(id);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"An error occurred while deleting note with id: {id}");
                throw;
            }
        }

        public async Task UpdateAsync(Note note)
        {
            try
            {
                this.logger.LogInformation($"Updating note with id: {note.Id}");
                await notesBoardRepo.UpdateNoteAsync(note);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"An error occurred while updating note with id: {note.Id}");
                throw;
            }
        }
    
    }

}
