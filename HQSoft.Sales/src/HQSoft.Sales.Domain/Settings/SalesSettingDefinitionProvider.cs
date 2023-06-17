using Volo.Abp.Settings;

namespace HQSoft.Sales.Settings;

public class SalesSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(SalesSettings.MySetting1));
    }
}
