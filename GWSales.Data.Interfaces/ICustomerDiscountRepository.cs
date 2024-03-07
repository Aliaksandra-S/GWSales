using GWSales.Data.Entities.Customer;
using GWSales.Services.Models.Customer.Discount;

namespace GWSales.Data.Interfaces;

public interface ICustomerDiscountRepository
{
    Task<DiscountProgramEntity?> CreateDiscountAsync(CreateDiscountModel discountModel);
    Task<GetDiscountModel?> GetCurrentCustomerDiscountAsync(int customerId);
    Task<GetDiscountListModel?> GetCustomerDiscountsForPeriodAsync(DiscountPeriodModel period);
    Task<DiscountProgramEntity?> UpdateDiscountAsync(UpdateDiscountModel discountModel);
    Task<GetDiscountWithCustomersModel?> ApplyDiscountToCustomersAsync(ApplyDiscountToCustomersModel model);
}
