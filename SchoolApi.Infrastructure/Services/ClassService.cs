using Microsoft.EntityFrameworkCore;
using SchoolApi.Application.Interfaces;
using SchoolApi.Domain.Entities;
using SchoolApi.Infrastructure.Data;

namespace SchoolApi.Infrastructure.Services;

public class ClassService : IClassService
{
    private readonly AppDbContext _context;

    public ClassService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Class>> GetAllAsync()
    {
        return await _context.Classes.ToListAsync();
    }

    public async Task<Class?> GetByIdAsync(int id)
    {
        return await _context.Classes.FindAsync(id);
    }

    public async Task<Class> CreateAsync(Class classEntity)
    {
        _context.Classes.Add(classEntity);
        await _context.SaveChangesAsync();
        return classEntity;
    }

    public async Task UpdateAsync(Class classEntity)
    {
        _context.Classes.Update(classEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var classEntity = await _context.Classes.FindAsync(id);
        if (classEntity is not null)
        {
            _context.Classes.Remove(classEntity);     
            // Refactor
            await _context.SaveChangesAsync();
        }  
    }
}
