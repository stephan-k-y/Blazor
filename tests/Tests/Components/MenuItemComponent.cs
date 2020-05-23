using Bunit.Mocking.JSInterop;
using Xunit;
using Bunit;
using BlazorMenuLibrary;
using System.Collections.Generic;

using static Bunit.ComponentParameterFactory;

namespace SampleApp.Tests.Components
{
    public class MenuItemComponent : TestContext
    {
        private IList<BlazorMenuItem> GenerateTestMenuItems()
        {
            BlazorMenuItem fileMenuClass = new BlazorMenuItem("File");
            BlazorMenuItem editMenuClass = new BlazorMenuItem("Edit");

            BlazorMenuItem newFileMenuClass = new BlazorMenuItem("New File");
            BlazorMenuItem newSolutionMenuClass = new BlazorMenuItem("File");
            BlazorMenuItem recentFilesMenuClass = new BlazorMenuItem("Recent Files");

            fileMenuClass.Add(newFileMenuClass);
            fileMenuClass.Add(newSolutionMenuClass);
            fileMenuClass.Add(recentFilesMenuClass);

            BlazorMenuItem undoMenuClass = new BlazorMenuItem("Undo");
            BlazorMenuItem redoMenuClass = new BlazorMenuItem("Redo");
            editMenuClass.Add(undoMenuClass);
            editMenuClass.Add(redoMenuClass);

            BlazorMenuItem recentFile1MenuClass = new BlazorMenuItem("Recent File1");
            BlazorMenuItem recentFile2MenuClass = new BlazorMenuItem("Recent File2");
            recentFilesMenuClass.Add(recentFile1MenuClass);
            recentFilesMenuClass.Add(recentFile2MenuClass);

            IList<BlazorMenuItem> menuItems = new List<BlazorMenuItem>();
            menuItems.Add(fileMenuClass);
            menuItems.Add(editMenuClass);

            return menuItems;
        }

        [Fact(DisplayName = "Should render BlazorMenuItemComponents with count equal to the count of menuItems being loaded")]
        public void ShouldRenderBlazorMenuItems()
        {
            IList<BlazorMenuItem> menuItems = GenerateTestMenuItems();

            var myMenuComponent = RenderComponent<BlazorMenuComponent>((nameof(BlazorMenuComponent.MenuItems), menuItems));

            var menuItemComponents = myMenuComponent.FindComponents<BlazorMenuItemComponent>();

            Assert.Equal(9, menuItemComponents.Count);
        }

        [Fact(DisplayName = "Should render top level menus")]
        public void ShouldRenderTopLevelMenus()
        {
            IList<BlazorMenuItem> menuItems = GenerateTestMenuItems();

            var myMenuComponent = RenderComponent<BlazorMenuComponent>((nameof(BlazorMenuComponent.MenuItems), menuItems));

            var menuItemComponents = myMenuComponent.FindComponents<BlazorMenuItemComponent>();

            int countTopLevelComponent = 0;
            for(int i = 0; i < menuItemComponents.Count; i++)
            {
                if (menuItemComponents[i].Instance.IsTopLevelMenuItem)
                {
                    countTopLevelComponent++;
                }
            }

            Assert.Equal(2, countTopLevelComponent);
        }
    }
}