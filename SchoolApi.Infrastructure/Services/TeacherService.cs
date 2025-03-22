using Microsoft.EntityFrameworkCore;
using SchoolApi.Application.Interfaces;
using SchoolApi.Domain.Entities;
using SchoolApi.Infrastructure.Data;

namespace SchoolApi.Infrastructure.Services;
public class TeacherService : ITeacherService
{
    private readonly AppDbContext _context;
    public TeacherService(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Teacher>> GetAllAsync() => await _context.Teachers.ToListAsync();
    public async Task<Teacher?> GetByIdAsync(int id) => await _context.Teachers.FindAsync(id);
    public async Task<Teacher> CreateAsync(Teacher teacher)
    {
        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();
        return teacher;
    }
    public async Task UpdateAsync(Teacher teacher)
    {
        _context.Teachers.Update(teacher);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var data = await _context.Teachers.FindAsync(id);
        if (data != null)
        {
            _context.Teachers.Remove(data);
            await _context.SaveChangesAsync();
        }
    }
}
