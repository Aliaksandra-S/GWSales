using GWSales.Services.Models;
using GWSales.Services.Models.Order;
using GWSales.WebApi.Models.Order;

namespace GWSales.Services.Interfaces;

public interface IOrderService
{
    Task<CommandResult<ResultType, GetOrderDto>> CreateOrderAsync(CreateOrderDto createDto);
    Task<CommandResult<ResultType, GetOrderListDto>> GetAllOrdersHeadersAsync();
    Task<CommandResult<ResultType, GetOrderListDto>> GetAllOrdersWithDetailsAsync();
    Task<CommandResult<ResultType, GetOrderDetailsListDto>> GetOrderDetailsByOrderIdAsync(int orderId);
    Task<CommandResult<ResultType, GetOrderListDto>> GetOrdersByProductsAsync(params int[] productIdArray);
    Task<CommandResult<ResultType, GetOrderListDto>> GetOrdersInPeriodAsync(OrderPeriodDto period);
    Task<CommandResult<ResultType, GetOrderListDto>> GetOrdersByCustomersAsync(params int[] customerIdArray);
    Task<CommandResult<ResultType, GetOrderDetailsDto>> UpdateOrderDetailsAsync(UpdateOrderDetailsDto updateDto);
    ////Task<GetOrderStatusModel> GetStatusByOrderIdAsync(int orderId);
    Task<CommandResult<ResultType, GetOrderDto>> ChangeOrderStatusAsync(ChangeOrderStatusDto statusModel);
}
