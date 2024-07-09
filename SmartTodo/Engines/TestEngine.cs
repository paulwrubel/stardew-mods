// using SmartTodo.Components.TodoItems;
// using SmartTodo.Models;

// namespace SmartTodo.Engines
// {
//     internal class TestEngine(
//         Action<string, StardewModdingAPI.LogLevel> log,
//         Action<ITodoItem>? addToCompletedCache = null
//     ) : BaseEngine(log, () => false)
//     {

//         private Action<ITodoItem>? AddToCompletedCache { get; } = addToCompletedCache;

//         public override List<ITodoItem> GetTodos()
//         {
//             return new List<ITodoItem>([
//                 new TestTodoItem("TEST: Go to the store", addToCompletedCache: AddToCompletedCache),
//                 new TestTodoItem("TEST: Give Morris a present (birthday)", addToCompletedCache: AddToCompletedCache),
//                 new TestTodoItem("TEST: Sleep!", addToCompletedCache: AddToCompletedCache)
//             ]);
//         }
//     }
// }