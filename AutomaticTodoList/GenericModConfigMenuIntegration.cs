using AutomaticTodoList.Integrations;
using StardewModdingAPI;

namespace AutomaticTodoList;

/// <summary>Registers the mod configuration with Generic Mod Config Menu.</summary>
/// <remarks>Construct an instance.</remarks>
/// <param name="manifest">The Automatic Todo List manifest.</param>
/// <param name="modRegistry">An API for fetching metadata about loaded mods.</param>
/// <param name="config">Get the current mod config.</param>
/// <param name="save">Save the mod's current config to the <c>config.json</c> file.</param>
internal class GenericModConfigMenuIntegration(IManifest manifest, IModRegistry modRegistry, ModConfig config, Action save, Action<string, object>? update = null)
{
    /*********
    ** Fields
    *********/
    /// <summary>The Automatic Todo List manifest.</summary>
    private readonly IManifest Manifest = manifest;

    /// <summary>The Generic Mod Config Menu integration.</summary>
    private readonly IGenericModConfigMenuApi? ConfigMenu = modRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");

    /// <summary>The current mod settings.</summary>
    private readonly ModConfig Config = config;

    /// <summary>Save the mod's current config to the <c>config.json</c> file.</summary>
    private readonly Action Save = save;

    /// <summary>A callback when the config was updated through the Generic Mod Config Menu.</summary>
    private readonly Action<string, object>? Update = update;

