using AutoMapper;
using HQSoft.Sales.OrderDetails;
using HQSoft.Sales.Orders;
using HQSoft.Sales.Products;

namespace HQSoft.Sales.Blazor;

public class SalesBlazorAutoMapperProfile : Profile
{
    public SalesBlazorAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Blazor project.
        CreateMap<OrderDto, CreateUpdateOrderDto>();
        CreateMap<OrdDetailDto, CreateUpdateOrdDetailsDto>();
        CreateMap<ProductDto, CreateUpdateProductDto>();
    }
}
