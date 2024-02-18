namespace SeminarHub.Models.Seminar
{
    public class SeminarDetailsViewModel : SeminarViewModel
    {
        public int Duration { get; set; }
        public string Details { get; set; } = string.Empty;
    }
}
