// using SmartTodo.Components.TodoItems;
// using SmartTodo.Models;
// using StardewValley;
// using StardewValley.SpecialOrders;

// namespace SmartTodo.Engines
// {
//     internal class SpecialOrdersBoardEngine(
//         Action<string, StardewModdingAPI.LogLevel> log,
//         Action<ITodoItem>? addToCompletedCache = null
//     ) : BaseEngine(log, () => false)
//     {

//         private Action<ITodoItem>? AddToCompletedCache { get; } = addToCompletedCache;

//         public override List<ITodoItem> GetTodos()
//         {
//             List<ITodoItem> items = [];

//             SpecialOrderType[] typesToCheck = [
//                 SpecialOrderType.Standard,
//                 SpecialOrderType.Qi
//             ];

//             foreach (SpecialOrderType type in typesToCheck)
//             {
//                 SpecialOrder leftOrder = Game1.player.team.GetAvailableSpecialOrder(0, type.ToStardewSpecialOrderTypeString());
//                 SpecialOrder rightOrder = Game1.player.team.GetAvailableSpecialOrder(1, type.ToStardewSpecialOrderTypeString());

//                 bool anyOrderIsAvailable = leftOrder is not null || rightOrder is not null;
//                 bool alreadyAcceptedOrder = Game1.player.team.acceptedSpecialOrderTypes.Contains(type.ToStardewSpecialOrderTypeString());

//                 if (anyOrderIsAvailable && !alreadyAcceptedOrder)
//                 {
//                     items.Add(new SpecialOrdersBoardTodoItem(
//                         type: type,
//                         addToCompletedCache: AddToCompletedCache
//                     ));
//                 }
//             }

//             return items;
//         }
//     }
// }