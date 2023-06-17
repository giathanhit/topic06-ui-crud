
using Blazorise; 
using HQSoft.Sales.Products;
using Microsoft.AspNetCore.Components; 
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;

namespace HQSoft.Sales.Blazor.Pages.Products
{
    public partial class ProductEdit
    { 
        protected CreateUpdateProductDto EditingEntity = new();
        protected Validations EditValidationsRef;

        [Parameter]
        public string Id { get; set; }

        private int PageSize { get; set; } = 1000;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }

        public Guid EditingEntityId { get; set; }

        protected override async Task OnInitializedAsync()
        { 
            await base.OnInitializedAsync();

            EditingEntityId = Guid.Parse(Id);

            var entityDto = await ProductAppService.GetAsync(EditingEntityId);

            EditingEntity = ObjectMapper.Map<ProductDto, CreateUpdateProductDto>(entityDto);

            if (EditValidationsRef != null)
            {
                await EditValidationsRef.ClearAll();
            }
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
                    await ProductAppService.UpdateAsync(EditingEntityId, EditingEntity);

                    NavigationManager.NavigateTo("products");
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        } 

        private void GoToProductPage()
        {
            NavigationManager.NavigateTo("products");
        }

        protected async Task DeleteEntitySync(Guid Id)
        {
            await ProductAppService.DeleteAsync(Id);
            NavigationManager.NavigateTo("products");  
        }
           
        private bool _hideItem = false;

        private void HideFilterBy()
        {
            _hideItem = !_hideItem;
        }
    }
}
