using Microsoft.AspNetCore.Mvc;
using NotesApi.Models;
using NotesApi;

namespace NotesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteRepository _repo;

        public NotesController(INoteRepository repo)
        {
            _repo = repo;
        }

        // GET: api/notes
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] string? sortBy, [FromQuery] bool asc = false)
        {
            int userId = 1; // 🔒 Temporary static user ID
            var notes = await _repo.GetAllAsync(userId, search, sortBy, asc);
            return Ok(notes);
        }

        // GET: api/notes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            int userId = 1;
            var note = await _repo.GetByIdAsync(id, userId);
            if (note == null)
                return NotFound();
            return Ok(note);
        }

        // POST: api/notes
        [HttpPost]
        public async Task<IActionResult> Create(NoteCreateDto dto)
        {
            int userId = 1;
            var newId = await _repo.CreateAsync(dto, userId);
            var createdNote = await _repo.GetByIdAsync(newId, userId);
            return CreatedAtAction(nameof(GetById), new { id = newId }, createdNote);
        }

        // PUT: api/notes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NoteUpdateDto dto)
        {
            int userId = 1;
            var updated = await _repo.UpdateAsync(id, dto, userId);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        // DELETE: api/notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int userId = 1;
            var deleted = await _repo.DeleteAsync(id, userId);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
