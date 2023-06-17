using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HQSoft.Sales.Products
{
    public class CreateUpdateProductDto
    {
        [Required]
        public string? ProductID { get; set; }

        [Required]
        [StringLength(128)]
        public string? ProductName { get; set; }

        [Required]
        public int Quanity { get; set; }

        [Required]
        public double SalesPrice { get; set; } 

        public string? Unit { get; set; }

        public string? SiteCode { get; set; }

        public string? QRCode { get; set; }
    }
}
