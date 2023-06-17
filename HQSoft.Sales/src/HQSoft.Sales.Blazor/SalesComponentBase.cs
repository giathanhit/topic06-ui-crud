using HQSoft.Sales.Localization;
using Volo.Abp.AspNetCore.Components;

namespace HQSoft.Sales.Blazor;

public abstract class SalesComponentBase : AbpComponentBase
{
    protected SalesComponentBase()
    {
        LocalizationResource = typeof(SalesResource);
    }
}
