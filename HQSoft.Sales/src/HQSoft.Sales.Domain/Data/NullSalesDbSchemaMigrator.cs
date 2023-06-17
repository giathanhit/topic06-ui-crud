using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace HQSoft.Sales.Data;

/* This is used if database provider does't define
 * ISalesDbSchemaMigrator implementation.
 */
public class NullSalesDbSchemaMigrator : ISalesDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
