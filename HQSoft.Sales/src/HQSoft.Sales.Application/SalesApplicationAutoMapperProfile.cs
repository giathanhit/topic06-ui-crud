using AutoMapper;
using HQSoft.Sales.OrderDetails;
using HQSoft.Sales.Orders;
using HQSoft.Sales.Products;

namespace HQSoft.Sales;

public class SalesApplicationAutoMapperProfile : Profile
{
    public SalesApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Product, ProductDto>();
        CreateMap<CreateUpdateProductDto, Product>();


        CreateMap<Order, OrderDto>();
        CreateMap<CreateUpdateOrderDto, Order>();

        CreateMap<OrderDetail, OrdDetailDto>();
        CreateMap<CreateUpdateOrdDetailsDto, OrderDetail>();
    }
}
