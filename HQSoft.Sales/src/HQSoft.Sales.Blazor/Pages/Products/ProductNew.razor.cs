using Blazorise;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp;
using HQSoft.Sales.Products;

namespace HQSoft.Sales.Blazor.Pages.Products
{
    public partial class ProductNew
    {
        protected Validations CreateValidationsRef;
        protected CreateUpdateProductDto NewEntity = new(); 

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync(); 

            if (CreateValidationsRef != null)
            {
                await CreateValidationsRef.ClearAll();
            }
        }

        protected virtual async Task CreateEntityAsync()
        {
            try
            {
                var validate = true;
                if (CreateValidationsRef != null)
                {
                    validate = await CreateValidationsRef.ValidateAll();
                }
                if (validate)
                {
                    await AppService.CreateAsync(NewEntity);
                    GoToProductPage();
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

        private bool _hideItem = false;
        private void HideFilterBy()
        {
            _hideItem = !_hideItem;
        }
    }
} 
