using LectureDemo.BLL.Services;
using LectureDemo.DAL.Repositories;
using LectureDemo.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LectureDemo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LecturesController : ControllerBase
{
    private readonly ILectureService _lectureService;
    private readonly IIndexedLectureRepository _indexedLectureRepository;

    public LecturesController(ILectureService lectureService, IIndexedLectureRepository indexedLectureRepository)
    {
        _lectureService = lectureService;
        _indexedLectureRepository = indexedLectureRepository;
    }

    /// <summary>
    /// Searches for lectures in SQL Server by description.
    /// </summary>
    /// <param name="term">The search term to filter lectures.</param>
    /// <returns>A list of lectures that match the search term.</returns>
    [HttpGet("search/sqlserver")]
    public async Task<ActionResult<IEnumerable<Lecture>>> SearchSqlServer([FromQuery] string term)
    {
        if (string.IsNullOrWhiteSpace(term))
            return BadRequest("Search term cannot be empty");

        var results = await _lectureService.SearchSqlServerByDescriptionAsync(term);
        return Ok(results);
    }

    /// <summary>
    /// Searches for lectures in PostgreSQL by description.
    /// </summary>
    /// <param name="term">The search term to filter lectures.</param>
    /// <returns>A list of lectures that match the search term.</returns>
    [HttpGet("search/postgresql")]
    public async Task<ActionResult<IEnumerable<Lecture>>> SearchPostgresql([FromQuery] string term)
    {
        if (string.IsNullOrWhiteSpace(term))
            return BadRequest("Search term cannot be empty");

        var results = await _lectureService.SearchPostgresqlByDescriptionAsync(term);
        return Ok(results);
    }

    /// <summary>
    /// Searches for lectures using full-text search in PostgreSQL.
    /// </summary>
    /// <param name="term">The search term to filter lectures.</param>
    /// <returns>A list of lectures that match the search term.</returns>
    [HttpGet("search/fulltext")]
    public async Task<ActionResult<IEnumerable<Lecture>>> SearchByFullText([FromQuery] string term)
    {

        var results = await _indexedLectureRepository.SearchByFullTextAsync(term);
        return Ok(results);
    }


    /// <summary>
    /// Searches for lectures using full-text search in PostgreSQL.
    /// </summary>
    /// <param name="term">The search term to filter lectures.</param>
    /// <returns>A list of lectures that match the search term.</returns>
    [HttpGet("search/fulltextExactmatch")]
    public async Task<ActionResult<IEnumerable<Lecture>>> SearchByFullTextExactmatch([FromQuery] string term)
    {

        var results = await _indexedLectureRepository.SearchByFullTextExactMatchAsync(term);
        return Ok(results);
    }

    /// <summary>
    /// Searches for lectures using full-text search in PostgreSQL.
    /// </summary>
    /// <param name="term">The search term to filter lectures.</param>
    /// <returns>A list of lectures that match the search term.</returns>
    [HttpGet("search/fulltextWithRanking")]
    public async Task<ActionResult<IEnumerable<Lecture>>> SearchByFullTextWithRanking([FromQuery] string term)
    {

        var results = await _indexedLectureRepository.SearchByFullTextWithRankingAsync(term);
        return Ok(results);
    }
    /// <summary>
    /// Searches for lectures using full-text search in PostgreSQL.
    /// </summary>
    /// <param name="term">The search term to filter lectures.</param>
    /// <returns>A list of lectures that match the search term.</returns>
    [HttpGet("search/FullTextWithRankingAndWeight")]
    public async Task<ActionResult<IEnumerable<Lecture>>> FullTextWithRankingAndWeight([FromQuery] string term)
    {

        var results = await _indexedLectureRepository.SearchByFullTextWithRankingAndWeightAsync(term);
        return Ok(results);
    }

}