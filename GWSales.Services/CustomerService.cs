using AutoMapper;
using GWSales.Data.Interfaces;
using GWSales.Services.Interfaces;
using GWSales.Services.Models;
using GWSales.Services.Models.Customer;
using GWSales.WebApi.Models.Customer;

namespace GWSales.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }
    public async Task<CommandResult<ResultType, GetCustomerDto>> CreateCustomerAsync(CreateCustomerDto customerDto)
    {
        var result = new CommandResult<ResultType, GetCustomerDto>();
        if (string.IsNullOrWhiteSpace(customerDto.Name))
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Name can't be null");

            return result;
        }

        var typeEntity = await _customerRepository.FindCustomerTypeAsync(customerDto.Type);

        if (typeEntity == null)
        {
            result.ResultType = ResultType.NotFound;
            result.Messages?.Add("Customer type doesn't exist");

            return result;
        }

        var createModel = new CreateCustomerModel
        {
            Name = customerDto.Name,
            CustomerTypeId = typeEntity.CustomerTypeId,
        };

        var createResult = await _customerRepository.CreateCustomerAsync(createModel);

        result.ResultType = ResultType.Success;
        result.Value = new GetCustomerDto
        {
            CustomerId = createResult.CustomerId,
            Name = createResult.Name,
            CustomerTypeId = typeEntity.CustomerTypeId,
            TypeName = typeEntity.TypeName,
        };

        return result;

    }

    public async Task<CommandResult<ResultType, GetCustomerTypeDto>> CreateCustomerTypeAsync(CreateCustomerTypeDto customerTypeDto)
    {
        var result = new CommandResult<ResultType, GetCustomerTypeDto>();

        if(string.IsNullOrWhiteSpace(customerTypeDto.TypeName))
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Name can't be null");

            return result;
        }

        var createResult = await _customerRepository.CreateCustomerTypeAsync(customerTypeDto.TypeName);

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetCustomerTypeDto>(createResult);

        return result;
    }

    public async Task<CommandResult<ResultType, GetCustomerListDto>> GetAllCustomersAsync()
    {
        var result = new CommandResult<ResultType, GetCustomerListDto>();

        var getResult = await _customerRepository.GetAllCustomersAsync();
        
        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetCustomerListDto>(getResult);

        return result;

    }

    public async Task<CommandResult<ResultType, GetCustomerDto>> GetCustomerByIdAsync(int customerId)
    {
        var result = new CommandResult<ResultType, GetCustomerDto>();

        var getResult = await _customerRepository.GetCustomerByIdAsync(customerId);
        if (getResult == null)
        {
            result.ResultType = ResultType.NotFound;
            result.Messages?.Add("Customer doesn't exist");

            return result;
        }

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetCustomerDto>(getResult);

        return result;
    }

    public async Task<CommandResult<ResultType, GetCustomerListDto>> GetCustomersByTypeAsync(GetCustomerByTypeDto typeDto)
    {
        var result = new CommandResult<ResultType, GetCustomerListDto>();

        var typeEntity = await _customerRepository.FindCustomerTypeAsync(typeDto.TypeName);
        if (typeEntity == null)
        {
            result.ResultType = ResultType.NotFound;
            result.Messages?.Add("Customer Type doesn't exist");

            return result;
        }

        var typeModel = _mapper.Map<GetCustomersByTypeModel>(typeDto);
        var getResult = await _customerRepository.GetCustomersByTypeAsync(typeModel);

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetCustomerListDto>(getResult);

        return result;

    }

    public async Task<CommandResult<ResultType, UpdateCustomerDto>> UpdateCustomerAsync(UpdateCustomerDto customerDto)
    {
        var result = new CommandResult<ResultType, UpdateCustomerDto>();

        if (string.IsNullOrWhiteSpace(customerDto.Name))
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Name can't be null");

            return result;
        }

        var customerModel = _mapper.Map<UpdateCustomerModel>(customerDto);
        var updateResult = await _customerRepository.UpdateCustomerAsync(customerModel);

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<UpdateCustomerDto>(updateResult);

        return result;
    }
}
