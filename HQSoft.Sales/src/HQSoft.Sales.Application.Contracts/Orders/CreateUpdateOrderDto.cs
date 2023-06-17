using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HQSoft.Sales.Orders
{
    public class CreateUpdateOrderDto
    {
        [Required]
      /*  [ConcurrencyCheck]*/
        public string? OrderNumber { get; set; } 
        public DateTime OrderDate { get; set; } = DateTime.Now;
         
        [Required]
        public string? DistributorCode { get; set; }
        [Required]
        public string? DistributorName { get; set; }

        [Required]
        public int Quanity { get; set; }
        [Required]
        public float Total { get; set; }
        [Required]
        public float Tax { get; set; }
        [Required]
        public int Discount { get; set; }
        [Required]
        public float TotalItem { get; set; }
        [Required]
        public float TotalAmount { get; set; }


        [Required]
        public string? CustomerID { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
         
        public string? DeliveryUnit { get; set; }
        public string? DeliveryStaff { get; set; }


        public string? Remark { get; set; }
        public int RewardAmount { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? InvoiceNote { get; set; }
        public Guid CreatorId { get; set; }


        [Required]
        public OrderType OrderType { get; set; } = OrderType.InvoiceOrder;
        [Required]
        public OrderHandle Handle { get; set; } = OrderHandle.None;
        [Required]
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Hold;
        [Required]
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;

    }
}
