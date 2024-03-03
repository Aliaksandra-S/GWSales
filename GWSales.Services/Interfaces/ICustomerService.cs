using GWSales.Services.Models;
using GWSales.WebApi.Models.Customer;

namespace GWSales.Services.Interfaces;

public interface ICustomerService
{
    Task<CommandResult<ResultType, GetCustomerDto>> CreateCustomerAsync(CreateCustomerDto customerDto);
    Task<CommandResult<ResultType, GetCustomerListDto>> GetAllCustomersAsync();
    Task<CommandResult<ResultType, GetCustomerListDto>> GetCustomersByTypeAsync(GetCustomerByTypeDto typeDto); 
    Task<CommandResult<ResultType, GetCustomerDto>> GetCustomerByIdAsync(int customerId);
    Task<CommandResult<ResultType, UpdateCustomerDto>> UpdateCustomerAsync(UpdateCustomerDto customerDto);
    Task<CommandResult<ResultType, GetCustomerTypeDto>> CreateCustomerTypeAsync(CreateCustomerTypeDto customerTypeDto); 
}
