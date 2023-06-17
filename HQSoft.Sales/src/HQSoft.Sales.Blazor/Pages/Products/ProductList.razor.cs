
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using HQSoft.Sales.Products;
using Blazorise.DataGrid;
using HQSoft.Sales.Orders;

namespace HQSoft.Sales.Blazor.Pages.Products
{
    public partial class ProductList
    {

        private IReadOnlyList<ProductDto> products { get; set; }
        private List<ProductDto> selectedRows = new List<ProductDto>();

        private int PageSize { get; set; } = 1000;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }

        protected override async Task OnInitializedAsync()
        { 
            await GetProductAsync();
        }
          
        private async Task GetProductAsync()
        {
            var result = await ProductAppService.GetListAsync(
                new GetProductListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting
                }
            );

            products = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private string _searchString;
        private DataGrid<OrderDto> dataGrid; 

        private Task _quickFilter(string e)
        {
            _searchString = e;
            return dataGrid.Reload();
        }


        private bool OnCustomFilter(ProductDto x)
        {
            // We want to accept empty value as valid or otherwise
            // datagrid will not show anything.
            if (string.IsNullOrEmpty(_searchString))
                return true;

            if (x != null && x.ProductID != null && x.ProductID.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x != null && x.ProductName != null && x.ProductName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x != null && x.Unit != null && x.Unit.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x != null && x.SiteCode != null && x.SiteCode.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true; 

            return false;
        }

        private void GotoEditPage(ProductDto product)
        {
            NavigationManager.NavigateTo($"/product/edit/{product.Id}");
        }

        private void GotoCreatePage()
        {
            NavigationManager.NavigateTo($"/product/new");
        }

        //Xóa nhiều dòng trong Datagrid 
        async Task DeleteSelectedRows()
        {
            foreach (var item in selectedRows)
            {
                await AppService.DeleteAsync(item.Id);
            }
            // Refresh lại danh sách sau khi xóa
            await GetProductAsync();
            selectedRows = new List<ProductDto>();
        }


        private bool _hideItem = false; 
        private void HideFilterBy()
        {
            _hideItem = !_hideItem;
        }
    }
}
