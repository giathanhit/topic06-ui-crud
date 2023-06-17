using HQSoft.Sales.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace HQSoft.Sales;

[DependsOn(
    typeof(SalesEntityFrameworkCoreTestModule)
    )]
public class SalesDomainTestModule : AbpModule
{

}
