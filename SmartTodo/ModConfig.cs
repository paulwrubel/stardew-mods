using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Menus;

namespace SmartTodo
{
    public sealed class ModConfig
    {
        public KeybindList ToggleTodoListKeybind { get; set; } = new(SButton.L);
    }
}
