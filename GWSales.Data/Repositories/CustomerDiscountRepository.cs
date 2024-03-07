using GWSales.Data.Entities.Customer;
using GWSales.Data.Interfaces;
using GWSales.Services.Models.Customer;
using GWSales.Services.Models.Customer.Discount;
using Microsoft.EntityFrameworkCore;

namespace GWSales.Data.Repositories;

public class CustomerDiscountRepository : ICustomerDiscountRepository
{
    private readonly SalesDbContext _context;

    public CustomerDiscountRepository(SalesDbContext context)
    {
        _context = context;
    }

    
    public async Task<DiscountProgramEntity?> CreateDiscountAsync(CreateDiscountModel discountModel)
    {
        var entity = new DiscountProgramEntity
        {
            DiscountRate = discountModel.DiscountRate,
            StartDate = discountModel.StartDate,
            EndDate = discountModel.EndDate,
            Comment = discountModel.Comment ?? "",
        };

        var resultEntity = await _context.CustomerDiscounts.AddAsync(entity);
        await _context.SaveChangesAsync();
        return resultEntity.Entity;
    }

    public async Task<GetDiscountWithCustomersModel?> ApplyDiscountToCustomersAsync(ApplyDiscountToCustomersModel model)
    { 
        var discount = await _context.CustomerDiscounts.FindAsync(model.DiscountId);
        if (discount == null)
        {
            return null;
        }

        foreach (var customerId in model.CustomerIdList)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
            {
                continue;
            }

            customer.DiscountId = discount.DiscountId;
            customer.Discount = discount;
            discount.Customers?.Add(customer);
        }


        await _context.SaveChangesAsync();

        var customers = discount.Customers == null
            ? null
            : discount.Customers.Select(x => new GetCustomerModel
            {
                CustomerId = x.CustomerId,
                Name = x.Name,
                //CustomerTypeId = x.CustomerTypeId,
                //TypeName = x.CustomerType.TypeName,
            }).ToArray();

        return new GetDiscountWithCustomersModel
        {
            DiscountId = discount.DiscountId,
            DiscountRate = discount.DiscountRate,
            StartDate = discount.StartDate,
            EndDate = discount.EndDate,
            Comment = discount.Comment,
            Customers = new GetCustomerListModel
            {
                Customers = customers,
            },
        };
    }

    public async Task<GetDiscountModel?> GetCurrentCustomerDiscountAsync(int customerId)
    {
        var customer = await _context.Customers.FindAsync(customerId);
            
        if (customer == null)
        {
            return null;
        }

        if(customer.DiscountId == null)
        {
            return null;
        }

        var discount = await _context.CustomerDiscounts.FindAsync(customer.DiscountId);
        
        return new GetDiscountModel
        {
            DiscountId = discount.DiscountId,
            DiscountRate = discount.DiscountRate,
            StartDate = discount.StartDate,
            EndDate = discount.EndDate,
            Comment = discount.Comment,
        };            
    }

    public async Task<GetDiscountListModel?> GetCustomerDiscountsForPeriodAsync(DiscountPeriodModel period)
    {
        var customer = await _context.Customers.FindAsync(period.CustomerId);
        if(customer == null)
        {
            return null;
        }

        var discounts = await _context.CustomerDiscounts
            .Include(c => c.Customers)
            .Where(x => x.Customers.Contains(customer))
            .Where(d => d.StartDate >= period.DateFrom && d.EndDate <= period.DateTo)
            .Select(x => new GetDiscountModel
            {
                DiscountId = x.DiscountId,
                DiscountRate = x.DiscountRate,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Comment = x.Comment,
            }).ToArrayAsync();

        return new GetDiscountListModel
        {
            Discounts = discounts,
        };
    }

    public async Task<DiscountProgramEntity?> UpdateDiscountAsync(UpdateDiscountModel discountModel)
    {
        var entity = await _context.CustomerDiscounts.FindAsync(discountModel.DiscountId);
        if (entity == null)
        {
            return null;
        }

        _context.CustomerDiscounts.Attach(entity);

        if(discountModel.DiscountRate != null)
        {
            entity.DiscountRate = (decimal)discountModel.DiscountRate;
        }

        if (discountModel.StartDate != null)
        {
            entity.StartDate = (DateOnly)discountModel.StartDate;
        }

        if(discountModel.EndDate != null)
        {
            entity.EndDate = (DateOnly)discountModel.EndDate;
        }
        
        if(discountModel.Comment != null)
        {
            entity.Comment = discountModel.Comment;
        }

        await _context.SaveChangesAsync();

        return entity;
    }
}
