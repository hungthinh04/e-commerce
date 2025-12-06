using Microsoft.EntityFrameworkCore;
using DemoApi.Models; // Import Model Dev

namespace DemoApi.Data;

// Kế thừa từ DbContext của hệ thống
public class AppDbContext : DbContext
{
    // Constructor này để nhận cấu hình từ Program.cs (chuỗi kết nối...)
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Khai báo bảng Developers trong Database
    // Tên biến 'Developers' sẽ là tên bảng trong DB sau này
    public DbSet<Dev> Developers { get; set; }
}