    /// <summary>Register the config menu if available.</summary>
    public void Register()
    {
        var configMenu = this.ConfigMenu;
        if (configMenu is null)
        {
            return;
        }

        configMenu.Register(this.Manifest, this.Reset, this.Save);

        // controls
        configMenu.AddSectionTitle(
            mod: this.Manifest,
            text: I18n.Config_General_Title
        );
        configMenu.AddKeybindList(
            mod: this.Manifest,
            name: I18n.Config_General_ToggleKeybind_Name,
            tooltip: I18n.Config_General_ToggleKeybind_Description,
            getValue: () => this.Config.ToggleTodoListKeybind,
            setValue: value => this.Config.ToggleTodoListKeybind = value
        );
        configMenu.AddNumberOption(
            mod: this.Manifest,
            name: I18n.Config_General_VisibleItemCount_Name,
            tooltip: I18n.Config_General_VisibleItemCount_Description,
            getValue: () => this.Config.VisibleItemCount,
            setValue: value => this.Config.VisibleItemCount = value,
            min: 1,
            max: 30
        );
        configMenu.AddNumberOption(
            mod: this.Manifest,
            name: I18n.Config_General_PanelPositionX_Name,
            tooltip: I18n.Config_General_PanelPositionX_Description,
            getValue: () => (int)this.Config.PanelPosition.X,
            setValue: value => this.Config.PanelPosition = new(value, this.Config.PanelPosition.Y),
            min: 0
        );
        configMenu.AddNumberOption(
            mod: this.Manifest,
            name: I18n.Config_General_PanelPositionY_Name,
            tooltip: I18n.Config_General_PanelPositionY_Description,
            getValue: () => (int)this.Config.PanelPosition.Y,
            setValue: value => this.Config.PanelPosition = new(this.Config.PanelPosition.X, value),
            min: 0
        );


        // engines
        configMenu.AddSectionTitle(
            mod: this.Manifest,
            text: I18n.Config_Checks_Title
        );
        configMenu.AddBoolOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_Birthdays_Toggle_Name,
            tooltip: I18n.Config_Checks_Birthdays_Toggle_Description,
            getValue: () => this.Config.CheckBirthdays,
            setValue: value => this.Config.CheckBirthdays = value
        );
        configMenu.AddBoolOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_HarvestableCrops_Toggle_Name,
            tooltip: I18n.Config_Checks_HarvestableCrops_Toggle_Description,
            getValue: () => this.Config.CheckHarvestableCrops,
            setValue: value => this.Config.CheckHarvestableCrops = value
        );
        configMenu.AddBoolOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_WaterableCrops_Toggle_Name,
            tooltip: I18n.Config_Checks_WaterableCrops_Toggle_Description,
            getValue: () => this.Config.CheckWaterableCrops,
            setValue: value => this.Config.CheckWaterableCrops = value
        );
        configMenu.AddBoolOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_ReadyMachines_Toggle_Name,
            tooltip: I18n.Config_Checks_ReadyMachines_Toggle_Description,
            getValue: () => this.Config.CheckReadyMachines,
            setValue: value => this.Config.CheckReadyMachines = value
        );
        configMenu.AddBoolOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_ToolPickup_Toggle_Name,
            tooltip: I18n.Config_Checks_ToolPickup_Toggle_Description,
            getValue: () => this.Config.CheckToolPickup,
            setValue: value => this.Config.CheckToolPickup = value
        );
        configMenu.AddBoolOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_BulletinBoard_Toggle_Name,
            tooltip: I18n.Config_Checks_BulletinBoard_Toggle_Description,
            getValue: () => this.Config.CheckDailyQuestBulletinBoard,
            setValue: value => this.Config.CheckDailyQuestBulletinBoard = value
        );
        configMenu.AddBoolOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_SpecialOrdersBoard_Toggle_Name,
            tooltip: I18n.Config_Checks_SpecialOrdersBoard_Toggle_Description,
            getValue: () => this.Config.CheckSpecialOrdersBoard,
            setValue: value => this.Config.CheckSpecialOrdersBoard = value
        );
        configMenu.AddBoolOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_TravelingMerchant_Toggle_Name,
            tooltip: I18n.Config_Checks_TravelingMerchant_Toggle_Description,
            getValue: () => this.Config.CheckTravelingMerchant,
            setValue: value => this.Config.CheckTravelingMerchant = value
        );
        configMenu.AddBoolOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_QueenOfSauce_Toggle_Name,
            tooltip: I18n.Config_Checks_QueenOfSauce_Toggle_Description,
            getValue: () => this.Config.CheckQueenOfSauce,
            setValue: value => this.Config.CheckQueenOfSauce = value
        );
        configMenu.AddBoolOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_GiftingNpcs_Toggle_Name,
            tooltip: I18n.Config_Checks_GiftingNpcs_Toggle_Description,
            getValue: () => this.Config.CheckGiftingNPCs,
            setValue: value => this.Config.CheckGiftingNPCs = value
        );
        configMenu.AddTextOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_GiftingNpcs_Npcs_Name,
            tooltip: I18n.Config_Checks_GiftingNpcs_Npcs_Description,
            getValue: () => this.Config.GiftingNPCsString,
            setValue: value => this.Config.GiftingNPCsString = value
        );

        if (this.Update is not null)
        {
            configMenu.OnFieldChanged(
                mod: this.Manifest,
                onChange: this.Update
            );
        }
    }


    /*********
    ** Private methods
    *********/
    /// <summary>Reset the mod's config to its default values.</summary>
    private void Reset()
    {
        ModConfig defaults = new();

        this.Config.ToggleTodoListKeybind = defaults.ToggleTodoListKeybind;
        this.Config.VisibleItemCount = defaults.VisibleItemCount;
        this.Config.PanelPosition = defaults.PanelPosition;

        this.Config.CheckBirthdays = defaults.CheckBirthdays;
        this.Config.CheckHarvestableCrops = defaults.CheckHarvestableCrops;
        this.Config.CheckWaterableCrops = defaults.CheckWaterableCrops;
        this.Config.CheckReadyMachines = defaults.CheckReadyMachines;
        this.Config.CheckToolPickup = defaults.CheckToolPickup;
        this.Config.CheckDailyQuestBulletinBoard = defaults.CheckDailyQuestBulletinBoard;
        this.Config.CheckSpecialOrdersBoard = defaults.CheckSpecialOrdersBoard;
        this.Config.CheckTravelingMerchant = defaults.CheckTravelingMerchant;
        this.Config.CheckQueenOfSauce = defaults.CheckQueenOfSauce;
        this.Config.CheckGiftingNPCs = defaults.CheckGiftingNPCs;
        this.Config.GiftingNPCsString = defaults.GiftingNPCsString;
    }
}
