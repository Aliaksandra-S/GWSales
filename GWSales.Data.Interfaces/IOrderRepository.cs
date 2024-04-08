using GWSales.Data.Entities.Order;
using GWSales.Services.Models.Order;

namespace GWSales.Data.Interfaces;

public interface IOrderRepository
{
    Task<GetOrderModel?> CreateOrderAsync(CreateOrderModel createModel);
    Task<GetOrderListModel> GetAllOrdersHeadersAsync();
    Task<GetOrderListModel> GetAllOrdersWithDetailsAsync();
    Task<GetOrderDetailsListModel> GetOrderDetailsByOrderIdAsync(int orderId);
    Task<GetOrderListModel> GetOrdersByProductsAsync(params int[] productIdArray);
    Task<GetOrderListModel> GetOrdersInPeriodAsync(OrderPeriodModel period);
    Task<GetOrderListModel> GetOrdersByCustomersAsync(params int[] customerIdArray);
    Task<GetOrderDetailsModel?> UpdateOrderDetailsAsync(UpdateOrderDetailsModel updateModel);
    Task<GetOrderStatusModel?> GetStatusByOrderIdAsync(int orderId);
    Task<GetOrderModel> ChangeOrderStatusAsync(ChangeOrderStatusModel statusModel);

}
