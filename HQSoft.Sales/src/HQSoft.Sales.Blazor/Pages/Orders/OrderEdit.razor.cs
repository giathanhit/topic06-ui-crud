using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using HQSoft.Sales.Products;
using Blazorise;
using Microsoft.AspNetCore.Components;
using HQSoft.Sales.Orders;
using System.Linq;
using HQSoft.Sales.OrderDetails; 

namespace HQSoft.Sales.Blazor.Pages.Orders
{
    public partial class OrderEdit
    {
        protected CreateUpdateOrderDto EditingEntity = new();
        protected CreateUpdateOrdDetailsDto EditingDetailEntity = new();
        protected Validations EditValidationsRef;

        [Parameter]
        public string Id { get; set; }
        public Guid EditingEntityId { get; set; }

        private IReadOnlyList<ProductDto> ProductList { get; set; }  

        protected override async Task OnInitializedAsync()
        {  
            await base.OnInitializedAsync();

            EditingEntityId = Guid.Parse(Id);

            var entityDto = await OrderAppService.GetAsync(EditingEntityId);

            EditingEntity = ObjectMapper.Map<OrderDto, CreateUpdateOrderDto>(entityDto);

            if (EditValidationsRef != null)
            {
                await EditValidationsRef.ClearAll();
            }
            var orderId = EditingEntity.OrderNumber;

            ProductList = await OrderAppService.GetProductsByOrderDetail(orderId);
        }

        protected virtual async Task UpdateEntityAsync()
        {
            try
            {
                var validate = true;
                if (EditValidationsRef != null)
                {
                    validate = await EditValidationsRef.ValidateAll();
                }
                if (validate)
                {
                    await OrderAppService.UpdateAsync(EditingEntityId, EditingEntity);

                    NavigationManager.NavigateTo("orders");
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        protected virtual async Task DeleteEntityAsync(Guid Id)
        {
            if(EditingEntity.OrderStatus == OrderStatus.Complete)
            {
                await Message.Warn("Đơn hàng này đã hoàn thành nên không thể xóa!!!");
            }
            else
            {
                await OrderAppService.DeleteAsync(Id);
                NavigationManager.NavigateTo("orders");
            }
        }

        private void GoToOrderPage()
        {
            NavigationManager.NavigateTo("/orders");
        }
         

        private bool _hideItem = false;

        private void HideFilterBy()
        {
            _hideItem = !_hideItem;
        } 

    }
}
