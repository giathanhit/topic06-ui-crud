using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HQSoft.Sales.Localization;
using HQSoft.Sales.MultiTenancy;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;
using Blazorise;
using Blazorise.Icons.FontAwesome;
using Blazorise.Bootstrap5;

namespace HQSoft.Sales.Blazor.Menus;

public class SalesMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public SalesMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<SalesResource>();
        var menu = context.GetLocalizer<SalesResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                SalesMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home"
                )
        );

        context.Menu.Items.Add(
            new ApplicationMenuItem(
                SalesMenus.Home,
                menu["Product Management"],
                "/products",
                icon: "fa fa-receipt"
            )
        );

        context.Menu.AddGroup(
                new ApplicationMenuGroup(
                    name: "Order",
                    displayName: l["Menu:Sales"]
                )
            );

        context.Menu.AddItem(
            new ApplicationMenuItem("Sales.DataEntry", l["Menu:Sales"], icon: "fa fa-shopping-cart", groupName: "Order")
                .AddItem(new ApplicationMenuItem(
                    name: "Sales.DataEntry",
                    displayName: l["Sales:DataEntry"]
                ).AddItem(new ApplicationMenuItem(
                    name: "Sales.DataEntry.SalesOrder",
                    displayName: l["Sales.DataEntry:SalesOrder"],
                    url: "/orders")
                 ).AddItem(new ApplicationMenuItem(
                     name: "Sales.DataEntry.POS",
                     displayName: l["Sales.DataEntry:POS"],
                     url: "/pos")
                 ).AddItem(new ApplicationMenuItem(
                     name: "Sales.DataEntry.TableOrderApproval",
                     displayName: "Table Order Approval",
                     url: "/table-order-approval")
                 ).AddItem(new ApplicationMenuItem(
                     name: "Sales.DataEntry.DeliveryAndPayment",
                     displayName: "Delivery And Payment",
                     url: "/delivery-and-payment")
                 ).AddItem(new ApplicationMenuItem(
                     name: "Sales.DataEntry.ProposedShipmentReport",
                     displayName: "Proposed Shipment Report",
                     url: "/proposed-shipment-report")
                 ).AddItem(new ApplicationMenuItem(
                     name: "Sales.DataEntry.ReturnOrderReport",
                     displayName: "Return Order Report",
                     url: "/return-order-report")
                 ))//End Datatry 

                .AddItem(new ApplicationMenuItem(
                     name: "Sales.RouteManager",
                     displayName: l["Sales.RouteManager"]
                 ).AddItem(new ApplicationMenuItem(
                     name: "Sales.RouteManager.DeliveryAndPayment",
                     displayName: "Delivery And Payment",
                     url: "/delivery-and-payment")
                 ))//End RouteManager

                .AddItem(new ApplicationMenuItem(
                     name: "Sales.Maintenance",
                     displayName: l["Sales.Maintenance"]
                 ).AddItem(new ApplicationMenuItem(
                     name: "Sales.Maintenance.ABC",
                     displayName: "Delivery And Payment",
                     url: "/delivery-and-payment")
                 ))//End Maintenance

        );//End Order


        context.Menu.Items.Add(
            new ApplicationMenuItem(
                SalesMenus.Home,
                menu["Gift Management"],
                "/gift",
                icon: "fas fa-gift"
            )
        );


        context.Menu.Items.Add(
            new ApplicationMenuItem(
                SalesMenus.Home,
                menu["Money"],
                "/money",
                icon: "fa fa-money-bill"
            )
        );

        context.Menu.Items.Add(
            new ApplicationMenuItem(
                SalesMenus.Home,
                menu["Sales"],
                "/sales",
                icon: "fa fa-credit-card"
            )
        );


        //context.Menu.Items.Add(
        //    new ApplicationMenuItem(
        //        SalesMenus.Home,
        //        menu["Price"],
        //        "/",
        //        icon: IconName.Home.ToString()
        //    )
        //);

        context.Menu.Items.Add(
            new ApplicationMenuItem(
                SalesMenus.Home,
                menu["Statistic"],
                "/statistic",
                icon: "fa fa-chart-line"
                )
            );

        context.Menu.Items.Add(
            new ApplicationMenuItem(
                SalesMenus.Home,
                menu["Info"],
                "/info",
                icon: "fa fa-info"
            )
        );

        //context.Menu.Items.Add(
        //    new ApplicationMenuItem(
        //        SalesMenus.Home,
        //        menu["Settings"],
        //        "/",
        //        icon: IconName.Home.ToString()
        //    )
        //);

        var administration = context.Menu.GetAdministration();

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        return Task.CompletedTask;
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var accountStringLocalizer = context.GetLocalizer<AccountResource>();

        var authServerUrl = _configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(new ApplicationMenuItem(
            "Account.Manage",
            accountStringLocalizer["MyAccount"],
            $"{authServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
            icon: "fa fa-cog",
            order: 1000,
            null).RequireAuthenticated());

        return Task.CompletedTask;
    }
}
