using GWSales.Services.Interfaces;
using GWSales.Services.Models;
using GWSales.WebApi.Models.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GWSales.WebApi.Controllers;

[Authorize(Roles = "Admin,User")]
[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    [Route("CreateCustomer")]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto customerDto)
    {
        var result = await _customerService.CreateCustomerAsync(customerDto);

        if (result.ResultType == ResultType.ValidationError || result.ResultType == ResultType.NotFound)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("CreateType")]
    public async Task<IActionResult> CreateCustomerType([FromBody] CreateCustomerTypeDto typeDto)
    {
        var result = await _customerService.CreateCustomerTypeAsync(typeDto);

        if (result.ResultType == ResultType.ValidationError)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("UpdateCustomer")]
    public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerDto customerDto)
    {
        var result = await _customerService.UpdateCustomerAsync(customerDto);

        if (result.ResultType == ResultType.ValidationError)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("GetAllCustomers")]
    public async Task<IActionResult> GetAllCustomers()
    {
        var result = await _customerService.GetAllCustomersAsync();

        return Ok(result);
    }

    [HttpPost]
    [Route("GetCustomerById")]
    public async Task<IActionResult> GetCustomerById([FromBody] int customerId)
    {
        var result = await _customerService.GetCustomerByIdAsync(customerId);

        if (result.ResultType == ResultType.NotFound)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("GetCustomersByType")]
    public async Task<IActionResult> GetCustomersByType([FromBody] GetCustomerByTypeDto typeDto)
    {
        var result = await _customerService.GetCustomersByTypeAsync(typeDto);

        if(result.ResultType == ResultType.NotFound)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
