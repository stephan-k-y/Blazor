using System;
using System.Collections.Generic;

namespace BlazorMenuLibrary
{
    public class BlazorMenuItem
    {
        /// <summary>
        /// The text that will be rendered for the menu item
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// List of children menu items
        /// </summary>
        public List<BlazorMenuItem> ChildItems { get; } = new List<BlazorMenuItem>();

        /// <summary>
        /// Adds a sub menu item
        /// </summary>
        /// <param name="child"></param>
        public void Add(BlazorMenuItem child)
        {
            ChildItems.Add(child);
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="text">The text that will be rendered for the menu item</param>
        public BlazorMenuItem(String text)
        {
            Text = text;
        }
    }
}
