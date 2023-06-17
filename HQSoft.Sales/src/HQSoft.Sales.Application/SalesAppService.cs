using System;
using System.Collections.Generic;
using System.Text;
using HQSoft.Sales.Localization;
using Volo.Abp.Application.Services;

namespace HQSoft.Sales;

/* Inherit your application services from this class.
 */
public abstract class SalesAppService : ApplicationService
{
    protected SalesAppService()
    {
        LocalizationResource = typeof(SalesResource);
    }
}
