using StardewModdingAPI;
using StardewModdingAPI.Utilities;

namespace AutomaticTodoList
{
    public sealed class ModConfig
    {
        public KeybindList ToggleTodoList { get; set; } = KeybindList.Parse($"{SButton.LeftShift} + {SButton.L}");

        public bool CheckBirthdays { get; set; } = true;

        public bool CheckHarvestableCrops { get; set; } = true;

        public bool CheckWaterableCrops { get; set; } = true;

        public bool CheckHarvestableMachines { get; set; } = true;

        public bool CheckToolPickup { get; set; } = true;

        public bool CheckDailyQuestBulletinBoard { get; set; } = true;

        public bool CheckSpecialOrdersBoard { get; set; } = true;
    }
}
