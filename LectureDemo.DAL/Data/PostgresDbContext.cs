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
    public DbSet<IndexedVectorLecture> IndexedVectorLectures { get; set; }

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


            //as per milan jovanvic video 
                  modelBuilder.Entity<IndexedVectorLecture>().HasKey(il => il.Id);

        // Configure the generated tsvector column
        modelBuilder.Entity<IndexedVectorLecture>().HasGeneratedTsVectorColumn(
            il => il.SearchVector, // Target column
            "english", // Language
            il => new { il.Title, il.Description } // Fields contributing to the tsvector
        );

        // Create an index on the SearchVector column with GIN
        modelBuilder.Entity<IndexedVectorLecture>().HasIndex(il => il.SearchVector)
            .HasMethod("GIN");

    }
    }
} 