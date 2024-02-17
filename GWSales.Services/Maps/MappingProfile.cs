using AutoMapper;
using GWSales.Data.Entities.Product;
using GWSales.Services.Models.Product;
using GWSales.Services.Models.Storage;
using GWSales.WebApi.Models.ProductAssortment;
using GWSales.WebApi.Models.Storage;

namespace GWSales.Services.Maps;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Product maps
        CreateMap<ProductEntity, GetProductModel>();

        CreateMap<CreateProductDto, CreateProductModel>();

        CreateMap<UpdateProductDto, UpdateProductModel>();

        CreateMap<GetProductModel, GetProductDto>();

        CreateMap<GetProductListModel, ProductListDto>();

        //Size maps
        CreateMap<SizeEntity, GetSizeDto>();

        CreateMap<ProductSizeEntity, GetProductSizeFullDto>();
        
        CreateMap<CreateSizeDto, CreateSizeModel>();

        CreateMap<AddProductSizeDto, AddProductSizeModel>();

        CreateMap<GetProductSizeListModel, ProductSizesListDto>();

        CreateMap<GetProductSizeModel, GetProductSizeFullDto>();

        CreateMap<GetProductSizeModel, GetProductSizeShortDto>();


    }
}
