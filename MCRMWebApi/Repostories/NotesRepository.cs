using MCRMWebApi.DTOs;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.DTOs.PartialDTOUpdate;
using MCRMWebApi.Services;
using AutoMapper;
using MCRMWebApi.DataContext;
using Microsoft.EntityFrameworkCore;

namespace MCRMWebApi.Repostories
{
    public class NotesRepository : INotesRepository
    {
        private readonly MCRMDbContext _context;
        private readonly IMapper _mapper;
        private readonly IProjectsRepository _projectsRepository;
        private readonly IClientsRepository _clientsRepository;
        private readonly IEmployeesRepository _employeesRepository;
        private readonly ILogger<NotesRepository> _logger;

        public NotesRepository(MCRMDbContext context, 
            IProjectsRepository projectRepository,
            IClientsRepository clientsRepository,
            IEmployeesRepository employeesRepository,
            IMapper mapper,
            ILogger<NotesRepository> logger)
        {
            _context = context;
            _projectsRepository = projectRepository;
            _logger = logger;
            _mapper = mapper;
            _clientsRepository = clientsRepository;
            _employeesRepository = employeesRepository;
        }


        public async Task<IEnumerable<NoteDTO>> GetAllNotes()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<NoteDTO> GetNoteByID(Guid id)
        {
            return await _context.Notes.FirstOrDefaultAsync(n => n.NoteId == id);
        }

        public async Task<NoteDTO> AddNote(NotePartialCreateDTO note)
        {
            NoteDTO noteEntity = _mapper.Map<NoteDTO>(note);
            noteEntity.DateCreated = DateTime.Now;

            if (noteEntity.ProjectId == -1)
            {
                // we asume that if project id is null then we are adding a standalone note
                noteEntity.ProjectId = -1;
            }
            else
            {
                var project = await _projectsRepository.GetProject(note.ProjectId);
                if (project == null)
                {
                    _logger.LogError("Project id is incorrect.");
                    return null;
                }
            }

            var client = await _clientsRepository.GetClient(note.OwnerId);
            var employee = await _employeesRepository.GetEmployee(note.OwnerId);
            if ((client == null)&&(employee == null))
            {
                _logger.LogInformation("The client / employee ID is incorect. The note entry wasn't added");
                return null;
            }
            
            await _context.Notes.AddAsync(noteEntity);
            await _context.SaveChangesAsync();
            return noteEntity;
        }

        public async Task<NoteDTO> UpdatePartial(Guid id, NotePartialUpdateDTO note)
        {
            NoteDTO noteToBeUpdated = await GetNoteByID(id);
            if (noteToBeUpdated == null)
            {
                return null;
            }

            noteToBeUpdated.Title = (note.Title != null)&&(note.Title != noteToBeUpdated.Title) 
                ? note.Title : noteToBeUpdated.Title;
            noteToBeUpdated.Note = (note.Note != null) && (note.Note != noteToBeUpdated.Note)
                ? note.Note : noteToBeUpdated.Note;
            noteToBeUpdated.ProjectId = (note.ProjectId != null) && (note.ProjectId != noteToBeUpdated.ProjectId)
                ? (int) note.ProjectId : noteToBeUpdated.ProjectId;
            noteToBeUpdated.OwnerId= (note.OwnerId != null) && (note.OwnerId != noteToBeUpdated.OwnerId) 
                ? (Guid) note.OwnerId : noteToBeUpdated.OwnerId;

            _context.Notes.Update(noteToBeUpdated);
            await _context.SaveChangesAsync();
            return noteToBeUpdated;
        }
        public async Task<NoteDTO> DeleteNote(Guid id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(n => n.NoteId == id);
            if (note == null)
            {
                return null;
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return note;
        }
    }
}
