using LectureDemo.Domain.Entities;

namespace LectureDemo.DAL.Repositories
{
    public interface IIndexedLectureRepository
    {
        Task<IEnumerable<IndexedLecture>> GetAllAsync();
        Task<IndexedLecture?> GetByIdAsync(int id);
        Task<IndexedLecture> AddAsync(IndexedLecture indexedLecture);
        Task<bool> UpdateAsync(IndexedLecture indexedLecture);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<IndexedLecture>> SearchByFullTextAsync(string searchTerm);
        Task<IEnumerable<object>> SearchByFullTextWithRankingAsync(string searchTerm);
    }
}