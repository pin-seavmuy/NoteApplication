using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Models;
using NotesApi.Repositories;
using System.Security.Claims;

namespace NotesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteRepository _repo;

        public NotesController(INoteRepository repo)
        {
            _repo = repo;
        }

        private int GetUserId() =>
            int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] string? sortBy, [FromQuery] bool asc = false)
        {
            var userId = GetUserId();
            var notes = await _repo.GetAllAsync(userId, search, sortBy, asc);
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetUserId();
            var note = await _repo.GetByIdAsync(id, userId);
            return note == null ? NotFound() : Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NoteCreateDto dto)
        {
            var userId = GetUserId();
            var newId = await _repo.CreateAsync(dto, userId);
            var note = await _repo.GetByIdAsync(newId, userId);
            return CreatedAtAction(nameof(GetById), new { id = newId }, note);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NoteUpdateDto dto)
        {
            var userId = GetUserId();
            var updated = await _repo.UpdateAsync(id, dto, userId);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var deleted = await _repo.DeleteAsync(id, userId);
            return deleted ? NoContent() : NotFound();
        }
    }
}
