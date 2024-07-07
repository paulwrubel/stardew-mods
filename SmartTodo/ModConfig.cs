using StardewModdingAPI;
using StardewModdingAPI.Utilities;

namespace SmartTodo
{
    public sealed class ModConfig
    {
        public KeybindList ToggleTodoListKeybind { get; set; } = new(SButton.L);
    }
}
