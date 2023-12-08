using System.Data;
using Dapper;
using OutputCachingInMemory.Models;

namespace OutputCachingInMemory.Repositories;

public interface ICustomerRepository
{
    public Task<List<Customer>> GetAllAsync();
    public Task<Customer?> GetAsync(Guid id);
    public Task CreateAsync(Customer customer);
    public Task UpdateAsync(Customer customer);
    public Task DeleteAsync(Guid id);
}
public class CustomerRepository(IDbConnection dbConnection) : ICustomerRepository
{
    private readonly IDbConnection _dbConnection = dbConnection;
    
    public async Task<List<Customer>> GetAllAsync()
    {
        var sqlQuery = @"
SELECT 
c.Id, 
c.FirstName,
c.LastName,
c.Email,
c.DateOfBirth
FROM Customer AS c";

        var customers = await _dbConnection.QueryAsync<Customer>(sqlQuery);
        var customersList = customers.ToList();
        return customersList;
    }

    public async Task<Customer?> GetAsync(Guid id)
    {
        var sqlQuery = @"
SELECT 
c.Id, 
c.FirstName,
c.LastName,
c.Email,
c.DateOfBirth
FROM Customer AS c
WHERE c.Id = @id";

        var customer = await _dbConnection.QueryFirstOrDefaultAsync<Customer>(sqlQuery, new
        {
            id
        });
        return customer;
    }

    public async Task CreateAsync(Customer customer)
    {
        customer.Id = Guid.NewGuid();
        var sqlQuery = @"
INSERT INTO Customer (Id, FirstName, LastName, Email, DateOfBirth)
VALUES (@Id, @FirstName, @LastName, @Email, @DateOfBirth)";

        await _dbConnection.ExecuteAsync(sqlQuery, customer);
    }

    public async Task UpdateAsync(Customer customer)
    {
        var sqlQuery = @"
UPDATE Customer SET
FirstName = @firstName,
LastName = @lastName,
Email = @email,
DateOfBirth = @dateOfBirth
WHERE Id = @Id";

        await _dbConnection.ExecuteAsync(sqlQuery, customer);
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var sqlQuery = @"
DELETE FROM Customer
WHERE Id = @id";

        await _dbConnection.ExecuteAsync(sqlQuery, new { id });
    }
}