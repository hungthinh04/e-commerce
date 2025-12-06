using Microsoft.AspNetCore.Mvc;
using DemoApi.Models; // Phải import cái Model vừa tạo
using System.Collections.Generic;

namespace DemoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevController : ControllerBase
{
    // Giả lập Database (Lưu trên RAM, tắt server là mất)
    // Phải dùng static để danh sách này được giữ nguyên giữa các lần gọi API
    private static List<Dev> _devs = new List<Dev>
    {
        new Dev { Id = 1, Name = "Thịnh", Level = "Fullstack" },
        new Dev { Id = 2, Name = "Nam", Level = "Frontend" }
    };

    // 1. GET: api/dev (Lấy tất cả)
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_devs);
    }

    // 2. GET: api/dev/{id} (Lấy theo ID)
    // Ví dụ: api/dev/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        // LINQ: Tìm thằng nào có Id trùng với id truyền vào
        // Giống: _devs.find(d => d.Id === id) bên JS
        var dev = _devs.FirstOrDefault(d => d.Id == id);

        if (dev == null)
        {
            return NotFound(new { Message = "Không tìm thấy Dev này!" }); // Trả về 404
        }
        return Ok(dev);
    }

    // 3. POST: api/dev (Thêm mới)
    // Nhận vào một object Dev từ Body của request
    [HttpPost]
    public IActionResult Create(Dev newDev)
    {
        // Tự động tăng ID
        newDev.Id = _devs.Count + 1;
        
        _devs.Add(newDev);

        // Trả về 201 Created và data vừa tạo
        return CreatedAtAction(nameof(GetById), new { id = newDev.Id }, newDev);
    }

    // 4. PUT: api/dev/{id} (Cập nhật thông tin)
    // Nhận ID từ URL và thông tin mới từ Body
    [HttpPut("{id}")]
    public IActionResult Update(int id, Dev updatedDev)
    {
        var dev = _devs.FirstOrDefault(d => d.Id == id);

        // Nếu không tìm thấy ID thì báo lỗi 404
        if (dev == null)
        {
            return NotFound();
        }

        // Cập nhật dữ liệu
        dev.Name = updatedDev.Name;
        dev.Level = updatedDev.Level;

        // Trả về 204 No Content (Chuẩn HTTP cho việc sửa/xóa thành công mà không cần trả dữ liệu về)
        return NoContent();
    }

    // 5. DELETE: api/dev/{id} (Xóa sổ)
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var dev = _devs.FirstOrDefault(d => d.Id == id);

        if (dev == null)
        {
            return NotFound();
        }

        _devs.Remove(dev); // Xóa khỏi List
        return NoContent(); // 204 OK
    }
}