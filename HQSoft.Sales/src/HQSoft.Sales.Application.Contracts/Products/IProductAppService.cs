using System; 
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HQSoft.Sales.Products;
public interface IProductAppService :
ICrudAppService< //Defines CRUD methods
    ProductDto, //Used to show product
    Guid, //Primary key of the product entity
    PagedAndSortedResultRequestDto, //Used for paging/sorting
    CreateUpdateProductDto> //Used to create/update a product
{
}
