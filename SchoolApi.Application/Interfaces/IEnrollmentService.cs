using SchoolApi.Domain.Entities;
using SchoolApi.Application.DTOs;


namespace SchoolApi.Application.Interfaces;

public interface IEnrollmentService
{
    Task<IEnumerable<EnrollmentResponseDto>> GetAllAsync();
    Task<Enrollment?> GetByIdAsync(int id);
    Task<Enrollment> CreateAsync(Enrollment enrollment);
    Task UpdateAsync(Enrollment enrollment);
    Task DeleteAsync(int id);
}
