// using SmartTodo.Components.TodoItems;
// using SmartTodo.Models;
// using StardewValley;

// namespace SmartTodo.Engines
// {
//     internal class BulletinBoardEngine(
//         Action<string, StardewModdingAPI.LogLevel> log,
//         Action<ITodoItem>? addToCompletedCache = null
//     ) : BaseEngine<BulletinBoardTodoItem>(log, () => false)
//     {

//         private Action<ITodoItem>? AddToCompletedCache { get; } = addToCompletedCache;

//         public override List<ITodoItem> GetTodos()
//         {
//             List<ITodoItem> items = [];

//             // check if any bulletin board has new content
//             if (Game1.CanAcceptDailyQuest())
//             {
//                 items.Add(new BulletinBoardTodoItem(addToCompletedCache: AddToCompletedCache));
//             }

//             return items;
//         }
//     }
// }