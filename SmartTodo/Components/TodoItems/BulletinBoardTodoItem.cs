// using SmartTodo.Models;
// using StardewValley;

// namespace SmartTodo.Components.TodoItems
// {
//     /// <summary>A BulletinBoardTodoItem todo item.</summary>
//     /// <remarks>Initializes a new instance of the <see cref="BulletinBoardTodoItem"/> class.</remarks>
//     /// <param name="text">The text of the todo item.</param>
//     internal class BulletinBoardTodoItem(
//         bool isChecked = false,
//         Action<ITodoItem>? addToCompletedCache = null
//     ) : BaseTodoItem(
//         "Check the daily quest bulletin board",
//         isChecked,
//         10,
//         addToCompletedCache
//     )
//     {
//         public override void OnUpdateTicked()
//         {
//             if (!IsChecked)
//             {
//                 if (Game1.player.acceptedDailyQuest.Value)
//                 {
//                     this.MarkCompleted();
//                 }
//             }
//         }
//     }
// }