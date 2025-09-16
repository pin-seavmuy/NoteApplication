namespace Note.Models
{
    public class Note
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Replace with real user logic later
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
