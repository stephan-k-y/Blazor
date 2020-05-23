using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BlazorMenuLibrary
{
    public partial class BlazorMenuComponent : ComponentBase
    {
        [Parameter]
        public IList<BlazorMenuItem> MenuItems { get; set; }

        [Parameter]
        public EventCallback<BlazorMenuItem> OnMenuItemClicked { get; set; }

        private void OnChildItemClick(BlazorMenuItem item)
        {
            OnMenuItemClicked.InvokeAsync(item);
        }
    }
}