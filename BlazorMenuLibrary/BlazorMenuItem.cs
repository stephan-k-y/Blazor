using System;
using System.Collections.Generic;

namespace BlazorMenuLibrary
{
    public class BlazorMenuItem
    {
        public string Text { get; set; }

        public List<BlazorMenuItem> ChildItems { get; } = new List<BlazorMenuItem>();

        public void Add(BlazorMenuItem child)
        {
            ChildItems.Add(child);
        }

        public BlazorMenuItem(String text)
        {
            Text = text;
        }
    }
}
