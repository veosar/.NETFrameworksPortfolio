using AN.Domain.Entities.Customers;
using AN.Domain.Entities.Shared;
using AN.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AN.Infrastructure.Persistence.Repositories;

internal sealed class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }               

    public async Task<Customer?> GetByIdAsync(CustomerId id)
    {
        var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
        return customer;
    }

    public void Add(Customer customer)
    {
        _context.Customers.Add(customer);
    }

    public async Task<bool> IsEmailUniqueAsync(Email email)
    {
        return !await _context.Customers.AnyAsync(c => c.Email == email);
    }

    public void Update(Customer customer)
    {
        // This is not necessary since EF is tracking our changes by default
        // _context.Customers.Update(customer);
    }

    public void Delete(Customer customer)
    {
        _context.Customers.Remove(customer);
    }
}