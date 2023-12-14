using System.Data;
using CQRSApi.Models;
using Dapper;

namespace CQRSApi.Repositories.ReadRepositories;

public interface IMovieReadRepository
{
    public Task<Movie?> GetByIdAsync(Guid id);
    public Task<List<Movie>> GetAllAsync();
}

public class MovieReadRepository : IMovieReadRepository
{
    private readonly IDbConnection _dbConnection;

    public MovieReadRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Movie?> GetByIdAsync(Guid id)
    {
        var sqlQuery = @"
SELECT
    Id,
    Title,
    Genre,
    DateOfRelease
    FROM movies
where Id = @Id";

        var result = await _dbConnection.QueryFirstOrDefaultAsync<Movie>(sqlQuery, new { Id = id });
        return result;
    }

    public async Task<List<Movie>> GetAllAsync()
    {
        var sqlQuery = @"
SELECT
    Id,
    Title,
    Genre,
    DateOfRelease
    FROM movies";

        var result = await _dbConnection.QueryAsync<Movie>(sqlQuery);
        var resultList = result.ToList();
        return resultList;
    }
}