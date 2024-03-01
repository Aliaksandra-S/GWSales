using GWSales.Data.Entities.Customer;
using GWSales.Data.Interfaces;
using GWSales.Services.Models.Customer;
using Microsoft.EntityFrameworkCore;

namespace GWSales.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly SalesDbContext _context;
    public CustomerRepository(SalesDbContext context)
    {
        _context = context;
    }
    public async Task<CustomerEntity?> CreateCustomerAsync(CreateCustomerModel model)
    {
        var entity = new CustomerEntity
        {
            Name = model.Name,
            CustomerTypeId = model.CustomerTypeId,
        };

        var resultEntity = await _context.Customers.AddAsync(entity);
        await _context.SaveChangesAsync();
        return resultEntity.Entity;
    }

    public async Task<CustomerTypeEntity?> CreateCustomerTypeAsync(string customerTypeName)
    {
        var entity = new CustomerTypeEntity
        {
            TypeName = customerTypeName,
        };

        var resultEntity = await _context.CustomerTypes.AddAsync(entity);
        await _context.SaveChangesAsync();
        return resultEntity.Entity;
    }

    public async Task<GetCustomersListModel?> GetAllCustomersAsync()
    {
        var customersWithTypes = await _context.Customers
            .Join(_context.CustomerTypes,
            outer => outer.CustomerTypeId,
            inner => inner.CustomerTypeId,
            (outer, inner) => new GetCustomerModel
            {
                CustomerId = outer.CustomerId,
                Name = outer.Name,
                CustomerTypeId = inner.CustomerTypeId,
                TypeName = inner.TypeName,
            }).ToArrayAsync();

        if (customersWithTypes == null)
        {
            return null;
        }

        return new GetCustomersListModel
        {
            Customers = customersWithTypes,
        };
    }

    public async Task<GetCustomerModel?> GetCustomerByIdAsync(int id)
    {
        return await _context.Customers.Where(x => x.CustomerId == id)
            .Join(_context.CustomerTypes,
            outer => outer.CustomerTypeId,
            inner => inner.CustomerTypeId,
            (outer, inner) => new GetCustomerModel
            {
                CustomerId = outer.CustomerId,
                Name = outer.Name,
                CustomerTypeId = inner.CustomerTypeId,
                TypeName = inner.TypeName,
            }).FirstOrDefaultAsync();
    }

    public async Task<GetCustomersListModel?> GetCustomersByType(GetCustomersByTypeModel model)
    {
        var customers =  await _context.CustomerTypes.Where(x => x.TypeName == model.CustomerTypeName)
            .Join(_context.Customers,
            outer => outer.CustomerTypeId,
            inner => inner.CustomerTypeId,
            (outer, inner) => new GetCustomerModel
            {
                CustomerId = inner.CustomerId,
                Name = inner.Name,
                CustomerTypeId = outer.CustomerTypeId,
                TypeName = outer.TypeName,
            }).ToArrayAsync();

        if (customers == null)
        {
            return null;
        }

        return new GetCustomersListModel
        {
            Customers = customers,
        };
    }

    public async Task<GetCustomerTypeModel?> GetCustomerTypeAsync(int customerId)
    {
        var customerEntity = await _context.Customers.FindAsync(customerId);
        if (customerEntity == null)
        {
            return null;
        }

        var typeEntity = customerEntity?.CustomerType;
        if(typeEntity == null)
        {
            return null;
        }

        return new GetCustomerTypeModel
        {
            CustomerTypeId = typeEntity.CustomerTypeId,
            TypeName = typeEntity.TypeName,
        };
    }

    public async Task<CustomerEntity?> UpdateCustomerAsync(UpdateCustomerModel model)
    {
        var entity = await _context.Customers.FindAsync(model.CustomerId);
        if (entity == null)
        {
            return null;
        }

        _context.Customers.Attach(entity);

        entity.Name = model.Name;

        await _context.SaveChangesAsync();

        return entity;
    }
}
