using GWSales.Data.Entities.Order;
using GWSales.Data.Interfaces;
using GWSales.Services.Models.Order;
using Microsoft.EntityFrameworkCore;

namespace GWSales.Data.Npgsql.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly SalesDbContext _context;

    public OrderRepository(SalesDbContext context)
    {
        _context = context;
    }
    public async Task<GetOrderModel?> CreateOrderAsync(CreateOrderModel createModel)
    {
        var orderEntity = await _context.Customers
            .Where(x => x.CustomerId == createModel.CustomerId)
            .Join(_context.CustomerTypes,
            outer => outer.CustomerTypeId,
            inner => inner.CustomerTypeId,
            (outer, inner) => new OrderEntity
            {
                CustomerId = outer.CustomerId,
                CustomerTypeId = inner.CustomerTypeId,
                OrderDate = createModel.OrderDate,
                OrderStatus = createModel.OrderStatus,
                TotalAmount = createModel.TotalAmount,
                CreatedAtUtc = createModel.CreatedAtUtc,

                Customer = outer,
                CustomerType = inner,
            }).FirstOrDefaultAsync();

        if (orderEntity == null)
        {
            return null;
        }

        var createdOrder = await _context.Orders.AddAsync(orderEntity);

        var detailsArray = createModel.Details.Details.Select(x => new OrderDetailsEntity
        {
            OrderId = createdOrder.Entity.OrderId,
            ProductSizeId = x.ProductSizeId,
            Quantity = x.Quantity,
            SubtotalAmount = x.SubtotalAmount,
            AppliedDiscountRate = x.AppliedDiscountRate,
            Comment = x.Comment,

            Order = createdOrder.Entity,
        }).ToArray();

        await _context.OrderDetails.AddRangeAsync(detailsArray);

        await _context.SaveChangesAsync();

        return new GetOrderModel
        {
            OrderId = createdOrder.Entity.OrderId,
            CustomerId = createdOrder.Entity.CustomerId,
            OrderDate = createdOrder.Entity.OrderDate,
            OrderStatus = createdOrder.Entity.OrderStatus,
            CreatedAtUtc = createdOrder.Entity.CreatedAtUtc,

            Details = await GetOrderDetailsByOrderIdAsync(createdOrder.Entity.OrderId),
        };
    }

    public async Task<GetOrderListModel> GetAllOrdersHeadersAsync()
    {
        var orderList = await _context.Orders.Select(x => new GetOrderModel
        {
            OrderId = x.OrderId,
            CustomerId = x.CustomerId,
            OrderDate = x.OrderDate,
            OrderStatus = x.OrderStatus,
            TotalAmount = x.TotalAmount,
            CreatedAtUtc = x.CreatedAtUtc,
        }).ToListAsync();

        return new GetOrderListModel
        {
            Orders = orderList,
        };
    }

    public async Task<GetOrderListModel> GetAllOrdersWithDetailsAsync()
    {
        var ordersWithDetails = await _context.Orders
            .Include(order => order.OrderDetails)
            .Select(order => new GetOrderModel
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderStatus = order.OrderStatus,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                CreatedAtUtc = order.CreatedAtUtc,
                Details = new GetOrderDetailsListModel
                {
                    Details = order.OrderDetails.Select(detail => new GetOrderDetailsModel
                    {
                        OrderDetailsId = detail.OrderId,
                        ProductSizeId = detail.ProductSizeId,
                        Quantity = detail.Quantity,
                        SubtotalAmount = detail.SubtotalAmount,
                        AppliedDiscountRate = detail.AppliedDiscountRate,
                        Comment = detail.Comment,
                    }).ToList(),
                }
            }).ToListAsync();

        return new GetOrderListModel
        {
            Orders = ordersWithDetails,
        };
    }

    public async Task<GetOrderDetailsListModel> GetOrderDetailsByOrderIdAsync(int orderId)
    {
        var detailsList = await _context.OrderDetails
            .Where(x => x.OrderId == orderId)
            .Select(x => new GetOrderDetailsModel
            {
                OrderDetailsId = x.OrderDetailsId,
                OrderId = x.OrderId,
                ProductSizeId = x.ProductSizeId,
                Quantity = x.Quantity,
                SubtotalAmount = x.SubtotalAmount,
                AppliedDiscountRate = x.AppliedDiscountRate,
                Comment = x.Comment,
            }).ToListAsync();

        return new GetOrderDetailsListModel
        {
            Details = detailsList,
        };
    }

    public async Task<GetOrderListModel> GetOrdersByProductsAsync(params int[] productIdArray)
    {
        var ordersWithDetails = await _context.Orders
            .Include(order => order.OrderDetails)
            .ThenInclude(orderDetail => orderDetail.ProductSize)
            .Where(order => order.OrderDetails.Any(orderDetail => productIdArray.Contains(orderDetail.ProductSize.ProductId)))
            .Select(order => new GetOrderModel
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderStatus = order.OrderStatus,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                CreatedAtUtc = order.CreatedAtUtc,
                Details = new GetOrderDetailsListModel
                {
                    Details = order.OrderDetails.Select(detail => new GetOrderDetailsModel
                    {
                        OrderDetailsId = detail.OrderId,
                        ProductSizeId = detail.ProductSizeId,
                        Quantity = detail.Quantity,
                        SubtotalAmount = detail.SubtotalAmount,
                        AppliedDiscountRate = detail.AppliedDiscountRate,
                        Comment = detail.Comment,
                    }).ToList(),
                }
            }).ToListAsync();

        return new GetOrderListModel
        {
            Orders = ordersWithDetails,
        };
    }

    public async Task<GetOrderListModel> GetOrdersInPeriodAsync(OrderPeriodModel period)
    {
        var ordersWithDetails = await _context.Orders
            .Where(order => order.OrderDate >= period.DateFrom && order.OrderDate <= period.DateTo)
            .Include(order => order.OrderDetails)
            .Select(order => new GetOrderModel
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderStatus = order.OrderStatus,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                CreatedAtUtc = order.CreatedAtUtc,
                Details = new GetOrderDetailsListModel
                {
                    Details = order.OrderDetails.Select(detail => new GetOrderDetailsModel
                    {
                        OrderDetailsId = detail.OrderId,
                        ProductSizeId = detail.ProductSizeId,
                        Quantity = detail.Quantity,
                        SubtotalAmount = detail.SubtotalAmount,
                        AppliedDiscountRate = detail.AppliedDiscountRate,
                        Comment = detail.Comment,
                    }).ToList(),
                }
            }).ToListAsync();

        return new GetOrderListModel
        {
            Orders = ordersWithDetails,
        };
    }

    public async Task<GetOrderListModel> GetOrdersByCustomersAsync(params int[] customerIdArray)
    {
        var ordersWithDetails = await _context.Orders
            .Where(order => customerIdArray.Contains(order.CustomerId))
            .Include(order => order.OrderDetails)
            .Select(order => new GetOrderModel
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderStatus = order.OrderStatus,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                CreatedAtUtc = order.CreatedAtUtc,
                Details = new GetOrderDetailsListModel
                {
                    Details = order.OrderDetails.Select(detail => new GetOrderDetailsModel
                    {
                        OrderDetailsId = detail.OrderId,
                        ProductSizeId = detail.ProductSizeId,
                        Quantity = detail.Quantity,
                        SubtotalAmount = detail.SubtotalAmount,
                        AppliedDiscountRate = detail.AppliedDiscountRate,
                        Comment = detail.Comment,
                    }).ToList(),
                }
            }).ToListAsync();

        return new GetOrderListModel
        {
            Orders = ordersWithDetails,
        };
    }

    public async Task<GetOrderDetailsModel?> UpdateOrderDetailsAsync(UpdateOrderDetailsModel updateModel)
    {
        var entity = await _context.OrderDetails.FindAsync(updateModel.OrderDetailsId);
        if (entity == null)
        {
            return null;
        }

        _context.OrderDetails.Attach(entity);

        if (updateModel.Quantity != null)
        {
            entity.Quantity = (int)updateModel.Quantity;
        }

        if (updateModel.Comment != null)
        {
            entity.Comment = updateModel.Comment;
        }

        await _context.SaveChangesAsync();

        return new GetOrderDetailsModel
        {
            OrderDetailsId = entity.OrderDetailsId,
            OrderId = entity.OrderId,
            ProductSizeId = entity.ProductSizeId,
            Quantity = entity.Quantity,
            SubtotalAmount = entity.SubtotalAmount,
            AppliedDiscountRate = entity.AppliedDiscountRate,
            Comment = entity.Comment,
        };
    }

    public async Task<GetOrderModel> ChangeOrderStatusAsync(ChangeOrderStatusModel statusModel)
    {
        var entity = await _context.Orders.FindAsync(statusModel.OrderId);

        _context.Orders.Attach(entity);

        entity.OrderStatus = statusModel.OrderStatus;

        await _context.SaveChangesAsync();

        return new GetOrderModel
        {
            OrderId = entity.OrderId,
            CustomerId = entity.CustomerId,
            OrderDate = entity.OrderDate,
            OrderStatus = entity.OrderStatus,
            CreatedAtUtc = DateTime.UtcNow,
        };
    }

    public async Task<GetOrderStatusModel?> GetStatusByOrderIdAsync(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);

        return order is null ? null : new GetOrderStatusModel
        {
            OrderId = order.OrderId,
            OrderStatus = order.OrderStatus,
        };
    }
}
