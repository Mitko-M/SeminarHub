using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.DataConstatns;

namespace SeminarHub.Data.Models
{
    public class Category
    {
        [Key]
        [Comment("Category identifier")]
        public int Id { get; set; }
        [Required]
        [MinLength(NameMin)]
        [MaxLength(NameMax)]
        [Comment("Category name")]
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Seminar> Seminars { get; set; } = new List<Seminar>();
    }
}
