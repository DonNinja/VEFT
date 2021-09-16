using Microsoft.EntityFrameworkCore;
using TechnicalRadiation.Models.Entities;

namespace TechnicalRadiation.Repositories.Contexts
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasOne(a => a.Name);
                // .WithMany(u => u.MessagesSent);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.UserTo)
                .WithMany(u => u.MessagesReceived);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<JwtToken> JwtTokens { get; set; }
    }
}