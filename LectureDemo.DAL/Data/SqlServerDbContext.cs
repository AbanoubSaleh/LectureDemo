using LectureDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using LectureDemo.DAL.Data.Extensions;

namespace LectureDemo.DAL.Data
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Lecture> Lectures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}