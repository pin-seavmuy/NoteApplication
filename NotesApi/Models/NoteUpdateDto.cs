namespace NotesApi.Models
{
    public class NoteUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
    }
}
