using HQSoft.Sales.Orders;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace HQSoft.Sales.Orders
{
    public class Order : FullAuditedAggregateRoot<Guid>
    {
        public string? OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? InvoiceNote { get; set; }

        public string? DeliveryStaff { get; set; }
        public string? DeliveryUnit { get; set; }

        public string? DistributorCode { get; set; }
        public string? DistributorName { get; set; }

        public string? Remark { get; set; }

        public string? CustomerID { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }

        public int Quanity { get; set; }
        public float Total { get; set; }
        public int RewardAmount { get; set; }
        public float Tax { get; set; }
        public int Discount { get; set; }
        public float TotalItem { get; set; }
        public float TotalAmount { get; set; }

        public OrderType OrderType { get; set; }
        public OrderHandle Handle { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

    }
}
