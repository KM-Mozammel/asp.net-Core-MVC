using Microsoft.EntityFrameworkCore;
using StudentPortal.Models.Entities;

namespace StudentPortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constuctor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
        {}

        // Using the Model of Student Entitie
        public DbSet<Student> Students { get; set; }
    }
}
