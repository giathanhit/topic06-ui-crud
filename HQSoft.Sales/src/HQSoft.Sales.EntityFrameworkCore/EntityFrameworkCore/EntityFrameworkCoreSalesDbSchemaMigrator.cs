using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HQSoft.Sales.Data;
using Volo.Abp.DependencyInjection;

namespace HQSoft.Sales.EntityFrameworkCore;

public class EntityFrameworkCoreSalesDbSchemaMigrator
    : ISalesDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreSalesDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the SalesDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<SalesDbContext>()
            .Database
            .MigrateAsync();
    }
}
