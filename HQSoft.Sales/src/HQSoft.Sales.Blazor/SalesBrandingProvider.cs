using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace HQSoft.Sales.Blazor;

[Dependency(ReplaceServices = true)]
public class SalesBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Sales";
}
