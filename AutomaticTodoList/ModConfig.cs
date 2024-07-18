using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;

namespace AutomaticTodoList;

public sealed class ModConfig
{
    /**
     * System settings, set by the mod internally.
     */

    public bool IsPanelVisible { get; set; } = true;

    /**
     * User settings, changable by the player in-game using the Generic Mod Config Menu.
     */

    public KeybindList ToggleTodoListKeybind { get; set; } = KeybindList.Parse($"{SButton.LeftShift} + {SButton.L}");

    public Vector2 PanelPosition { get; set; } = new(10, 80);

    public int VisibleItemCount { get; set; } = 10;

    public bool CheckBirthdays { get; set; } = true;

    public bool CheckFestivals { get; set; } = true;

    public bool CheckHarvestableCrops { get; set; } = true;

    public bool CheckWaterableCrops { get; set; } = true;

    public bool CheckReadyMachines { get; set; } = true;

    public bool CheckToolPickup { get; set; } = true;

    public bool CheckDailyQuestBulletinBoard { get; set; } = true;

    public bool CheckSpecialOrdersBoard { get; set; } = true;

    public bool CheckTravelingMerchant { get; set; } = true;

    public bool CheckQueenOfSauce { get; set; } = true;

    public bool CheckGiftingNPCs { get; set; } = false;

    public string GiftingNPCsString { get; set; } = "";
}
