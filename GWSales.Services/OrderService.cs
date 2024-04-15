using AutoMapper;
using GWSales.Data.Interfaces;
using GWSales.Services.Interfaces;
using GWSales.Services.Models;
using GWSales.Services.Models.Order;
using GWSales.Services.Models.Product;
using GWSales.Services.Models.Storage;
using GWSales.WebApi.Models.Order;

namespace GWSales.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IProductSizeRepository _productSizeRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IDiscountRepository discountRepository,
        IProductSizeRepository productSizeRepository,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _discountRepository = discountRepository;
        _productSizeRepository = productSizeRepository;
        _productRepository = productRepository; 
        _mapper = mapper;
    }

    public async Task<CommandResult<ResultType, GetOrderDto>> CreateOrderAsync(CreateOrderDto createDto)
    {
        var result = new CommandResult<ResultType, GetOrderDto>();

        if (createDto.OrderDate < DateOnly.FromDateTime(DateTime.Now))
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Order can't be created in past");

            return result;
        }

        if (createDto.Details.Details.Any(x => x.Quantity <= 0))
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Quantity must be greater than 0");

            return result;
        }

        var customer = await _customerRepository.GetCustomerByIdAsync(createDto.CustomerId);
        if (customer == null)
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Invalid customer's ID");

            return result;
        }

        var verified = new Dictionary<int, CreateOrderDetailsDto>();

        foreach (var detail in createDto.Details.Details)
        {
            var productSize = await _productSizeRepository.GetProductSizeByProductAndSizeAsync(detail.ProductId, detail.SizeId);
            if (productSize == null)
            {
                result.Messages?.Add($"Product ID ({detail.ProductId}) or Size ID ({detail.SizeId}) doesn't exist");
                continue;
            }

            if (productSize.Quantity < detail.Quantity)
            {
                result.Messages?.Add($"Product {detail.ProductId} of size {detail.SizeId} is not enough in stock ({productSize.Quantity})");
                continue;
            }

            var changeQuantityResult = await _productSizeRepository.ChangeProductSizeQuantityAsync(new ChangeProductSizeQuantityModel
            {
                ProductSizeId = productSize.ProductSizeId,
                NewQuantity = productSize.Quantity - detail.Quantity,
            });
           
            if (changeQuantityResult == 0)
            {
                result.Messages?.Add($"Changing units of (product - {detail.ProductId}, size - {detail.SizeId}) in stock is failed");
                continue;
            }

            verified.Add(productSize.ProductSizeId, detail);
        }

        if (result.Messages?.Count > 0)
        {
            result.ResultType = ResultType.ValidationError;

            return result;
        }

        var getPricesModel = new GetPriceByCustomerTypeListModel
        {
            ProductsWithCustomerTypes = createDto.Details.Details.Select(x => new GetPriceByCustomerTypeModel
            {
                ProductId = x.ProductId,
                CustomerTypeId = customer.CustomerTypeId,
            }).ToList(),
        };

        var prices = await _productRepository.GetPricesByProductIdAsync(getPricesModel);

        var discount = await _discountRepository.GetCurrentCustomerDiscountAsync(customer.CustomerId);
        
        var discountRate = (discount != null && discount.EndDate >= createDto.OrderDate)
            ? discount.DiscountRate
            : 0m;

        var totalAmount = 0m;

        var createDetailsModel = new List<CreateOrderDetailsModel>();
        foreach (var detail in verified)
        {
            var productPrice = prices.Prices.Where(x => x.ProductId == detail.Value.ProductId).FirstOrDefault();
            if (productPrice == null)
            {
                continue;
            }    

            var subtotal = detail.Value.Quantity * productPrice.Price;
            totalAmount += subtotal;

            createDetailsModel.Add(new CreateOrderDetailsModel
            {
                ProductSizeId = detail.Key,
                Quantity = detail.Value.Quantity,
                SubtotalAmount = subtotal,
                AppliedDiscountRate = discountRate,
                Comment = detail.Value.Comment,
            });
        }

        var createOrderModel = new CreateOrderModel
        {
            CustomerId = customer.CustomerId,
            OrderDate = createDto.OrderDate,
            OrderStatus = (int)OrderStatus.Created,
            TotalAmount = totalAmount - (discountRate * 0.01m * totalAmount),
            CreatedAtUtc = DateTime.UtcNow,

            Details = new CreateOrderDetailsListModel
            {
                Details = createDetailsModel,
            }
        };
        
        var created = await _orderRepository.CreateOrderAsync(createOrderModel);

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetOrderDto>(created);

        return result;          
    }

    public async Task<CommandResult<ResultType, GetOrderListDto>> GetAllOrdersHeadersAsync()
    {
        var result = new CommandResult<ResultType, GetOrderListDto>();

        var orders = await _orderRepository.GetAllOrdersHeadersAsync();
        if (orders.Orders.Count == 0)
        {
            result.Messages?.Add("There are no orders yet");
        }

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetOrderListDto>(orders);

        return result;
    }

    public async Task<CommandResult<ResultType, GetOrderListDto>> GetAllOrdersWithDetailsAsync()
    {
        var result = new CommandResult<ResultType, GetOrderListDto>();

        var orders = await _orderRepository.GetAllOrdersWithDetailsAsync();
        if (orders.Orders.Count == 0)
        {
            result.Messages?.Add("There are no orders yet");
        }

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetOrderListDto>(orders);

        return result;
    }

    public async Task<CommandResult<ResultType, GetOrderDetailsListDto>> GetOrderDetailsByOrderIdAsync(int orderId)
    {
        var result = new CommandResult<ResultType, GetOrderDetailsListDto>();

        var details = await _orderRepository.GetOrderDetailsByOrderIdAsync(orderId);
        if (details.Details.Count == 0)
        {
            result.Messages?.Add("There are no details yet");
        }

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetOrderDetailsListDto>(details);

        return result;
    }

    public async Task<CommandResult<ResultType, GetOrderListDto>> GetOrdersByProductsAsync(params int[] productIdArray)
    {
        var result = new CommandResult<ResultType, GetOrderListDto>();

        var orders = await _orderRepository.GetOrdersByProductsAsync(productIdArray);
        if (orders.Orders.Count == 0)
        {
            result.Messages?.Add("There are no such orders");
        }

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetOrderListDto>(orders);

        return result;
    }

    public async Task<CommandResult<ResultType, GetOrderListDto>> GetOrdersInPeriodAsync(OrderPeriodDto period)
    {
        var result = new CommandResult<ResultType, GetOrderListDto>();

        if (period.DateFrom == null)
        {
            period.DateFrom = DateOnly.MinValue;
        }

        if (period.DateTo == null)
        {
            period.DateTo = DateOnly.MaxValue;
        }

        var orders = await _orderRepository.GetOrdersInPeriodAsync(_mapper.Map<OrderPeriodModel>(period));
        
        if (orders.Orders.Count == 0)
        {
            result.Messages?.Add("There are no such orders");
        }

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetOrderListDto>(orders);

        return result;
    }

    public async Task<CommandResult<ResultType, GetOrderListDto>> GetOrdersByCustomersAsync(params int[] customerIdArray)
    {
        var result = new CommandResult<ResultType, GetOrderListDto>();

        var orders = await _orderRepository.GetOrdersByCustomersAsync(customerIdArray);
        if (orders.Orders.Count == 0)
        {
            result.Messages?.Add("There are no such orders");
        }

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetOrderListDto>(orders);

        return result;
    }

    public async Task<CommandResult<ResultType, GetOrderDetailsDto>> UpdateOrderDetailsAsync(UpdateOrderDetailsDto updateDto)
    {
        var result = new CommandResult<ResultType, GetOrderDetailsDto> ();

        if (updateDto.Quantity != null && updateDto.Quantity < 0)
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Quantity can't be less than 0");

            return result;
        }

        var updated = await _orderRepository.UpdateOrderDetailsAsync(_mapper.Map<UpdateOrderDetailsModel>(updateDto));
        if (updated == null)
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add($"Order Details ID {updateDto.OrderDetailsId} isn't valid");

            return result;
        }

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetOrderDetailsDto>(updated);

        return result;
    }

    public async Task<CommandResult<ResultType, GetOrderDto>> ChangeOrderStatusAsync(ChangeOrderStatusDto statusDto)
    {
        var result = new CommandResult<ResultType, GetOrderDto>();

        var currentStatus = await _orderRepository.GetStatusByOrderIdAsync(statusDto.OrderId);

        if (currentStatus == null)
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Order ID is not valid");

            return result;
        }

        var possibleStatuses = OrderStatusChangeMap.ChangeMap[currentStatus.OrderStatus]; 
        
        if (!possibleStatuses.Contains(statusDto.OrderStatus))
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add($"Order status can't be changed from {currentStatus.OrderStatus} to {statusDto.OrderStatus}");

            return result;
        }

        var orderModel = await _orderRepository.ChangeOrderStatusAsync(_mapper.Map<ChangeOrderStatusModel>(statusDto));
        
        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetOrderDto>(orderModel);

        return result;
    }
}
