using HQSoft.Sales.OrderDetails;
using HQSoft.Sales.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;
using Volo.Abp.Domain.Repositories;

namespace HQSoft.Sales.Orders;

[RemoteService(IsEnabled = true)]
public class OrderAppService :
    CrudAppService<
        Order, //The Order entity
        OrderDto, //Used to show Orders
        Guid, //Primary key of the Order entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateOrderDto>, //Used to create/update a Order
    IOrderAppService //implement the IOrderAppService
{
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<OrderDetail> _orderDetailRepository;

    public OrderAppService(
        IRepository<Order, Guid> repository,
        IUnitOfWorkManager unitOfWorkManager,
        IRepository<Order> orderRepository,
        IRepository<Product> productRepository,
        IRepository<OrderDetail> orderDetailRepository)
        : base(repository)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _orderDetailRepository = orderDetailRepository;
    }

    //Lấy danh sách sản phẩm theo Mã hóa đơn và Mã sản phẩm từ bảng OrderDetail
    public async Task<List<ProductDto>> GetProductsByOrderDetail(string orderId)
    {
        var orderDetails = await _orderDetailRepository.GetListAsync(od => od.OrderID == orderId);

        var products = new List<ProductDto>();


        // Vòng lặp để truy vấn danh sách sản phẩm tương ứng với mỗi chi tiết đơn hàng trong danh sách
        foreach (var orderDetail in orderDetails)
        {
            var product = await _productRepository.FirstOrDefaultAsync(p => p.ProductID == orderDetail.ProductID);

            if (product != null)
            {
                products.Add(ObjectMapper.Map<Product, ProductDto>(product));
            }
        }
        return products;
    }

    // Tạo ra một mã đơn hàng mới.
    public async Task<string?> GenerateOrderIdAsync()
    {
        using (var unitOfWork = _unitOfWorkManager.Begin())
        {
            var lastOrder = _orderRepository
                .GetListAsync()
                .Result
                .OrderByDescending(x => x.OrderNumber)
                .FirstOrDefault();
            var newOrderId = "SO00000000";
            if (lastOrder != null)
            {
                var lastOrderId = lastOrder.OrderNumber; 
                var lastOrderNumber = int.Parse(lastOrderId.Substring(2)); 
                newOrderId = "SO" + (lastOrderNumber + 1).ToString("D8");
            }
            // `Any`: Kiểm tra mã đơn hàng mới có bị trùng với mã đơn hàng nào trong CSDL ko 
            while (_orderRepository.GetListAsync().Result.Any(x => x.OrderNumber == newOrderId))
            { 
                newOrderId = "SO" + (int.Parse(newOrderId.Substring(2)) + 1).ToString("D8");
            }
            await unitOfWork.SaveChangesAsync();
            return newOrderId;
        }
    }

    public override async Task<OrderDto> GetAsync(Guid id)
    { 
        var queryable = await Repository.GetQueryableAsync();
         
        var query = from Order in queryable
                    where Order.Id == id
                    select new { Order };
         
        var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
        if (queryResult == null)
        {
            throw new EntityNotFoundException(typeof(Order), id);
        }

        var OrderDto = ObjectMapper.Map<Order, OrderDto>(queryResult.Order);
        return OrderDto;
    }

    public override async Task<PagedResultDto<OrderDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    { 
        var queryable = await Repository.GetQueryableAsync();
         
        var query = from Order in queryable
                    select new { Order };
         
        query = query
            .OrderBy(NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);
         
        var queryResult = await AsyncExecuter.ToListAsync(query);
         
        var OrderDtos = queryResult.Select(x =>
        {
            var OrderDto = ObjectMapper.Map<Order, OrderDto>(x.Order);
            return OrderDto;
        }).ToList();
         
        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<OrderDto>(
            totalCount,
            OrderDtos
        );
    }

    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"Order.{nameof(Order.OrderNumber)}";
        }

        return $"Order.{sorting}";
    } 
}
