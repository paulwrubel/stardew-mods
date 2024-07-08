using SmartTodo.Components.TodoItems;
using SmartTodo.Models;
using StardewValley;

namespace SmartTodo.Engines
{
    internal class HarvestableMachinesEngine(
        Action<string, StardewModdingAPI.LogLevel> log,
        Action<List<ITodoItem>>? addNewItems = null,
        Action<ITodoItem>? addToCompletedCache = null,
        Func<ITodoItem, bool>? removeFromCompletedCache = null
    ) : BaseEngine(log)
    {

        private Action<List<ITodoItem>>? AddNewItems { get; } = addNewItems;

        private Action<ITodoItem>? AddToCompletedCache { get; } = addToCompletedCache;

        private Func<ITodoItem, bool>? RemoveFromCompletedCache { get; } = removeFromCompletedCache;

        private readonly List<string> addedLocations = [];

        public override List<ITodoItem> GetTodos()
        {
            List<ITodoItem> items = [];

            // check if there are harvestable machine in various locations
            foreach (GameLocation gameLocation in GameHelper.GetLocations())
            {
                if (gameLocation is not null)
                {
                    bool alreadyAddedToLocation = addedLocations.Contains(gameLocation.Name);
                    if (!alreadyAddedToLocation)
                    {
                        int harvestableCount = gameLocation.getNumberOfMachinesReadyForHarvest();
                        if (harvestableCount > 0)
                        {
                            items.Add(new HarvestableMachinesTodoItem(
                                gameLocation,
                                addToCompletedCache: AddToCompletedCache,
                                removeFromCompletedCache: RemoveFromCompletedCache
                            ));
                            addedLocations.Add(gameLocation.Name);
                        }
                    }
                }
            }

            return items;
        }

        public override void OnTimeChanged()
        {
            base.OnTimeChanged();

            if (AddNewItems is not null)
            {
                AddNewItems(GetTodos());
            }
        }
    }
}