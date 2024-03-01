using GWSales.Data.Entities.Customer;
using GWSales.Services.Models.Customer;

namespace GWSales.Data.Interfaces;

public interface ICustomerRepository
{
    Task<CustomerEntity?> CreateCustomerAsync(CreateCustomerModel model);
    Task<GetCustomersListModel?> GetAllCustomersAsync();
    Task<GetCustomerModel?> GetCustomerByIdAsync(int id);
    Task<GetCustomersListModel?> GetCustomersByType(GetCustomersByTypeModel model); 
    Task<CustomerEntity?> UpdateCustomerAsync(UpdateCustomerModel model);
    Task<GetCustomerTypeModel?> GetCustomerTypeAsync(int customerId);
    Task<CustomerTypeEntity?> CreateCustomerTypeAsync(string customerTypeName);
}
