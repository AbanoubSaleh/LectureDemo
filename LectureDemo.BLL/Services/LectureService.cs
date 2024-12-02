using LectureDemo.DAL.Repositories;
using LectureDemo.Domain.Entities;

namespace LectureDemo.BLL.Services;

public class LectureService : ILectureService
{
    private readonly ILectureRepository _lectureRepository;

    public LectureService(ILectureRepository lectureRepository)
    {
        _lectureRepository = lectureRepository;
    }

    public async Task<IEnumerable<Lecture>> SearchSqlServerByDescriptionAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return Enumerable.Empty<Lecture>();

        return await _lectureRepository.SearchSqlServerByDescriptionAsync(searchTerm);
    }

    public async Task<IEnumerable<Lecture>> SearchPostgresqlByDescriptionAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return Enumerable.Empty<Lecture>();

        return await _lectureRepository.SearchPostgresqlByDescriptionAsync(searchTerm);
    }

    public Task<Lecture> AddAsync(Lecture lecture)
    {
        return _lectureRepository.AddAsync(lecture);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return _lectureRepository.DeleteAsync(id);
    }

    public Task<IEnumerable<Lecture>> GetAllAsync()
    {
        return _lectureRepository.GetAllAsync();
    }

    public Task<Lecture?> GetByIdAsync(int id)
    {
        return _lectureRepository.GetByIdAsync(id);
    }

    public Task<bool> UpdateAsync(Lecture lecture)
    {
        return _lectureRepository.UpdateAsync(lecture);
    }
} 