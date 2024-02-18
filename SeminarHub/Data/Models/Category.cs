using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.DataConstatns;

namespace SeminarHub.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(NameMin)]
        [MaxLength(NameMax)]
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Seminar> Seminars { get; set; } = new List<Seminar>();
    }
}
