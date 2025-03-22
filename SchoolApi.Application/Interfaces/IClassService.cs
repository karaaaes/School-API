using SchoolApi.Domain.Entities;

namespace SchoolApi.Application.Interfaces;

public interface IClassService
{
    Task<IEnumerable<Class>> GetAllAsync();
    Task<Class?> GetByIdAsync(int id);
    Task<Class> CreateAsync(Class classEntity);
    Task UpdateAsync(Class classEntity);
    Task DeleteAsync(int id);
}
