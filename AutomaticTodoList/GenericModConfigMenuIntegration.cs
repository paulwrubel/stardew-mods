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
            text: I18n.Config_Controls_Title
        );
        configMenu.AddKeybindList(
            mod: this.Manifest,
            name: I18n.Config_Controls_ToggleKeybind_Name,
            tooltip: I18n.Config_Controls_ToggleKeybind_Description,
            getValue: () => this.Config.ToggleTodoList,
            setValue: value => this.Config.ToggleTodoList = value
        );

        // engines
        configMenu.AddSectionTitle(
            mod: this.Manifest,
            text: I18n.Config_Checks_Title
        );
        configMenu.AddBoolOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_Birthdays_Name,
            tooltip: I18n.Config_Checks_Birthdays_Description,
            getValue: () => this.Config.CheckBirthdays,
            setValue: value => this.Config.CheckBirthdays = value
        );
        configMenu.AddBoolOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_HarvestableCrops_Name,
            tooltip: I18n.Config_Checks_HarvestableCrops_Description,
            getValue: () => this.Config.CheckHarvestableCrops,
            setValue: value => this.Config.CheckHarvestableCrops = value
        );
        configMenu.AddBoolOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_WaterableCrops_Name,
            tooltip: I18n.Config_Checks_WaterableCrops_Description,
            getValue: () => this.Config.CheckWaterableCrops,
            setValue: value => this.Config.CheckWaterableCrops = value
        );
        configMenu.AddBoolOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_ReadyMachines_Name,
            tooltip: I18n.Config_Checks_ReadyMachines_Description,
            getValue: () => this.Config.CheckReadyMachines,
            setValue: value => this.Config.CheckReadyMachines = value
        );
        configMenu.AddBoolOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_ToolPickup_Name,
            tooltip: I18n.Config_Checks_ToolPickup_Description,
            getValue: () => this.Config.CheckToolPickup,
            setValue: value => this.Config.CheckToolPickup = value
        );
        configMenu.AddBoolOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_BulletinBoard_Name,
            tooltip: I18n.Config_Checks_BulletinBoard_Description,
            getValue: () => this.Config.CheckDailyQuestBulletinBoard,
            setValue: value => this.Config.CheckDailyQuestBulletinBoard = value
        );
        configMenu.AddBoolOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_SpecialOrdersBoard_Name,
            tooltip: I18n.Config_Checks_SpecialOrdersBoard_Description,
            getValue: () => this.Config.CheckSpecialOrdersBoard,
            setValue: value => this.Config.CheckSpecialOrdersBoard = value
        );
        configMenu.AddBoolOption(
            mod: this.Manifest,
            name: I18n.Config_Checks_TravelingMerchant_Name,
            tooltip: I18n.Config_Checks_TravelingMerchant_Description,
            getValue: () => this.Config.CheckTravelingMerchant,
            setValue: value => this.Config.CheckTravelingMerchant = value
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

        this.Config.ToggleTodoList = defaults.ToggleTodoList;

        this.Config.CheckBirthdays = defaults.CheckBirthdays;
        this.Config.CheckHarvestableCrops = defaults.CheckHarvestableCrops;
        this.Config.CheckWaterableCrops = defaults.CheckWaterableCrops;
        this.Config.CheckReadyMachines = defaults.CheckReadyMachines;
        this.Config.CheckToolPickup = defaults.CheckToolPickup;
        this.Config.CheckDailyQuestBulletinBoard = defaults.CheckDailyQuestBulletinBoard;
        this.Config.CheckSpecialOrdersBoard = defaults.CheckSpecialOrdersBoard;
        this.Config.CheckTravelingMerchant = defaults.CheckTravelingMerchant;
    }
}
