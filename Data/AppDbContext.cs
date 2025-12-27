using Microsoft.EntityFrameworkCore;


namespace NotesAPI.Data 
{
    public class AppDbContext : DbContext
    {
        public DbSet<Note> Notes{get;set;}

        public DbSet<Category> Categories{get;set;}

        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
            
        }
       
    }
}