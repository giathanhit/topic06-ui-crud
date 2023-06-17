using Blazorise;
using HQSoft.Sales.Blazor.Pages.Products;
using HQSoft.Sales.OrderDetails;
using HQSoft.Sales.Orders;
using HQSoft.Sales.Products;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace HQSoft.Sales.Blazor.Pages.Orders
{
    public partial class OrderNew
    {  
        private IReadOnlyList<ProductDto> products { get; set; }

        protected Validations CreateValationRef;

        protected CreateUpdateOrderDto NewEntity = new(); 
        protected CreateUpdateOrdDetailsDto NewDetailEntity = new();

        private int PageSize { get; set; } = 1000;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (CreateValationRef != null)
            {
                await CreateValationRef.ClearAll();
            }

            NewEntity.OrderNumber = await OrderAppService.GenerateOrderIdAsync();
            NewDetailEntity.OrderID = await OrderAppService.GenerateOrderIdAsync();

            await CalculatePrice();
            await GetProductAsync();
        }
        private void UpdateTotal(int newQuantity)
        {
            NewDetailEntity.Quantity = newQuantity;
            NewDetailEntity.Total = NewDetailEntity.Quantity * NewDetailEntity.Price;
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
         
        // Trong component
        protected virtual async Task CalculatePrice()
        {
            if (!string.IsNullOrEmpty(NewDetailEntity.ProductID))
            {
                // Tìm sản phẩm trong productList dựa trên mã sản phẩm
                var product = products.FirstOrDefault(p => p.ProductID == NewDetailEntity.ProductID);
                if (product != null)
                {
                    // Tính toán giá tiền và đưa vào cột Price
                    NewDetailEntity.Price = NewDetailEntity.Quantity * product.SalesPrice;
                }
            }
        } 
        protected virtual async Task CreateEntityAsync()
        {
            try
            {
                var validate = true;
                if (CreateValationRef != null)
                {
                    validate = await CreateValationRef.ValidateAll();
                }
                if (validate)  
                { 
                    OrderAppService.CreateAsync(NewEntity);
                    OrdDetailAppService.CreateAsync(NewDetailEntity);
                    Message.Success("Thêm mới thành công!!!");
                    NavigationManager.NavigateTo("orders");

                } 
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
          
        private void GoToOrderPage()
        {
            NavigationManager.NavigateTo("orders");
        }


        private bool _hideItem = false;

        private void HideFilterBy()
        {
            _hideItem = !_hideItem;
        }
    }
}
