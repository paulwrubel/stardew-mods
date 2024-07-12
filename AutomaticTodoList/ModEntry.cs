using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;

namespace AutomaticTodoList
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        /*********
        ** Properties
        *********/
        /// <summary>The mod configuration from the player.</summary>
        private ModConfig Config = null!; // set in Entry()

        private AutomaticTodoListManager AutomaticTodoListManager = null!; // set in Entry()

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            this.Config = Helper.ReadConfig<ModConfig>();
            this.AutomaticTodoListManager = new(this.Config, this.Monitor.Log);

            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
            helper.Events.GameLoop.DayStarted += this.OnDayStarted;
            helper.Events.GameLoop.TimeChanged += this.OnTimeChanged;
            helper.Events.GameLoop.OneSecondUpdateTicked += this.OnOneSecondUpdateTicked;
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
            helper.Events.Input.ButtonsChanged += this.OnButtonsChanged;
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

            this.AutomaticTodoListManager.OnGameLaunched(e);
        }

        /// <summary>Raised after the player begins a new day.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnDayStarted(object? sender, DayStartedEventArgs e)
        {
            this.AutomaticTodoListManager.OnDayStarted(e);
        }

        /// <summary>Raised after the game time changes.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnTimeChanged(object? sender, TimeChangedEventArgs e)
        {
            this.AutomaticTodoListManager.OnTimeChanged(e);
        }

        /// <summary>Raised before/after the game state is updated, once per second.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnOneSecondUpdateTicked(object? sender, OneSecondUpdateTickedEventArgs e)
        {
            this.AutomaticTodoListManager.OnOneSecondUpdateTicked(e);
        }

        /// <summary>Raised after the game state is updated (≈60 times per second).</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnUpdateTicked(object? sender, UpdateTickedEventArgs e)
        {
            this.AutomaticTodoListManager.OnUpdateTicked(e);
        }

        /// <summary>Raised after the player pressed/released any buttons on the keyboard, mouse, or controller. 
        /// This includes mouse clicks. If the player pressed/released multiple keys at once, this is only raised once.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonsChanged(object? sender, ButtonsChangedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
            {
                return;
            }

            this.AutomaticTodoListManager.OnButtonsChanged(e);
        }

        private void OnRendered(object? sender, RenderedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
            {
                return;
            }

            AutomaticTodoListManager.OnRendered(e);
        }

        private void OnMenuChanged(object? sender, MenuChangedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
            {
                return;
            }

            this.AutomaticTodoListManager.OnMenuChanged(e);
        }

        private void OnConfigChanged(string fieldID, object newValue)
        {
            // Empty for now
        }
    }
}
