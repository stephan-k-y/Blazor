using Xunit;
using Bunit;
using BlazorMenuLibrary;
using System.Collections.Generic;

using static Bunit.ComponentParameterFactory;
using AngleSharp.Dom;

namespace SampleApp.Tests.Components
{
    public class MenuItemComponent : TestContext
    {
        private IList<BlazorMenuItem> GenerateTestMenuItems()
        {
            BlazorMenuItem item11 = new BlazorMenuItem("Item 1.1");
            BlazorMenuItem item12 = new BlazorMenuItem("Item 1.2");

            BlazorMenuItem item121 = new BlazorMenuItem("Item 1.2.1");
            BlazorMenuItem item122 = new BlazorMenuItem("Item 1.2.2");
            item12.Add(item121);
            item12.Add(item122);


            IList<BlazorMenuItem> menuItems = new List<BlazorMenuItem>();
            menuItems.Add(item11);
            menuItems.Add(item12);

            return menuItems;
        }

        [Fact(DisplayName = "Should render BlazorMenuItemComponents with count equal to the count of menuItems being loaded")]
        public void ShouldRenderBlazorMenuItems()
        {
            IList<BlazorMenuItem> menuItems = GenerateTestMenuItems();

            var myMenuComponent = RenderComponent<BlazorMenuComponent>((nameof(BlazorMenuComponent.MenuItems), menuItems));

            var menuItemComponents = myMenuComponent.FindComponents<BlazorMenuItemComponent>();

            Assert.Equal(4, menuItemComponents.Count);
        }

        [Fact(DisplayName = "Should render top level menus")]
        public void ShouldRenderTopLevelMenus()
        {
            IList<BlazorMenuItem> menuItems = GenerateTestMenuItems();

            var myMenuComponent = RenderComponent<BlazorMenuComponent>((nameof(BlazorMenuComponent.MenuItems), menuItems));

            var menuItemComponents = new List<IRenderedComponent<BlazorMenuItemComponent>>();
            menuItemComponents.AddRange(myMenuComponent.FindComponents<BlazorMenuItemComponent>());

            int countTopLevelComponent =
                menuItemComponents.FindAll((component) => component.Instance.IsTopLevelMenuItem).Count;

            Assert.Equal(2, countTopLevelComponent);
        }

        [Fact(DisplayName = "Clicking on item with no subitems shoud trigger OnMenuItemClicked")]
        public void ShouldTriggerOnMenuItemClicked()
        {
            var wasCalled = false;

            IList<BlazorMenuItem> menuItems = GenerateTestMenuItems();

            var myMenuComponent = RenderComponent<BlazorMenuComponent>(
               EventCallback(nameof(BlazorMenuComponent.OnMenuItemClicked), (BlazorMenuItem _) => wasCalled = true),
               (nameof(BlazorMenuComponent.MenuItems), menuItems)
           );

            var allAnchors = myMenuComponent.FindAll("a");

            var anchorElements = new List<IElement>();
            anchorElements.AddRange(allAnchors);

            var item121 = anchorElements.Find((element) => element.TextContent.Trim() == "Item 1.2.1");
            item121?.Click();

            Assert.True(wasCalled);
        }

        [Fact(DisplayName = "Clicking on item with subitems shoud not trigger OnMenuItemClicked")]
        public void ShouldNotTriggerOnMenuItemClicked()
        {
            var wasCalled = false;

            IList<BlazorMenuItem> menuItems = GenerateTestMenuItems();

            var myMenuComponent = RenderComponent<BlazorMenuComponent>(
               EventCallback(nameof(BlazorMenuComponent.OnMenuItemClicked), (BlazorMenuItem _) => wasCalled = true),
               (nameof(BlazorMenuComponent.MenuItems), menuItems)
           );

            var allAnchors = myMenuComponent.FindAll("a");

            var anchorElements = new List<IElement>();
            anchorElements.AddRange(allAnchors);

            var item12 = anchorElements.Find((element) => element.TextContent.Trim() == "Item 1.2");
            item12?.Click();

            Assert.False(wasCalled);
        }

        [Fact(DisplayName = "Should render arrow for a menu with a submenu")]
        public void ShouldRenderArrow()
        {
            IList<BlazorMenuItem> menuItems = GenerateTestMenuItems();

            var myMenuComponent = RenderComponent<BlazorMenuComponent>((nameof(BlazorMenuComponent.MenuItems), menuItems));

            var menuItemComponents = new List<IRenderedComponent<BlazorMenuItemComponent>>();
            menuItemComponents.AddRange(myMenuComponent.FindComponents<BlazorMenuItemComponent>());


            menuItemComponents.ForEach((menuItem) =>
            {
                if (menuItem.Instance.MenuItem?.ChildItems?.Count > 0)
                {
                    Assert.NotNull(menuItem.Find(".arrow"));
                }
            });
        }
    }
}