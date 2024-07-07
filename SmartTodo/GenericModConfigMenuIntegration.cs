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
    internal class GenericModConfigMenuIntegration(IManifest manifest, IModRegistry modRegistry, ModConfig config, Action save)
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
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Reset the mod's config to its default values.</summary>
        private void Reset()
        {
            ModConfig defaults = new();

            this.Config.ToggleTodoListKeybind = defaults.ToggleTodoListKeybind;
        }
    }
}
