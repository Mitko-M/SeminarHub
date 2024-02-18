using System.ComponentModel.DataAnnotations;
using static SeminarHub.Models.ErrorMessages;
using static SeminarHub.Data.DataConstatns;
using SeminarHub.Models.Category;

namespace SeminarHub.Models.Seminar
{
    public class SeminarFormModel
    {
        [Required(ErrorMessage = RequiredError)]
        [StringLength(TopicMax, MinimumLength = TopicMin, ErrorMessage = LengthError)]
        public string Topic { get; set; } = string.Empty;
        [Required(ErrorMessage = RequiredError)]
        [StringLength(LecturerMax, MinimumLength = LecturerMin, ErrorMessage = LengthError)]
        public string Lecturer { get; set; } = string.Empty;
        [Required(ErrorMessage = RequiredError)]
        [StringLength(DetailsMax, MinimumLength = DetailsMin, ErrorMessage = LengthError)]
        public string Details { get; set; } = string.Empty;
        [Required(ErrorMessage = RequiredError)]
        public DateTime DateAndTime { get; set; }
        [Required(ErrorMessage = RequiredError)]
        [Range(DurationMin, DurationMax, ErrorMessage = DurationError)]
        public int Duration { get; set; }
        [Required(ErrorMessage = RequiredError)]
        public int CategoryId { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
