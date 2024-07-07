namespace SmartTodo.Models
{
    public interface IEngine
    {
        List<ITodoItem> GetTodos();
    }
}