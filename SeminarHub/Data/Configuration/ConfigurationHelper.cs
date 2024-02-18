using Microsoft.AspNetCore.Identity;
using SeminarHub.Data.Models;

namespace SeminarHub.Data.Configuration
{
    public static class ConfigurationHelper
    {
        public static Category Cat1 = new Category()
        {
            Id = 1,
            Name = "Technology & Innovation"
        };

        public static Category Cat2 = new Category()
        {
            Id = 2,
            Name = "Business & Entrepreneurship"
        };

        public static Category Cat3 = new Category()
        {
            Id = 3,
            Name = "Science & Research"
        };

        public static Category Cat4 = new Category()
        {
            Id = 4,
            Name = "Arts & Culture"
        };

        public static IdentityUser TestUser = GetUser();
        private static IdentityUser GetUser()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            var user = new IdentityUser()
            {
                UserName = "test@softuni.bg",
                NormalizedUserName = "TEST@SOFTUNI.BG",
            };

            user.PasswordHash = hasher.HashPassword(user, "softuni");

            return user;
        }
    }
}
