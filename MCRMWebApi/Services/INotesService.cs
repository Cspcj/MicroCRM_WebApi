using MCRMWebApi.DTOs;

namespace MCRMWebApi.Services
{
    public interface INotesService
    {
        // generate interface for NotesService

        Task<NoteDTO> AddNoteAsync(NotePartialCreateDTO note);
        Task<NoteDTO> DeleteNoteAsync(Guid id);
        Task<IEnumerable<NoteDTO>> GetAllNotes();
        Task<NoteDTO> GetNoteByID(Guid id);
        Task<NoteDTO> UpdatePartial(Guid id, NotePartialUpdateDTO note);
    }
}
