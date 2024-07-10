using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SmartTodo.Components;
using SmartTodo.Engines;
using SmartTodo.Models;
using StardewModdingAPI.Events;
using StardewValley;

namespace SmartTodo
{
    /// <summary>Manages the Smart Todo List.</summary>
    internal sealed class SmartTodoManager
    {

        /// <summary>The config for the mod.</summary>
        public ModConfig Config { get; set; }

        private readonly Action<string, StardewModdingAPI.LogLevel> Log;

        private readonly HashSet<IEngine> engines = [];

        private static readonly Vector2 initialSmartTodoPanelPosition = new(10, 100);
        private SmartTodoPanel smartTodoPanel = null!; // initialized in OnGameLaunched

        private bool isPanelOpen = true;

        /// <summary>Initializes a new instance of the <see cref="SmartTodoManager"/> class.</summary>
        public SmartTodoManager(ModConfig config, Action<string, StardewModdingAPI.LogLevel> log)
        {
            this.Config = config;
            this.Log = log;

            this.InitEngines();
        }

        internal void OnGameLaunched(GameLaunchedEventArgs e)
        {
            this.smartTodoPanel = new(
                initialSmartTodoPanelPosition,
                this.GatherItems
            );
        }

        internal void OnDayStarted(DayStartedEventArgs e)
        {
            foreach (IEngine engine in this.engines)
            {
                engine.OnDayStarted(e);
            }
        }

        internal void OnTimeChanged(TimeChangedEventArgs e)
        {
            foreach (IEngine engine in this.engines)
            {
                engine.OnTimeChanged(e);
            }
        }

        internal void OnOneSecondUpdateTicked(OneSecondUpdateTickedEventArgs e)
        {
            foreach (IEngine engine in this.engines)
            {
                engine.OnOneSecondUpdateTicked(e);
            }
        }

        internal void OnUpdateTicked(UpdateTickedEventArgs e)
        {
            foreach (IEngine engine in this.engines)
            {
                engine.OnUpdateTicked(e);
            }
        }

        internal void OnRendered(RenderedEventArgs e)
        {
            if (isPanelOpen && this.smartTodoPanel is not null)
            {
                this.smartTodoPanel.draw(Game1.spriteBatch);
            }
        }

        internal void OnButtonPressed(ButtonPressedEventArgs e)
        {
            if (this.Config.ToggleTodoListKeybind.JustPressed())
            {
                isPanelOpen = !isPanelOpen;
            }
        }

        internal void OnMenuChanged(MenuChangedEventArgs e)
        {
            // Empty for now
        }

        private ICollection<ITodoItem> GatherItems()
        {
            var allItems = this.engines.Aggregate(new List<ITodoItem>(), (accumulatedItems, engine) =>
            {
                if (engine.IsEnabled())
                {
                    accumulatedItems.AddRange(engine.Items);
                }
                return accumulatedItems;
            });

            allItems.Sort((a, b) =>
            {
                int checkedComp = a.IsChecked.CompareTo(b.IsChecked);
                if (checkedComp != 0)
                {
                    return checkedComp;
                }

                int priorityComp = -a.Priority.CompareTo(b.Priority);
                if (priorityComp != 0)
                {
                    return priorityComp;
                }

                return a.Text.CompareTo(b.Text);
            });

            return allItems;
        }

        public void InitEngines(bool forceReset = false)
        {
            if (this.engines.Count > 0 && !forceReset)
            {
                // we already initialized the engines
                return;
            }

            this.engines.Clear();

            this.engines.Add(new TestEngine(Log, () => false));
            this.engines.Add(new BirthdayEngine(Log, () => this.Config.CheckBirthdays));
            this.engines.Add(new HarvestableCropsEngine(Log, () => this.Config.CheckHarvestableCrops));
            this.engines.Add(new WaterableCropsEngine(Log, () => this.Config.CheckWaterableCrops));
            // this.Engines.Add(new HarvestableMachinesEngine(Log, () => this.Config.CheckHarvestableMachines));
            this.engines.Add(new ToolPickupEngine(Log, () => this.Config.CheckToolPickup));
            this.engines.Add(new BulletinBoardEngine(Log, () => this.Config.CheckDailyQuestBulletinBoard));
            this.engines.Add(new SpecialOrdersBoardEngine(Log, () => this.Config.CheckSpecialOrdersBoard));
        }
    }
}
