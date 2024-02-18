using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeminarHub.Data.Models;

namespace SeminarHub.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category[] 
            { 
                ConfigurationHelper.Cat1, 
                ConfigurationHelper.Cat2, 
                ConfigurationHelper.Cat3, 
                ConfigurationHelper.Cat4 
            });
        }
    }
}
