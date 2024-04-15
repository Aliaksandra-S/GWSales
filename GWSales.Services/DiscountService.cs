using AutoMapper;
using GWSales.Data.Interfaces;
using GWSales.Services.Interfaces;
using GWSales.Services.Models;
using GWSales.Services.Models.Customer.Discount;
using GWSales.WebApi.Models.Customer.Discount;

namespace GWSales.Services;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public DiscountService(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }
    
    public async Task<CommandResult<ResultType, GetDiscountDto>> CreateDiscountAsync(CreateDiscountDto discountDto)
    {
        var result = new CommandResult<ResultType, GetDiscountDto>();

        if (discountDto.DiscountRate <= 0)
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Discount percentage can't be 0 or less than 0");

            return result;
        }

        if (discountDto.EndDate.HasValue && discountDto.EndDate < discountDto.StartDate)
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Discount end date shouldn't be earlier than the start date");

            return result;
        }

        var discountModel = _mapper.Map<CreateDiscountModel>(discountDto);
        var createResult = await _discountRepository.CreateDiscountAsync(discountModel);

        var addDiscountModel = new ApplyDiscountToCustomersModel
        {
            DiscountId = createResult.DiscountId,
            CustomerIdList = discountDto.CustomerIdList,
        };

        var addResult = await _discountRepository.ApplyDiscountToCustomersAsync(addDiscountModel);

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetDiscountDto>(createResult);

        return result;

    }

    public async Task<CommandResult<ResultType, GetDiscountDto>> GetCurrentCustomerDiscountAsync(int customerId)
    {
        var result = new CommandResult<ResultType, GetDiscountDto>();

        var getResult = await _discountRepository.GetCurrentCustomerDiscountAsync(customerId);

        if (getResult == null)
        {
            result.ResultType = ResultType.NotFound;
            result.Messages?.Add("Customer doesn't exist or customer doesn't have any discount");

            return result;
        }

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetDiscountDto>(getResult);

        return result;
    }

    public async Task<CommandResult<ResultType, GetDiscountListDto>> GetCustomerDiscountsForPeriodAsync(DiscountPeriodDto periodDto)
    {
        var result = new CommandResult<ResultType, GetDiscountListDto>();

        if (periodDto.DateFrom.HasValue && periodDto.DateTo.HasValue && periodDto.DateFrom > periodDto.DateTo)
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Discount end date shouldn't be earlier than the start date");

            return result;
        }

        if (!periodDto.DateFrom.HasValue)
        {
            periodDto.DateFrom = DateOnly.MinValue;
        }

        if (!periodDto.DateTo.HasValue)
        {
            periodDto.DateFrom = DateOnly.MaxValue;
        }

        var model = _mapper.Map<DiscountPeriodModel>(periodDto);
        var getResult = await _discountRepository.GetCustomerDiscountsForPeriodAsync(model);

        if (getResult == null)
        {
            result.ResultType = ResultType.NotFound;
            result.Messages?.Add("Customer doesn't exist");

            return result;
        }

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetDiscountListDto>(getResult);

        return result;
    }

    public async Task<CommandResult<ResultType, UpdateDiscountDto>> UpdateDiscountAsync(UpdateDiscountDto discountDto)
    {
        var result = new CommandResult<ResultType, UpdateDiscountDto>();

        if (discountDto.DiscountRate <= 0)
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Discount percentage can't be 0 or less than 0");

            return result;
        }

        if (discountDto.EndDate.HasValue && discountDto.EndDate < discountDto.StartDate)
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Discount end date shouldn't be earlier than the start date");

            return result;
        }

        var model = _mapper.Map<UpdateDiscountModel>(discountDto);
        var updateResult = await _discountRepository.UpdateDiscountAsync(model);

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<UpdateDiscountDto>(updateResult);

        return result;
    }
}
