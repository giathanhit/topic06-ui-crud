using HQSoft.Sales.OrderDetails;
using HQSoft.Sales.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HQSoft.Sales.Orders;
public interface IOrderAppService :
ICrudAppService< //Defines CRUD methods
    OrderDto, //Used to show order
    Guid, //Primary key of the order entity
    PagedAndSortedResultRequestDto, //Used for paging/sorting
    CreateUpdateOrderDto> //Used to create/update a order
{ 
    Task<string?> GenerateOrderIdAsync(); 
    Task<List<ProductDto>> GetProductsByOrderDetail(string orderId); 
}
