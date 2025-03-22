using Microsoft.EntityFrameworkCore;
using SchoolApi.Application.DTOs;
using SchoolApi.Application.Interfaces;
using SchoolApi.Domain.Entities;
using SchoolApi.Infrastructure.Data;

namespace SchoolApi.Infrastructure.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly AppDbContext _context;

    public EnrollmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EnrollmentResponseDto>> GetAllAsync()
    {
        var enrollments = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Subject)
            .Include(e => e.Teacher)
            .Include(e => e.Class)
            .ToListAsync();

        var result = enrollments.Select(e => new EnrollmentResponseDto
        {
            Id = e.Id,
            StudentName = e.Student?.Name ?? "",
            SubjectName = e.Subject?.Title ?? "",
            TeacherName = e.Teacher?.Name ?? "",
            ClassName = e.Class?.Name ?? ""
        });

        return result;
    }

    public async Task<Enrollment?> GetByIdAsync(int id)
    {
        return await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Subject)
            .Include(e => e.Teacher)
            .Include(e => e.Class)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Enrollment> CreateAsync(Enrollment enrollment)
    {
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();
        return enrollment;
    }

    public async Task UpdateAsync(Enrollment enrollment)
    {
        _context.Enrollments.Update(enrollment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment != null)
        {
            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
        }
    }
}
