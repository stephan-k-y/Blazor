using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BlazorMenuLibrary
{
    public partial class BlazorMenuComponent : ComponentBase
    {
        /// <summary>
        /// The menu items to be displayed
        /// </summary>
        [Parameter]
        public IList<BlazorMenuItem> MenuItems { get; set; }

        /// <summary>
        /// Triggered when a click on a menu item happens
        /// (does not trugger if the menu item has sub items)
        /// </summary>
        [Parameter]
        public EventCallback<BlazorMenuItem> OnBlazorMenuItemClicked { get; set; }

        private void OnChildItemClick(BlazorMenuItem item)
        {
            OnBlazorMenuItemClicked.InvokeAsync(item);
        }
    }
}