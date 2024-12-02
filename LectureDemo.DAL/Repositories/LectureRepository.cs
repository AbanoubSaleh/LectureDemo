using LectureDemo.DAL.Data;
using LectureDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LectureDemo.DAL.Repositories;

public class LectureRepository : ILectureRepository
{
    private readonly SqlServerDbContext _sqlContext;
    private readonly PostgresDbContext _postgresContext;

    public LectureRepository(SqlServerDbContext sqlContext, PostgresDbContext postgresContext)
    {
        _sqlContext = sqlContext;
        _postgresContext = postgresContext;
    }

    public async Task<IEnumerable<Lecture>> SearchSqlServerByDescriptionAsync(string searchTerm)
    {
        return await _sqlContext.Lectures
            .Where(l => EF.Functions.Like(l.Description, $"%{searchTerm}%"))
            .ToListAsync();
    }

    public async Task<IEnumerable<Lecture>> SearchPostgresqlByDescriptionAsync(string searchTerm)
    {
        return await _postgresContext.Lectures
            .Where(l => EF.Functions.ILike(l.Description, $"%{searchTerm}%"))
            .ToListAsync();
    }

    public Task<Lecture> AddAsync(Lecture lecture)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Lecture>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Lecture?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Lecture lecture)
    {
        throw new NotImplementedException();
    }

    // Implement your repository methods here
} 