using AN.Domain.Entities.Customers;
using AN.Domain.Entities.Shared;

namespace AN.Domain.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(CustomerId id);
    void Add(Customer customer);
    Task<bool> IsEmailUniqueAsync(Email email);
    void Update(Customer customer);
    void Delete(Customer customer);
}