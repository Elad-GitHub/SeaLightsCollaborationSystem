using Microsoft.EntityFrameworkCore;

namespace ReportsCollaborationAPI.Models
{
    public class NoteContext : DbContext
    {
        public NoteContext(DbContextOptions<NoteContext> options) : base(options)
        {

        }

        public DbSet<Note> Notes { get; set; }
    }
}
