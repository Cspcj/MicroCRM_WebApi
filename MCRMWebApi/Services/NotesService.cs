using MCRMWebApi.DTOs;
using MCRMWebApi.Repostories;

namespace MCRMWebApi.Services
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _repositoty;
        private readonly ILogger<NotesRepository> _logger;

        public NotesService(INotesRepository repositoty, ILogger<NotesRepository> logger)
        {
            _logger = logger;
            _repositoty = repositoty;
        }

        public async Task<NoteDTO> AddNoteAsync(NotePartialCreateDTO note)
        {
            return await _repositoty.AddNote(note);
        }

        public async Task<NoteDTO> DeleteNoteAsync(Guid id)
        {
            return await _repositoty.DeleteNote(id);
        }

        public async Task<IEnumerable<NoteDTO>> GetAllNotes()
        {
             return await _repositoty.GetAllNotes();
        }

        public async Task<NoteDTO> GetNoteByID(Guid id)
        {
            return await _repositoty.GetNoteByID(id);
        }

        public async Task<NoteDTO> UpdatePartial(Guid id, NotePartialUpdateDTO note)
        {
            return await _repositoty.UpdatePartial(id, note);   
        }
    }
}
