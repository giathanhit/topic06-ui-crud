﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace HQSoft.Sales.Orders
{
    public class OrderDetail : FullAuditedAggregateRoot<Guid>
    { 
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
    }
}
