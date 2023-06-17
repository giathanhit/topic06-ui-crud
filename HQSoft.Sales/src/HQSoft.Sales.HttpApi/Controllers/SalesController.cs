using HQSoft.Sales.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HQSoft.Sales.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SalesController : AbpControllerBase
{
    protected SalesController()
    {
        LocalizationResource = typeof(SalesResource);
    }
}
