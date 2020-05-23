using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorMenuLibrary
{
    public partial class BlazorMenuItemComponent : ComponentBase
    {
        [Parameter]
        public BlazorMenuItem MenuItem { get; set; }

        [Parameter]
        public EventCallback<BlazorMenuItem> OnMenuItemClicked { get; set; }

        [Parameter]
        public bool IsTopLevelMenuItem { get; set; }

        private string ChildrenVisible { get; set; }

        private string Color { get; set; }

        private bool HasChildrenItems {
            get { return MenuItem?.ChildItems?.Count > 0; }
        }

        protected override void OnInitialized()
        {
            SetColor(false);
            SetChildrenVisible(false);
            base.OnInitialized();
        }

        private void SetChildrenVisible(bool visible)
        {
            ChildrenVisible = visible ? "visibility:visible" : "visibility:hidden";
        }

        private void SetColor(bool highlighted)
        {
            Color = highlighted ? "background-color: #bbbf15" : "background-color: #aaaf15";
        }

        private void MouseOver(MouseEventArgs e)
        {
            SetColor(true);
            SetChildrenVisible(true);
        }

        private void MouseOut()
        {
            SetColor(false);
            SetChildrenVisible(false);
        }

        private void OnClick()
        {
            if (MenuItem.ChildItems.Count == 0)
            {
                SetChildrenVisible(false);
                OnMenuItemClicked.InvokeAsync(MenuItem);
            }
        }

        private void OnChildElementClicked(BlazorMenuItem menuItem)
        {
            SetChildrenVisible(false);
            OnMenuItemClicked.InvokeAsync(menuItem);
        }
    }
}