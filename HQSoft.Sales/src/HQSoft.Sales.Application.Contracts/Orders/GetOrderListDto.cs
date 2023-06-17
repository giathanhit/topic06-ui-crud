using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HQSoft.Sales.Orders
{
    public class GetOrderListDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
