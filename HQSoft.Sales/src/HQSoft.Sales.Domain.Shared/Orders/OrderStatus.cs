using System;
using System.Collections.Generic;
using System.Text;

namespace HQSoft.Sales.Orders
{
    public enum OrderStatus
    {
        Ordered,
        Hold,
        PrintedInvoice,
        Processing,
        ShipOnly,
        DeliveryAllocation,
        Complete,
        Cancel
    }
}
