using Moq;
using NotesBoard.Data.Repository.Contract;
using NotesBoard.Models;
using NotesBoard.Services;
using NotesBoard.Services.Contract;

namespace NotesBoard.Tests
{
    public class NoteServiceTest
    {
        private Mock<INoteBoardRepository> noteRepositoryMock;

        private INoteService noteService;

        [SetUp]
        public void Setup()
        {
            noteRepositoryMock = new Mock<INoteBoardRepository>();
            noteService = new NoteService(noteRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllNotes()
        {
            List<Note> notes = new List<Note>
            {
                new Note { Id = "1", Title = "Note 1", Content = "Content Test" },
                new Note { Id = "2", Title = "Note 2", Content = "Content Test and Demo" }
            };

            noteRepositoryMock.Setup(x => x.GetAllNotesAsync()).ReturnsAsync(notes);

            List<Note> result = await noteService.GetAllAsync();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Count, Is.EqualTo(notes.Count));

            Assert.That(result[0].Id, Is.EqualTo(notes[0].Id));
            Assert.That(result[0].Title, Is.EqualTo(notes[0].Title));
            Assert.That(result[0].Content, Is.EqualTo(notes[0].Content));

            Assert.That(result[1].Id, Is.EqualTo(notes[1].Id));
            Assert.That(result[1].Title, Is.EqualTo(notes[1].Title));
            Assert.That(result[1].Content, Is.EqualTo(notes[1].Content));
        }

        [Test]
        public async Task CreateAsync_ShouldCallCreateNoteAsync()
        {
            Note note = new Note { Id = "1", Title = "Note 1", Content = "Content Test" };
            await noteService.CreateAsync(note);
            noteRepositoryMock.Verify(x => x.CreateNoteAsync(note), Times.Once);

        }

        [Test]
        public async Task DeleteAsync_ShouldCallDeleteNoteAsync()
        {
            string noteId = "1";
            await noteService.DeleteAsync(noteId);
            noteRepositoryMock.Verify(x => x.DeleteNoteAsync(noteId), Times.Once);
        }

        [Test]
        public async Task CreateAndDelete_ShouldWorkTogether()
        {
            Note note = new Note { Id = "1", Title = "Test", Content = "Content" };

            noteRepositoryMock.SetupSequence(x => x.GetAllNotesAsync())
                .ReturnsAsync(new List<Note> { note })
                .ReturnsAsync(new List<Note>());

            await noteService.CreateAsync(note);
            List<Note> afterCreate = await noteService.GetAllAsync();

            await noteService.DeleteAsync(note.Id);
            List<Note> afterDelete = await noteService.GetAllAsync();

            Assert.That(afterCreate.Count, Is.EqualTo(1));
            Assert.That(afterDelete.Count, Is.EqualTo(0));

            noteRepositoryMock.Verify(x => x.CreateNoteAsync(note), Times.Once);
            noteRepositoryMock.Verify(x => x.DeleteNoteAsync(note.Id), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoNotesExist()
        {
            noteRepositoryMock.Setup(x => x.GetAllNotesAsync()).ReturnsAsync(new List<Note>());

            List<Note> result = await noteService.GetAllAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task CreateAsync_ShouldCallRepository_WithNullNote()
        {
            Note note = null!;

            await noteService.CreateAsync(note);

            noteRepositoryMock.Verify(x => x.CreateNoteAsync(note), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_ShouldCallRepository_WithNonExistentId()
        {
            string nonExistentId = "999";

            await noteService.DeleteAsync(nonExistentId);

            noteRepositoryMock.Verify(x => x.DeleteNoteAsync(nonExistentId), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_ShouldCallRepository_WithEmptyId()
        {
            string emptyId = string.Empty;

            await noteService.DeleteAsync(emptyId);

            noteRepositoryMock.Verify(x => x.DeleteNoteAsync(emptyId), Times.Once);
        }
    }
}
