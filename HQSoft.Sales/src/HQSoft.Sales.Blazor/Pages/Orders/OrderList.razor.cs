using HQSoft.Sales.Orders;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Blazorise;
using Blazorise.DataGrid;
using System.Linq;
using System.Net.Sockets; 
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.BlazoriseUI.Components;

namespace HQSoft.Sales.Blazor.Pages.Orders
{
    public partial class OrderList
    {
        protected CreateUpdateOrderDto EditingEntity = new();
        private IReadOnlyList<OrderDto> ordersList { get; set; }
        private List<OrderDto> selectedRows = new List<OrderDto>();
        private int PageSize { get; set; } = 1000;
        //private int PageSize { get; } = LimitedResultRequestDto.MaxMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetOrderAsync();
            await SetBreadcrumbItemAsync();
        }

        protected virtual ValueTask SetBreadcrumbItemAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Sales"]));
            return ValueTask.CompletedTask;
        } 
         
        private async Task GetOrderAsync()
        {
            var result = await OrderAppService.GetListAsync(
                new GetOrderListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting
                }
            );

            ordersList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private string _searchString;
        private DataGrid<OrderDto> dataGrid; 

        private Task _quickFilter(string e)
        {
            _searchString = e;
            return dataGrid.Reload();
        }
 
        private bool OnCustomFilter(OrderDto x)
        { 
            if (string.IsNullOrEmpty(_searchString))
                return true; 

            if (x != null && x.OrderNumber != null && x.OrderNumber.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x != null && x.CustomerID != null && x.CustomerID.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x != null && x.CustomerName != null && x.CustomerName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x != null && x.DistributorCode != null && x.DistributorCode.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x != null && x.OrderStatus.ToString() != null && x.OrderStatus.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if ($"{x.OrderDate}".Contains(_searchString))
                return true;

            return false;
        }

        //Xóa nhiều dòng trong Datagrid 
        async Task DeleteSelectedRows()
        {
            foreach (var item in selectedRows)
            {
                if(item.OrderStatus == OrderStatus.Complete)
                {
                    await Message.Warn("Đơn hàng này đã hoàn thành nên không thể xóa!!!");
                }
                else
                {
                    await OrderDetailAppService.DeleteAsync(item.Id);
                    await OrderAppService.DeleteAsync(item.Id);
                }
            }
            // Refresh lại danh sách sau khi xóa
            await GetOrderAsync();
            selectedRows = new List<OrderDto>();
        }

        private void GoToEditPage(OrderDto order)
        {
            NavigationManager.NavigateTo($"order/edit/{order.Id}");
        }
        private void GoToCreatePage()
        {
            NavigationManager.NavigateTo("order/new");
        }

        private bool _hideItem = false;

        private void HideFilterBy()
        {
            _hideItem = !_hideItem;
        }

        DatePicker<DateTime?> datePicker; 
          
    }
}
