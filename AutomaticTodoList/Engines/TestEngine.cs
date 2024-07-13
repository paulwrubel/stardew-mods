using AutomaticTodoList.Components.TodoItems;
using AutomaticTodoList.Models;

namespace AutomaticTodoList.Engines;

internal class TestEngine(
    Action<string, StardewModdingAPI.LogLevel> log,
    Func<bool> isEnabled
) : BaseEngine<TestTodoItem>(log, isEnabled, Frequency.OnceADay)
{
    private static readonly List<TestTodoItem> testTodoItems = [
        new TestTodoItem("TEST: Go to the store"),
            new TestTodoItem("TEST: Give Morris a present (birthday)"),
            new TestTodoItem("TEST: Sleep!")
    ];

    public override void UpdateItems()
    {
        foreach (TestTodoItem item in testTodoItems)
        {
            if (items.Any(i => i.Text == item.Text))
            {
                continue;
            }

            items.Add(item);
        }
    }
}
