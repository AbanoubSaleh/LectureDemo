using LectureDemo.DAL.Data.Extensions;
using LectureDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LectureDemo.DAL.Data
{
    public class PostgresDbContext : DbContext
    {
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options)
        : base(options)
    {
    }

    public DbSet<Lecture> Lectures { get; set; }
    public DbSet<IndexedLecture> IndexedLectures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IndexedLecture>()
            .Property(i => i.Title)
            .HasColumnType("text");

        modelBuilder.Entity<IndexedLecture>()
            .Property(i => i.Description)
            .HasColumnType("text");

        modelBuilder.Entity<IndexedLecture>()
            .HasIndex(i => new { i.Title, i.Description })
            .HasMethod("GIN")
            .IsTsVectorExpressionIndex("english");
    }
    }
} 