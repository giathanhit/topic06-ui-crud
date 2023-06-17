using HQSoft.Sales.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HQSoft.Sales.Permissions;

public class SalesPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SalesPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(SalesPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SalesResource>(name);
    }
}
