using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // EF Core extensions (UseSqlite)
using DemoApi.Data; // AppDbContext

var builder = WebApplication.CreateBuilder(args);

// --- PHẦN 1: ĐĂNG KÝ DỊCH VỤ (Services) ---

// QUAN TRỌNG: Dòng này để kích hoạt tính năng Controller
builder.Services.AddControllers(); 

// Các dịch vụ cho Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // Cấu hình dùng SQLite, file database tên là "app.db"
    options.UseSqlite("Data Source=app.db");
});

var app = builder.Build();

// --- PHẦN 2: CẤU HÌNH MIDDLEWARE (Pipeline) ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// QUAN TRỌNG: Dòng này để tự động tìm và map các Route trong folder Controllers
app.MapControllers(); 

app.Run();