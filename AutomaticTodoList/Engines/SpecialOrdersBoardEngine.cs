using AutomaticTodoList.Components.TodoItems;
using AutomaticTodoList.Models;
using StardewValley;
using StardewValley.SpecialOrders;

namespace AutomaticTodoList.Engines;

internal class SpecialOrdersBoardEngine(
    Action<string, StardewModdingAPI.LogLevel> log,
    Func<bool> isEnabled
) : BaseEngine<SpecialOrdersBoardTodoItem>(log, isEnabled, Frequency.OnceADay)
{
    public override void UpdateItems()
    {
        SpecialOrderType[] typesToCheck = [
            SpecialOrderType.Standard,
                SpecialOrderType.Qi
        ];

        foreach (SpecialOrderType type in typesToCheck)
        {
            SpecialOrder leftOrder = Game1.player.team.GetAvailableSpecialOrder(0, type.ToStardewSpecialOrderTypeString());
            SpecialOrder rightOrder = Game1.player.team.GetAvailableSpecialOrder(1, type.ToStardewSpecialOrderTypeString());

            bool anyOrderIsAvailable = leftOrder is not null || rightOrder is not null;
            bool alreadyAcceptedOrder = Game1.player.team.acceptedSpecialOrderTypes.Contains(type.ToStardewSpecialOrderTypeString());

            if (anyOrderIsAvailable && !alreadyAcceptedOrder)
            {
                items.Add(new SpecialOrdersBoardTodoItem(type));
            }
        }
    }
}
