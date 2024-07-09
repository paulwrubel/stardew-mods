// using SmartTodo.Components.TodoItems;
// using SmartTodo.Models;
// using StardewValley;

// namespace SmartTodo.Engines
// {
//     internal class HarvestableCropsEngine(
//         Action<string, StardewModdingAPI.LogLevel> log,
//         Action<ITodoItem>? addToCompletedCache = null
//     ) : BaseEngine(log, () => false)
//     {
//         private Action<ITodoItem>? AddToCompletedCache { get; } = addToCompletedCache;

//         public override List<ITodoItem> GetTodos()
//         {
//             List<ITodoItem> items = [];

//             // check if there are harvestable crops in various locations
//             foreach (GameLocation gameLocation in GameHelper.GetLocations())
//             {
//                 if (gameLocation is not null)
//                 {
//                     int harvestableCount = gameLocation.getTotalCropsReadyForHarvest();
//                     if (harvestableCount > 0)
//                     {
//                         items.Add(new HarvestableCropsTodoItem(gameLocation, addToCompletedCache: AddToCompletedCache));
//                     }
//                 }
//             }

//             return items;
//         }
//     }
// }