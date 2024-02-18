using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data.Configuration;
using SeminarHub.Data.Models;

namespace SeminarHub.Data
{
    public class SeminarHubDbContext : IdentityDbContext
    {
        public SeminarHubDbContext(DbContextOptions<SeminarHubDbContext> options)
            : base(options)
        {
        }

        public DbSet<Seminar> Seminars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SeminarParticipant> SeminarsParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<SeminarParticipant>()
                .HasKey(sp => new { sp.SeminarId, sp.ParticipantId });

            builder
                .Entity<SeminarParticipant>()
                .HasOne(s => s.Seminar)
                .WithMany(s => s.SeminarsParticipants)
                .HasForeignKey(s => s.SeminarId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new SeminarConfiguration());

            base.OnModelCreating(builder);
        }
    }
}