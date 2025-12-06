using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Để dùng ToListAsync
using DemoApi.Data;
using DemoApi.Models;

namespace DemoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevController : ControllerBase
{
    private readonly AppDbContext _context;

    // 1. Dependency Injection: Xin Database từ hệ thống
    public DevController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/dev
    [HttpGet]
    public async Task<IActionResult> GetDevelopers()
    {
        // SELECT * FROM Developers
        var devs = await _context.Developers.ToListAsync(); 
        return Ok(devs);
    }

    // GET: api/dev/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDev(int id)
    {
        var dev = await _context.Developers.FindAsync(id);
        if (dev == null) return NotFound();
        return Ok(dev);
    }

    // POST: api/dev
    [HttpPost]
    public async Task<IActionResult> CreateDev(Dev dev)
    {
        // INSERT INTO Developers VALUES (...)
        _context.Developers.Add(dev);
        
        // Lệnh này mới thực sự lưu vào DB (Commit)
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDev), new { id = dev.Id }, dev);
    }

    // PUT: api/dev/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDev(int id, Dev devUpdate)
    {
        if (id != devUpdate.Id) return BadRequest();

        // Đánh dấu object này là "đã bị sửa"
        _context.Entry(devUpdate).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Developers.Any(e => e.Id == id)) return NotFound();
            else throw;
        }

        return NoContent();
    }

    // DELETE: api/dev/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDev(int id)
    {
        var dev = await _context.Developers.FindAsync(id);
        if (dev == null) return NotFound();

        _context.Developers.Remove(dev);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}