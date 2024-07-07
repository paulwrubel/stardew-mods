using SmartTodo.Components.TodoItems;
using SmartTodo.Models;

namespace SmartTodo.Engines
{
    internal class TestEngine(Action<ITodoItem>? addToCompletedCache = null) : IEngine
    {

        private Action<ITodoItem>? AddToCompletedCache { get; } = addToCompletedCache;

        public List<ITodoItem> GetTodos()
        {
            return new List<ITodoItem>([
                // new TestTodoItem("TEST: Go to the store", addToCompletedCache: AddToCompletedCache),
                // new TestTodoItem("TEST: Give Morris a present (birthday)", addToCompletedCache: AddToCompletedCache),
                // new TestTodoItem("TEST: Sleep!", addToCompletedCache: AddToCompletedCache)
            ]);
        }
    }
}