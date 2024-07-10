using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;

namespace SmartTodo
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        /*********
        ** Properties
        *********/
        /// <summary>The mod configuration from the player.</summary>
        private ModConfig Config = null!; // set in Entry()

        private SmartTodoManager SmartTodoManager = null!; // set in Entry()

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            this.Config = Helper.ReadConfig<ModConfig>();
            this.SmartTodoManager = new(this.Config, this.Monitor.Log);

            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
            helper.Events.GameLoop.DayStarted += this.OnDayStarted;
            helper.Events.GameLoop.TimeChanged += this.OnTimeChanged;
            helper.Events.GameLoop.OneSecondUpdateTicked += this.OnOneSecondUpdateTicked;
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            helper.Events.Display.Rendered += this.OnRendered;
            helper.Events.Display.MenuChanged += this.OnMenuChanged;
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the game is launched, right before the first update tick. This happens once per game session (unrelated to loading saves). All mods are loaded and initialised at this point, so this is a good time to set up mod integrations.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
        {
            var configMenu = new GenericModConfigMenuIntegration(
                manifest: this.ModManifest,
                modRegistry: this.Helper.ModRegistry,
                config: this.Config,
                save: () => this.Helper.WriteConfig(this.Config),
                update: this.OnConfigChanged
            );
            configMenu.Register();

            this.SmartTodoManager.OnGameLaunched(e);
        }

        /// <summary>Raised after the player begins a new day.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnDayStarted(object? sender, DayStartedEventArgs e)
        {
            this.SmartTodoManager.OnDayStarted(e);
        }

        /// <summary>Raised after the game time changes.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnTimeChanged(object? sender, TimeChangedEventArgs e)
        {
            this.SmartTodoManager.OnTimeChanged(e);
        }

        /// <summary>Raised before/after the game state is updated, once per second.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnOneSecondUpdateTicked(object? sender, OneSecondUpdateTickedEventArgs e)
        {
            this.SmartTodoManager.OnOneSecondUpdateTicked(e);
        }

        /// <summary>Raised after the game state is updated (≈60 times per second).</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnUpdateTicked(object? sender, UpdateTickedEventArgs e)
        {
            this.SmartTodoManager.OnUpdateTicked(e);
        }

        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
            {
                return;
            }

            this.SmartTodoManager.OnButtonPressed(e);
        }

        private void OnRendered(object? sender, RenderedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
            {
                return;
            }

            SmartTodoManager.OnRendered(e);
        }

        private void OnMenuChanged(object? sender, MenuChangedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
            {
                return;
            }

            this.SmartTodoManager.OnMenuChanged(e);
        }

        private void OnConfigChanged(string fieldID, object newValue)
        {
            // Empty for now
        }
    }
}
