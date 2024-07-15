using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;

namespace AutomaticTodoList;

public sealed class ModConfig
{
    public KeybindList ToggleTodoList { get; set; } = KeybindList.Parse($"{SButton.LeftShift} + {SButton.L}");

    public Vector2 PanelPosition { get; set; } = new(10, 100);

    public bool CheckBirthdays { get; set; } = true;

    public bool CheckHarvestableCrops { get; set; } = true;

    public bool CheckWaterableCrops { get; set; } = true;

    public bool CheckReadyMachines { get; set; } = true;

    public bool CheckToolPickup { get; set; } = true;

    public bool CheckDailyQuestBulletinBoard { get; set; } = true;

    public bool CheckSpecialOrdersBoard { get; set; } = true;

    public bool CheckTravelingMerchant { get; set; } = true;

    public bool CheckGiftingNPCs { get; set; } = false;

    public string GiftingNPCsString { get; set; } = "";
}
