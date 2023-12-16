using CustomersApi.Messaging;
using CustomersApi.Models;
using CustomersApi.Repositories;
using CustomersContracts.Messages;

namespace CustomersApi.Services;

public interface ICustomerService
{
    public Task<List<Customer>> GetAllAsync();
    public Task<Customer?> GetAsync(Guid id);
    public Task CreateAsync(Customer customer);
    public Task<Customer?> UpdateAsync(Customer customer);
    public Task DeleteAsync(Guid id);
}

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ISnsMessenger _snsMessenger;

    public CustomerService(ICustomerRepository customerRepository, ISnsMessenger snsMessenger)
    {
        _customerRepository = customerRepository;
        _snsMessenger = snsMessenger;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return customers;
    }

    public async Task<Customer?> GetAsync(Guid id)
    {
        var customer = await _customerRepository.GetAsync(id);
        return customer;
    }

    public async Task CreateAsync(Customer customer)
    {
        await _customerRepository.CreateAsync(customer);
        var customerCreated = new CustomerCreated
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            TotalMoneySpent = customer.TotalMoneySpent
        };
        await _snsMessenger.PublishMessageAsync(customerCreated);
    }

    public async Task<Customer?> UpdateAsync(Customer customer)
    {
        var existingCustomer = await _customerRepository.GetAsync(customer.Id);
        if (existingCustomer is null)
        {
            return null;
        }
        var oldTotalMoneySpent = existingCustomer.TotalMoneySpent;
        await _customerRepository.UpdateAsync(customer);
        var customerUpdated = new CustomerUpdated
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            OldTotalMoneySpent = oldTotalMoneySpent,
            NewTotalMoneySpent = customer.TotalMoneySpent
        };
        await _snsMessenger.PublishMessageAsync(customerUpdated);
        return customer;
    }

    public async Task DeleteAsync(Guid id)
    {
        var customer = await _customerRepository.GetAsync(id);
        await _customerRepository.DeleteAsync(id);
        var customerDeleted = new CustomerDeleted
        {
            Id = id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email
        };
        await _snsMessenger.PublishMessageAsync(customerDeleted);
    }
}