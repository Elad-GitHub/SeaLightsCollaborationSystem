using Microsoft.EntityFrameworkCore;

namespace ReportsCollaborationAPI.Models
{
    public class CollaborationSystemContext : DbContext
    {
        public CollaborationSystemContext(DbContextOptions<CollaborationSystemContext> options) : base(options)
        {

        }

        public DbSet<Note> Notes { get; set; }

        public DbSet<File> Files { get; set; }
    }
}
