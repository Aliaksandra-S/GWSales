using GWSales.Data.Entities.Customer;
using GWSales.Services.Models.Customer.Discount;

namespace GWSales.Data.Interfaces;

public interface ICustomerDiscountRepository
{
    Task<CustomerDiscountEntity?> CreateDiscountAsync(CreateDiscountModel discountModel);
    Task<GetDiscountModel?> GetCurrentCustomerDiscountAsync(int customerId);
    Task<GetDiscountListModel?> GetCustomerDiscountsForPeriodAsync(DiscountPeriodModel period);
    Task<CustomerDiscountEntity?> UpdateDiscountAsync(UpdateDiscountModel discountModel);
    Task<GetDiscountWithCustomersModel?> ApplyDiscountToCustomersAsync(ApplyDiscountToCustomersModel model);
}
