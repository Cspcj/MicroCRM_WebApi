using Microsoft.EntityFrameworkCore;
using MCRMWebApi.DTOs;
using Microsoft.Extensions.Options;

namespace MCRMWebApi.DataContext
{
    public class MCRMDbContext : DbContext
    {
        public MCRMDbContext(DbContextOptions<MCRMDbContext> options)
        : base(options)
        {
            
        }

        public DbSet<ClientDTO> Clients { get; set; }
        public DbSet<ProjectDTO> Projects { get; set; }
        public DbSet<NoteDTO> Notes { get; set; }
        public DbSet<TaskDTO> Tasks { get; set; }
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<EmployeeDTO> Employees { get; set; }
    }
}
