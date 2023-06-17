using System.Threading.Tasks;

namespace HQSoft.Sales.Data;

public interface ISalesDbSchemaMigrator
{
    Task MigrateAsync();
}
