using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]

        public async Task<IActionResult> GetAllNotes()
        {
            var note = await _context.Notes.ToListAsync();

            return Ok(note);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetNotebyID(int id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(n=>n.NoteID ==id);
            
            if(note == null)
            {
                return NotFound();
            }
            
            return Ok(note);
        }

        [HttpPost]

        public async Task<IActionResult> CreateNote(Note note)
        {
            

            if(note == null)
            {
                return BadRequest();
            }
            note.CreatedAt = DateTime.Now;

           await _context.Notes.AddAsync(note);
           await _context.SaveChangesAsync();

           return Ok(note);
            
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> updateNote(int id,Note note)
        {
            var existingNote = await _context.Notes.FirstOrDefaultAsync(n=>n.NoteID ==id);
            
            if(existingNote == null)
            {
                return NotFound();
            }

            existingNote.Title = note.Title;
            existingNote.Content = note.Content;
            existingNote.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(existingNote);

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> deleteNote(int id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(c=>c.NoteID==id);

            if(note==null)
            {
                return NotFound();
            }

             _context.Notes.Remove(note);
             await _context.SaveChangesAsync();

             return NoContent();
        }

    }
}