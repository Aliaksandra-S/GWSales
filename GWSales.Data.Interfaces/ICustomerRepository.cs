using GWSales.Data.Entities.Customer;
using GWSales.Services.Models.Customer;

namespace GWSales.Data.Interfaces;

public interface ICustomerRepository
{
    Task<CustomerEntity?> CreateCustomerAsync(CreateCustomerModel model);
    Task<GetCustomerListModel> GetAllCustomersAsync();
    Task<GetCustomerModel?> GetCustomerByIdAsync(int id);
    Task<GetCustomerListModel?> GetCustomersByTypeAsync(GetCustomersByTypeModel model); 
    Task<CustomerEntity?> UpdateCustomerAsync(UpdateCustomerModel model);
    Task<GetCustomerTypeModel?> GetCustomerTypeByCustomerIdAsync(int customerId);
    Task<CustomerTypeEntity?> CreateCustomerTypeAsync(string customerTypeName);
    Task<CustomerTypeEntity?> FindCustomerTypeAsync(string typeName);
}
