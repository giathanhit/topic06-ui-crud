using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HQSoft.Sales.OrderDetails
{
    public class CreateUpdateOrdDetailsDto
    { 
        public string? OrderID { get; set; }
        public string? ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? SKU { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
    }
}
