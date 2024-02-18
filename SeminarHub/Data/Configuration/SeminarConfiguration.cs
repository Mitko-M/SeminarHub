using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeminarHub.Data.Models;

namespace SeminarHub.Data.Configuration
{
    public class SeminarConfiguration : IEntityTypeConfiguration<Seminar>
    {
        public void Configure(EntityTypeBuilder<Seminar> builder)
        {
            builder.HasData(new Seminar[]
            {
                new Seminar()
                {
                    Id = 1,
                    Topic = "AI Unmasked: Beyond Myths and Limits",
                    Lecturer = "Yani Lozanova",
                    OrganizerId = ConfigurationHelper.TestUser.Id,
                    DateAndTime = DateTime.Parse("07/03/2024 19:00"),
                    Duration = 35,
                    CategoryId = 1
                },
                new Seminar()
                {
                    Id = 2,
                    Topic = "Hypersonic sound and other inventions",
                    Lecturer = "WOODY NORRIS",
                    OrganizerId = ConfigurationHelper.TestUser.Id,
                    DateAndTime = DateTime.Parse("30/03/2024 17:15"),
                    Duration = 45,
                    CategoryId = 3
                },
                new Seminar()
                {
                    Id = 3,
                    Topic = "Let's reframe cancel culture",
                    Lecturer = "Sarah Jones",
                    OrganizerId = ConfigurationHelper.TestUser.Id,
                    DateAndTime = DateTime.Parse("25/04/2024 13:00"),
                    Duration = 120,
                    CategoryId = 4
                }
            });
        }
    }
}
