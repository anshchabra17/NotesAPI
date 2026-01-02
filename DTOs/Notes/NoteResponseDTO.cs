namespace NotesAPI.DTOs.Notes
{
    public class NoteResponseDTO
    {
        public int NoteID { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int? CategoryID { get; set; }
    }
}