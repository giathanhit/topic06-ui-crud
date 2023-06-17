using Volo.Abp.Modularity;

namespace HQSoft.Sales;

[DependsOn(
    typeof(SalesApplicationModule),
    typeof(SalesDomainTestModule)
    )]
public class SalesApplicationTestModule : AbpModule
{

}
