using HQSoft.Sales.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace HQSoft.Sales.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SalesEntityFrameworkCoreModule),
    typeof(SalesApplicationContractsModule)
    )]
public class SalesDbMigratorModule : AbpModule
{

}
