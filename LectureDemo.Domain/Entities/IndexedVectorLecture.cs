using NpgsqlTypes;
using System.ComponentModel.DataAnnotations.Schema;

namespace LectureDemo.Domain.Entities;

public class IndexedVectorLecture
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    // Add the SearchVector property
    [Column(TypeName = "tsvector")]
    public NpgsqlTsVector SearchVector { get; set; }
}
