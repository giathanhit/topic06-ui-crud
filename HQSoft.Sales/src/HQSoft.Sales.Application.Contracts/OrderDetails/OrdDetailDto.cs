using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HQSoft.Sales.OrderDetails
{
    public class OrdDetailDto : AuditedEntityDto<Guid>
    { 
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public string? ProductName { get; set; }
        public string SKU { get; set; }
        public string? SiteCode { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public bool IsSelected { get; set; }
    }
}
