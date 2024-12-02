using LectureDemo.Domain.Entities;

namespace LectureDemo.BLL.Services;

public interface ILectureService
{
    Task<IEnumerable<Lecture>> GetAllAsync();
    Task<Lecture?> GetByIdAsync(int id);
    Task<Lecture> AddAsync(Lecture lecture);
    Task<bool> UpdateAsync(Lecture lecture);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Lecture>> SearchSqlServerByDescriptionAsync(string searchTerm);
    Task<IEnumerable<Lecture>> SearchPostgresqlByDescriptionAsync(string searchTerm);
} 