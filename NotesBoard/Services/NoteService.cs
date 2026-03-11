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
                this.logger.LogInformation("Getting all notes from the repository");
                var notes = await notesBoardRepo.GetAllNotesAsync();
                this.logger.LogInformation("Successfully retrieved {NoteCount} notes", notes.Count);
                return notes;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to retrieve notes from repository");
                throw;
            }
        }

        public async Task CreateAsync(Note note)
        {
            try
            {
                this.logger.LogInformation("Creating a new note with title: {NoteTitle}", note.Title);
                await notesBoardRepo.CreateNoteAsync(note);
                this.logger.LogInformation("Successfully created note with title: {NoteTitle}", note.Title);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to create note with title: {NoteTitle}", note.Title);
                throw;
            }
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                this.logger.LogInformation("Deleting note with id: {NoteId}", id);
                await notesBoardRepo.DeleteNoteAsync(id);
                this.logger.LogInformation("Successfully deleted note with id: {NoteId}", id);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to delete note with id: {NoteId}", id);
                throw;
            }
        }

        public async Task UpdateAsync(Note note)
        {
            try
            {
                this.logger.LogInformation("Updating note with id: {NoteId}", note.Id);
                await notesBoardRepo.UpdateNoteAsync(note);
                this.logger.LogInformation("Successfully updated note with id: {NoteId}", note.Id);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to update note with id: {NoteId}", note.Id);
                throw;
            }
        }
    
    }

}
