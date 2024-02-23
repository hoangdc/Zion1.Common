using Microsoft.AspNetCore.Components;
using RestSharp;
using Telerik.Blazor.Components;
using Zion1.Common.Helper.Api;

namespace Zion1.Common.Components
{
    public class ComponentApi : ComponentBase
    {
        [Parameter]
        public bool IsServer { get; set; } = true;

        public ApiConsumer ApiConsumer { get; set; } = new ApiConsumer(ApiHelper.ApiSettings);

        public TelerikNotification NotificationResult { get; set; } = new();
        public string MessageResult { get; set; } = string.Empty;

        protected async override Task OnInitializedAsync()
        {
            await OnInitAsync();

            await base.OnInitializedAsync();
        }

        protected virtual Task OnInitAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<T> GetAsync<T>(string resourceName)
        {
            return await ApiConsumer.ExecuteJsonAsync<T>(resourceName);
        }

        public async Task SaveAsync<T>(T selectedItem, string resourceName)
        {
            if (selectedItem != null)
            {
                ApiConsumer.Body = selectedItem;
                var response = await ApiConsumer.ExecuteAsync(resourceName);

                if (!response.IsSuccessStatusCode)
                {
                    //Logic for handling unsuccessful response
                    MessageResult = response.StatusCode + " - " + response.Content;
                }
                else
                {
                    MessageResult = "Success";
                }
            }
            else
            {
                MessageResult = "Item not found!";
            }
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender && !string.IsNullOrEmpty(MessageResult.Trim()))
            {
                NotificationResult.HideAll();
                NotificationResult.Show(new NotificationModel
                {
                    Text = MessageResult,
                    ThemeColor = "success",
                    CloseAfter = 3000
                });
            }

            return base.OnAfterRenderAsync(firstRender);
        }

    }
}
