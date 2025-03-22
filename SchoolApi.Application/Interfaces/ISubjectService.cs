using SchoolApi.Domain.Entities;

namespace SchoolApi.Application.Interfaces;

public interface ISubjectService
{
    Task<IEnumerable<Subject>> GetAllAsync();
    Task<Subject?> GetByIdAsync(int id);
    Task<Subject> CreateAsync(Subject subject);
    Task UpdateAsync(Subject subject);
    Task DeleteAsync(int id);
}
