using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarHub.Data.Models
{
    public class SeminarParticipant
    {
        [Required]
        [Comment("Seminar identifier")]
        public int SeminarId { get; set; }
        [ForeignKey(nameof(SeminarId))]
        public Seminar Seminar { get; set; } = null!;
        [Required]
        [Comment("Identifier for an application user who is a participant in the seminar")]
        public string ParticipantId { get; set; } = string.Empty;
        [ForeignKey(nameof(ParticipantId))]
        public IdentityUser Participant { get; set; } = null!;
    }
}
