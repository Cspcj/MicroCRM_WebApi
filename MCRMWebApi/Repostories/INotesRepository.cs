using MCRMWebApi.DTOs;

namespace MCRMWebApi.Repostories
{
    public interface INotesRepository
    {
        Task<IEnumerable<NoteDTO>> GetAllNotes();
        Task<NoteDTO> GetNoteByID(Guid id);
        Task<NoteDTO> AddNote(NotePartialCreateDTO note);
        Task<NoteDTO> DeleteNote(Guid id);
        Task<NoteDTO> UpdatePartial(Guid id, NotePartialUpdateDTO note);
    }
}
