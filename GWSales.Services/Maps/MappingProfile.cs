﻿using AutoMapper;
using GWSales.Data.Entities.Customer;
using GWSales.Data.Entities.Product;
using GWSales.Services.Models.Customer;
using GWSales.Services.Models.Customer.Discount;
using GWSales.Services.Models.Order;
using GWSales.Services.Models.Product;
using GWSales.Services.Models.Storage;
using GWSales.Services.Models.User;
using GWSales.WebApi.Models.Customer;
using GWSales.WebApi.Models.Customer.Discount;
using GWSales.WebApi.Models.Order;
using GWSales.WebApi.Models.ProductAssortment;
using GWSales.WebApi.Models.Storage;
using GWSales.WebApi.Models.User;

namespace GWSales.Services.Maps;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //User maps
        CreateMap<RegisterUserDto, RegisterUserModel>();

        //Product maps
        CreateMap<ProductEntity, GetProductModel>();

        CreateMap<ProductEntity, GetProductDto>();

        CreateMap<CreateProductDto, CreateProductModel>();

        CreateMap<UpdateProductDto, UpdateProductModel>();

        CreateMap<GetProductModel, GetProductDto>();

        CreateMap<GetProductListModel, ProductListDto>();

        //Size maps
        CreateMap<SizeEntity, GetSizeDto>();

        CreateMap<ProductSizeEntity, GetProductSizeFullDto>();

        CreateMap<ProductSizeEntity, GetProductSizeShortDto>();

        CreateMap<CreateSizeDto, CreateSizeModel>();

        CreateMap<AddProductSizeDto, AddProductSizeModel>();

        CreateMap<GetProductSizeListModel, ProductSizesListDto>();

        CreateMap<GetProductSizeModel, GetProductSizeFullDto>();

        CreateMap<GetProductSizeModel, GetProductSizeShortDto>();

        //Customer
        CreateMap<CustomerTypeEntity, GetCustomerTypeDto>();

        CreateMap<GetCustomerModel, GetCustomerDto>();

        CreateMap<GetCustomerListModel, GetCustomerListDto>();

        CreateMap<GetCustomerByTypeDto, GetCustomersByTypeModel>();

        CreateMap<UpdateCustomerDto, UpdateCustomerModel>();

        CreateMap<CustomerEntity, UpdateCustomerDto>();

        //Discount
        CreateMap<CreateDiscountDto, CreateDiscountModel>();
        
        CreateMap<UpdateDiscountDto, UpdateDiscountModel>();

        CreateMap<DiscountProgramEntity, GetDiscountDto>();

        CreateMap<GetDiscountModel, GetDiscountDto>();

        CreateMap<GetDiscountListModel, GetDiscountListDto>();

        CreateMap<DiscountPeriodDto, DiscountPeriodModel>();

        //Order
        CreateMap<CreateOrderDto, CreateOrderModel>();

        CreateMap<CreateOrderDetailsDto, CreateOrderDetailsModel>();

        CreateMap<CreateOrderDetailsListDto, CreateOrderDetailsListModel>();

        CreateMap<GetOrderModel, GetOrderDto>();

        CreateMap<GetOrderListModel, GetOrderListDto>();

        CreateMap<GetOrderDetailsModel, GetOrderDetailsDto>();

        CreateMap<GetOrderDetailsListModel, GetOrderDetailsListDto>();

        CreateMap<OrderPeriodDto, OrderPeriodModel>();

        CreateMap<UpdateOrderDetailsDto, UpdateOrderDetailsModel>();

        CreateMap<ChangeOrderStatusDto, ChangeOrderStatusModel>();
    }
}
