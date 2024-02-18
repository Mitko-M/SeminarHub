using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SeminarHub.Data.DataConstatns;

namespace SeminarHub.Data.Models
{
    public class Seminar
    {
        [Key]
        [Comment("Seminar identifier")]
        public int Id { get; set; }
        [Required]
        [MinLength(TopicMin)]
        [MaxLength(TopicMax)]
        [Comment("Seminar topic")]
        public string Topic { get; set; } = string.Empty;
        [Required]
        [MinLength(LecturerMin)]
        [MaxLength(LecturerMax)]
        [Comment("Seminar lecturer")]
        public string Lecturer { get; set; } = string.Empty;
        [Required]
        [MinLength(DetailsMin)]
        [MaxLength(DetailsMax)]
        [Comment("Seminar details")]
        public string Details { get; set; } = string.Empty;
        [Required]
        [Comment("Seminar organizer")]
        public string OrganizerId { get; set; } = string.Empty;
        [ForeignKey(nameof(OrganizerId))]
        public IdentityUser Organizer { get; set; } = null!;
        [Required]
        [Comment("Seminar beginning time and date")]
        public DateTime DateAndTime { get; set; }
        [Required]
        [Range(DurationMin, DurationMax)]
        [Comment("Seminar duration")]
        public int Duration { get; set; }
        [Required]
        [Comment("Seminar cateofry identifier")]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public ICollection<SeminarParticipant> SeminarsParticipants { get; set; } = new List<SeminarParticipant>();
    }
}
