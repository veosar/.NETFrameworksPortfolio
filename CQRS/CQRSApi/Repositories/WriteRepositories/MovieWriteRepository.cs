using System.Data;
using CQRSApi.Models;
using Dapper;

namespace CQRSApi.Repositories.WriteRepositories;

public interface IMovieWriteRepository
{
    public Task CreateAsync(Movie movie);
    public Task UpdateAsync(Movie movie);
    public Task DeleteAsync(Guid id);
}

public class MovieWriteRepository : IMovieWriteRepository
{
    private readonly IDbConnection _dbConnection;

    public MovieWriteRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task CreateAsync(Movie movie)
    {
        var sqlQuery = @"
INSERT INTO Movies (Id, Title, Genre, DateOfRelease) VALUES (@Id, @Title, @Genre, @DateOfRelease)";

        await _dbConnection.ExecuteAsync(sqlQuery, movie);
    }

    public async Task UpdateAsync(Movie movie)
    {
        var sqlQuery = @"
UPDATE Movies set Title = @Title, Genre = @Genre, DateOfRelease = @DateOfRelease where Id = @Id
";
        await _dbConnection.ExecuteAsync(sqlQuery, movie);
    }

    public async Task DeleteAsync(Guid id)
    {
        var sqlQuery = @"
DELETE FROM Movies where Id = @Id
";
        await _dbConnection.ExecuteAsync(sqlQuery, new {Id = id});
    }
}