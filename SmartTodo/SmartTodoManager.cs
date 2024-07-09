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
        // private readonly List<ITodoItem> Items = [];

        // private readonly List<ITodoItem> CompletedItemsCache = [];

        // private int GutterLength { get; }

        private bool isPanelOpen = true;

        /// <summary>Initializes a new instance of the <see cref="SmartTodoManager"/> class.</summary>
        public SmartTodoManager(ModConfig config, Action<string, StardewModdingAPI.LogLevel> log)
        {
            this.Config = config;
            this.Log = log;

            this.InitEngines();
        }

        internal void OnGameLaunched()
        {
            this.smartTodoPanel = new(
                initialSmartTodoPanelPosition,
                this.GatherItems
            );
        }

        internal void OnDayStarted()
        {
            foreach (IEngine engine in this.engines)
            {
                engine.OnDayStarted();
            }
            // this.ResetEngines();
            // this.ClearAndRecheckForItems(reAddCompleted: false);
            // this.CompletedItemsCache.Clear();
        }

        internal void OnTimeChanged()
        {
            foreach (IEngine engine in this.engines)
            {
                engine.OnTimeChanged();
            }

            // foreach (ITodoItem item in Items)
            // {
            //     item.OnTimeChanged();
            // }

            // this.ClearAndRecheckForItems();
        }

        internal void OnUpdateTicked()
        {
            foreach (IEngine engine in this.engines)
            {
                engine.OnUpdateTicked();
            }

            // foreach (ITodoItem item in Items)
            // {
            //     item.OnUpdateTicked();
            // }
        }

        internal void OnRendered()
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
                // if (IsPanelOpen)
                // {
                //     this.RecheckEngines();
                // }
            }
        }

        internal void OnMenuChanged(MenuChangedEventArgs e)
        {
            // Empty for now
        }

        private ICollection<ITodoItem> GatherItems()
        {
            var gatheredItems = this.engines.Aggregate(new List<ITodoItem>(), (gatheredItems, engine) =>
            {
                if (engine.IsEnabled())
                {
                    gatheredItems.AddRange(engine.Items);
                }
                return gatheredItems;
            });

            gatheredItems.Sort((a, b) =>
            {
                var comp = b.Priority.CompareTo(a.Priority);
                if (comp == 0)
                {
                    comp = a.Text.CompareTo(b.Text);
                }
                return comp;
            });

            return gatheredItems;
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
            // this.Engines.Add(new ToolPickupEngine(Log, () => this.Config.CheckToolPickup));
            // this.Engines.Add(new BulletinBoardEngine(Log, () => this.Config.CheckDailyQuestBulletinBoard));
            // this.Engines.Add(new SpecialOrdersBoardEngine(Log, () => this.Config.CheckSpecialOrdersBoard));


            //  // if (this.Config.CheckBirthdays)
            // // {
            // //     // this.Engines.Add(new BirthdayEngine(Log, this.CompletedItemsCache.Add));
            // this.Engines.Add(new BirthdayEngine(Log, () => this.Config.CheckBirthdays));
            // // }

            // // if (this.Config.CheckHarvestableCrops)
            // // {
            // // this.Engines.Add(new HarvestableCropsEngine(Log, this.CompletedItemsCache.Add));
            // this.Engines.Add(new HarvestableCropsEngine(Log, () => this.Config.CheckHarvestableCrops));
            // // }

            // // if (this.Config.CheckHarvestableCrops)
            // // {
            // // this.Engines.Add(new WaterableCropsEngine(Log, this.CompletedItemsCache.Add));
            // this.Engines.Add(new WaterableCropsEngine(Log, () => this.Config.CheckHarvestableCrops));
            // // }

            // // if (this.Config.CheckHarvestableMachines)
            // // {
            // // void addAndSort(List<ITodoItem> newItems)
            // // {
            // //     this.Items.AddRange(newItems);
            // //     SortItems();
            // // }

            // // this.Engines.Add(new HarvestableMachinesEngine(Log, addAndSort, this.CompletedItemsCache.Add, this.CompletedItemsCache.Remove));
            // this.Engines.Add(new HarvestableMachinesEngine(Log, () => this.Config.CheckHarvestableMachines));
            // // }

            // // if (this.Config.CheckToolPickup)
            // // {
            // // this.Engines.Add(new ToolPickupEngine(Log, this.CompletedItemsCache.Add));
            // this.Engines.Add(new ToolPickupEngine(Log, () => this.Config.CheckToolPickup));
            // // }

            // // if (this.Config.CheckDailyQuestBulletinBoard)
            // // {
            // // this.Engines.Add(new BulletinBoardEngine(Log, this.CompletedItemsCache.Add));
            // this.Engines.Add(new BulletinBoardEngine(Log, () => this.Config.CheckDailyQuestBulletinBoard));
            // // }

            // // if (this.Config.CheckSpecialOrdersBoard)
            // // {
            // // this.Engines.Add(new SpecialOrdersBoardEngine(Log, this.CompletedItemsCache.Add));
            // this.Engines.Add(new SpecialOrdersBoardEngine(Log, () => this.Config.CheckSpecialOrdersBoard));
            // // }
        }

        // public void ClearAndRecheckForItems(bool reAddCompleted = true)
        // {
        //     Items.Clear();
        //     foreach (IEngine engine in this.Engines)
        //     {
        //         Items.AddRange(engine.GetTodos());
        //     }

        //     if (reAddCompleted)
        //     {
        //         Items.AddRange(this.CompletedItemsCache);
        //     }

        //     SortItems();

        //     this.SmartTodoPanel = new(new Vector2(10, 100), () => Items);
        // }

        // private void SortItems()
        // {
        //     Items.Sort((a, b) =>
        //     {
        //         var comp = b.Priority.CompareTo(a.Priority);
        //         if (comp == 0)
        //         {
        //             comp = a.Text.CompareTo(b.Text);
        //         }
        //         return comp;
        //     });
        // }
    }
}
