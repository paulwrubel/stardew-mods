using SmartTodo.Integrations;
using StardewModdingAPI;

namespace SmartTodo
{
    /// <summary>Registers the mod configuration with Generic Mod Config Menu.</summary>
    /// <remarks>Construct an instance.</remarks>
    /// <param name="manifest">The CJB Cheats Menu manifest.</param>
    /// <param name="modRegistry">An API for fetching metadata about loaded mods.</param>
    /// <param name="config">Get the current mod config.</param>
    /// <param name="save">Save the mod's current config to the <c>config.json</c> file.</param>
    internal class GenericModConfigMenuIntegration(IManifest manifest, IModRegistry modRegistry, ModConfig config, Action save, Action<string, object>? update = null)
    {
        /*********
        ** Fields
        *********/
        /// <summary>The CJB Cheats Menu manifest.</summary>
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
            configMenu.AddKeybindList(
                mod: this.Manifest,
                name: () => "Toggle Smart Todo List",
                tooltip: () => "Start or stop displaying the smart todo list panel in the HUD",
                getValue: () => this.Config.ToggleTodoListKeybind,
                setValue: value => this.Config.ToggleTodoListKeybind = value
            );

            // engines
            configMenu.AddBoolOption(
                mod: this.Manifest,
                name: () => "Check Birthdays",
                tooltip: () => "Whether or not to add todo items for gifting villagers on their birthdays",
                getValue: () => this.Config.CheckBirthdays,
                setValue: value => this.Config.CheckBirthdays = value
            );
            configMenu.AddBoolOption(
                mod: this.Manifest,
                name: () => "Check Harvestable Crops",
                tooltip: () => "Whether or not to add items for harvesting crops in various locations",
                getValue: () => this.Config.CheckHarvestableCrops,
                setValue: value => this.Config.CheckHarvestableCrops = value
            );
            configMenu.AddBoolOption(
                mod: this.Manifest,
                name: () => "Check Waterable Crops",
                tooltip: () => "Whether or not to add items for watering crops in various locations",
                getValue: () => this.Config.CheckWaterableCrops,
                setValue: value => this.Config.CheckWaterableCrops = value
            );
            configMenu.AddBoolOption(
                mod: this.Manifest,
                name: () => "Check Harvestable Machines",
                tooltip: () => "Whether or not to add items for harvesting machines in various locations",
                getValue: () => this.Config.CheckHarvestableMachines,
                setValue: value => this.Config.CheckHarvestableMachines = value
            );
            configMenu.AddBoolOption(
                mod: this.Manifest,
                name: () => "Check Tools Pickup",
                tooltip: () => "Whether or not to add items for picking up upgraded tools from Clint",
                getValue: () => this.Config.CheckToolPickup,
                setValue: value => this.Config.CheckToolPickup = value
            );
            configMenu.AddBoolOption(
                mod: this.Manifest,
                name: () => "Check Bulletin Board",
                tooltip: () => "Whether or not to add todo items to accept the daily quest from the bulletin board outside Pierre's General Store",
                getValue: () => this.Config.CheckDailyQuestBulletinBoard,
                setValue: value => this.Config.CheckDailyQuestBulletinBoard = value
            );
            configMenu.AddBoolOption(
                mod: this.Manifest,
                name: () => "Check Special Orders Boards",
                tooltip: () => "Whether or not to add todo items to accept special orders from special orders boards",
                getValue: () => this.Config.CheckSpecialOrdersBoard,
                setValue: value => this.Config.CheckSpecialOrdersBoard = value
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

            this.Config.CheckBirthdays = defaults.CheckBirthdays;
            this.Config.CheckHarvestableCrops = defaults.CheckHarvestableCrops;
            this.Config.CheckWaterableCrops = defaults.CheckWaterableCrops;
            this.Config.CheckHarvestableMachines = defaults.CheckHarvestableMachines;
            this.Config.CheckToolPickup = defaults.CheckToolPickup;
        }
    }
}
