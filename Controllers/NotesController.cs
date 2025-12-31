using Microsoft.AspNetCore.Mvc;
using NotesAPI.Data;

namespace NotesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class NotesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateNote(Note note)
        {
            note.CreatedAt = DateTime.Now;

            _context.Notes.Add(note);
            _context.SaveChanges();


            return Ok(note);
        }

        [HttpGet]

        public IActionResult GetAllNotes()
        {
            var notes = _context.Notes.ToList();
            return Ok(notes);
        }

        [HttpGet("{id}")]

        public IActionResult GetNoteById(int id)
        {
            var note = _context.Notes.FirstOrDefault(n => n.NoteID == id);
            if(note == null)
            {
                return NotFound();
            }
            
            return Ok(note);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateNote(int id, Note UpdatedNote)
        {
            var existingnote = _context.Notes.FirstOrDefault(n => n.NoteID == id);

            if(existingnote ==null)
            {
                return NotFound();
            }

            existingnote.Title = UpdatedNote.Title;
            existingnote.Content = UpdatedNote.Content;
            existingnote.UpdatedAt = DateTime.Now;

            _context.SaveChanges();

            return Ok(existingnote);
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteNote(int id)
        {
            var note = _context.Notes.FirstOrDefault(c=> c.NoteID ==id);

            if (note == null)
            {
                return NotFound();
            }
              
            _context.Notes.Remove(note);  
            _context.SaveChanges();
            return NoContent();
        }
    }
}