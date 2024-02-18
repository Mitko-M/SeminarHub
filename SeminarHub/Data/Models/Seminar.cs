using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SeminarHub.Data.DataConstatns;

namespace SeminarHub.Data.Models
{
    public class Seminar
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(TopicMin)]
        [MaxLength(TopicMax)]
        public string Topic { get; set; } = string.Empty;
        [Required]
        [MinLength(LecturerMin)]
        [MaxLength(LecturerMax)]
        public string Lecturer { get; set; } = string.Empty;
        [Required]
        [MinLength(DetailsMin)]
        [MaxLength(DetailsMax)]
        public string Details { get; set; } = string.Empty;
        [Required]
        public string OrganizerId { get; set; } = string.Empty;
        [ForeignKey(nameof(OrganizerId))]
        public IdentityUser Organizer { get; set; } = null!;
        [Required]
        public DateTime DateAndTime { get; set; }
        [Required]
        [Range(DurationMin, DurationMax)]
        public int Duration { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public ICollection<SeminarParticipant> SeminarsParticipants { get; set; } = new List<SeminarParticipant>();
    }
}
