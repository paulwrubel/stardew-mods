using StardewModdingAPI;
using StardewModdingAPI.Utilities;

namespace SmartTodo
{
    public sealed class ModConfig
    {
        public KeybindList ToggleTodoListKeybind { get; set; } = new(SButton.L);

        public bool CheckBirthdays { get; set; } = true;

        public bool CheckHarvestableCrops { get; set; } = true;

        public bool CheckWaterableCrops { get; set; } = true;

        public bool CheckToolPickup { get; set; } = true;
    }
}
