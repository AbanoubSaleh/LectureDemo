using LectureDemo.DAL.Data;
using LectureDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using System.Text.RegularExpressions;

namespace LectureDemo.DAL.Repositories
{
    public class IndexedLectureRepository : IIndexedLectureRepository
    {
        private readonly SqlServerDbContext _sqlContext;
        private readonly PostgresDbContext _postgresContext;
        public IndexedLectureRepository(SqlServerDbContext sqlContext, PostgresDbContext postgresContext)
        {
            _sqlContext = sqlContext;
            _postgresContext = postgresContext;
        }

        public Task<IndexedLecture> AddAsync(IndexedLecture indexedLecture)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IndexedLecture>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IndexedLecture?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IndexedLecture>> SearchByFullTextAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Enumerable.Empty<IndexedLecture>(); // Return an empty list if searchTerm is invalid
            }
            var tsQuery = string.Join(" & ", Regex.Split(searchTerm.Trim(), @"\s+").Where(word => !string.IsNullOrEmpty(word)));

            return await _postgresContext.IndexedLectures
                .Where(il => EF.Functions
                    .ToTsVector("english", il.Title + " " + il.Description)
                    .Matches(EF.Functions.ToTsQuery("english", tsQuery)))
                .ToListAsync();
        }

        public async Task<IEnumerable<IndexedLecture>> SearchByFullTextExactMatchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Enumerable.Empty<IndexedLecture>(); // Return an empty list if searchTerm is invalid
            }

            return await _postgresContext.IndexedLectures
                .Where(il => EF.Functions
                    .ToTsVector("english", il.Title + " " + il.Description)
                    .Matches(EF.Functions.PhraseToTsQuery("english", searchTerm)))
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> SearchByFullTextWithRankingAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Enumerable.Empty<object>(); // Return an empty list if searchTerm is invalid
            }

            // Create a tsquery from the searchTerm
            var tsQuery = string.Join(" & ", Regex.Split(searchTerm.Trim(), @"\s+").Where(word => !string.IsNullOrEmpty(word)));

            return await _postgresContext.IndexedLectures
                .Where(il => EF.Functions
                    .ToTsVector("english", il.Title + " " + il.Description)
                    .Matches(EF.Functions.ToTsQuery("english", tsQuery)))
                .Select(il => new
                {
                    il.Id,
                    il.Title,
                    il.Description,
                    Rank = EF.Functions
                        .ToTsVector("english", il.Title + " " + il.Description).Rank(
                          EF.Functions.ToTsQuery("english", tsQuery)
                        ) // Calculate rank
                })
                .OrderByDescending(x => x.Rank) // Order results by rank
                .ToListAsync();

        }

        public async Task<IEnumerable<object>> SearchByFullTextWithRankingAndWeightAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Enumerable.Empty<IndexedVectorLecture>(); // Return an empty list if searchTerm is invalid
            }

            var tsQuery = string.Join(" & ", Regex.Split(searchTerm.Trim(), @"\s+").Where(word => !string.IsNullOrEmpty(word)));

            return await _postgresContext.IndexedVectorLectures
                .Where(il => EF.Functions
                    .ToTsVector("english", il.Title)
                    .SetWeight(NpgsqlTsVector.Lexeme.Weight.A)
                    .Concat(EF.Functions.ToTsVector("english", il.Description)
                        .SetWeight(NpgsqlTsVector.Lexeme.Weight.B))
                    .Matches(EF.Functions.ToTsQuery("english", tsQuery)))
                .Select(il => new
                {
                    il.Id,
                    il.Title,
                    il.Description,
                    Rank = EF.Functions
                        .ToTsVector("english", il.Title)
                        .SetWeight(NpgsqlTsVector.Lexeme.Weight.A)
                        .Concat(EF.Functions.ToTsVector("english", il.Description)
                            .SetWeight(NpgsqlTsVector.Lexeme.Weight.B))
                        .Rank(EF.Functions.ToTsQuery("english", tsQuery))
                })
                .OrderByDescending(x => x.Rank)
                .ToListAsync();

        }

        public Task<bool> UpdateAsync(IndexedLecture indexedLecture)
        {
            throw new NotImplementedException();
        }
    }
}