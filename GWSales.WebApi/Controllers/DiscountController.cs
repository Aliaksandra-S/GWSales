using GWSales.Services.Interfaces;
using GWSales.Services.Models;
using GWSales.WebApi.Models.Customer.Discount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GWSales.WebApi.Controllers;

[Authorize(Roles = "Admin,User")]
[ApiController]
[Route("api/[controller]")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService _discountService;

    public DiscountController(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    [HttpPost]
    [Route("CreateDiscount")]
    public async Task<IActionResult> CreateDiscount([FromBody] CreateDiscountDto discountDto)
    {
        var result = await _discountService.CreateDiscountAsync(discountDto);

        if (result.ResultType == ResultType.ValidationError)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("GetCustomerDiscount")]
    public async Task<IActionResult> GetCurrentCustomerDiscount([FromBody] int customerId)
    {
        var result = await _discountService.GetCurrentCustomerDiscountAsync(customerId);

        if (result.ResultType == ResultType.NotFound)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("GetDiscountForPeriod")]
    public async Task<IActionResult> GetCustomerDiscountsForPeriod([FromBody] DiscountPeriodDto periodDto)
    {
        var result = await _discountService.GetCustomerDiscountsForPeriodAsync(periodDto);

        if (result.ResultType == ResultType.ValidationError || result.ResultType == ResultType.NotFound)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("UpdateDiscount")]
    public async Task<IActionResult> UpdateDiscount([FromBody] UpdateDiscountDto discountDto)
    {
        var result = await _discountService.UpdateDiscountAsync(discountDto);

        if (result.ResultType == ResultType.ValidationError || result.ResultType == ResultType.NotFound)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
