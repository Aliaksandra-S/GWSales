using GWSales.Services.Models;
using GWSales.WebApi.Models.Customer.Discount;

namespace GWSales.Services.Interfaces;

public interface IDiscountService
{
    Task<CommandResult<ResultType, GetDiscountDto>> CreateDiscountAsync(CreateDiscountDto discountDto);
    Task<CommandResult<ResultType, GetDiscountDto>> GetCurrentCustomerDiscountAsync(int customerId);
    Task<CommandResult<ResultType, GetDiscountListDto>> GetCustomerDiscountsForPeriodAsync(DiscountPeriodDto periodDto);
    Task<CommandResult<ResultType, UpdateDiscountDto>> UpdateDiscountAsync(UpdateDiscountDto discountDto);
}
