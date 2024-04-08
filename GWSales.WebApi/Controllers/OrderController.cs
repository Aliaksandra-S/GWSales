using GWSales.Services.Interfaces;
using GWSales.Services.Models;
using GWSales.WebApi.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GWSales.WebApi.Controllers;

[Authorize(Roles = "Admin,User")]
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [Route("CreateOrder")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createDto)
    {
        var result = await _orderService.CreateOrderAsync(createDto);

        if (result.ResultType == ResultType.ValidationError)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("GetAllHeaders")]
    public async Task<IActionResult> GetAllOrdersHeaders()
    {
        var result = await _orderService.GetAllOrdersHeadersAsync();

        return Ok(result);
    }

    [HttpPost]
    [Route("GetAllWithDetails")]
    public async Task<IActionResult> GetAllOrdersWithDetails()
    {
        var result = await _orderService.GetAllOrdersWithDetailsAsync();

        return Ok(result);
    }

    [HttpPost]
    [Route("GetDetailsByOrderId")]
    public async Task<IActionResult> GetOrderDetailsById([FromBody] int orderId)
    {
        var result = await _orderService.GetOrderDetailsByOrderIdAsync(orderId);

        return Ok(result);
    }

    [HttpPost]
    [Route("GetOrdersByProducts")]
    public async Task<IActionResult> GetOrdersByProducts([FromBody] int[] productIds)
    {
        var result = await _orderService.GetOrdersByProductsAsync(productIds);

        return Ok(result);
    }

    [HttpPost]
    [Route("GetOrdersInPeriod")]
    public async Task<IActionResult> GetOrdersInPeriod([FromBody] OrderPeriodDto periodDto)
    {
        var result = await _orderService.GetOrdersInPeriodAsync(periodDto);

        return Ok(result);
    }

    [HttpPost]
    [Route("GetOrdersByCustomers")]
    public async Task<IActionResult> GetOrdersByCustomers([FromBody] int[] customerIds)
    {
        var result = await _orderService.GetOrdersByCustomersAsync(customerIds);

        return Ok(result);
    }

    [HttpPost]
    [Route("UpdateOrderDetails")]
    public async Task<IActionResult> UpdateOrderDetails([FromBody] UpdateOrderDetailsDto updateDto)
    {
        var result = await _orderService.UpdateOrderDetailsAsync(updateDto);

        if (result.ResultType == ResultType.ValidationError)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("ChangeOrderStatus")]
    public async Task<IActionResult> ChangeOrderStatus([FromBody] ChangeOrderStatusDto statusDto)
    {
        var result = await _orderService.ChangeOrderStatusAsync(statusDto);

        if (result.ResultType == ResultType.ValidationError)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
