using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace HQSoft.Sales;

[Dependency(ReplaceServices = true)]
public class SalesBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Sales";
}
