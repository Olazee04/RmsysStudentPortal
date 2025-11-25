using Microsoft.EntityFrameworkCore;
using RmsysStudentPortal.web.Models.Entities;

namespace RmsysStudentPortal.web.Data
{
    public class RmsysStudentPortalDbContext: DbContext
    {
        public RmsysStudentPortalDbContext(DbContextOptions<RmsysStudentPortalDbContext> options): base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
    }
}
