using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HQSoft.Sales.Orders
{
    public class OrderDto : AuditedEntityDto<Guid>
    {
        public Guid CreatorId { get; set; }
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
        public bool IsSelected { get; set; }
    }
}
