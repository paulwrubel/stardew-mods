using System.ComponentModel;
using AutomaticTodoList.Models;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.SpecialOrders;

namespace AutomaticTodoList.Components.TodoItems;

/// <summary>A SpecialOrdersBoardTodoItem todo item.</summary>
/// <remarks>Initializes a new instance of the <see cref="SpecialOrdersBoardTodoItem"/> class.</remarks>
/// <param name="text">The text of the todo item.</param>
internal class SpecialOrdersBoardTodoItem(SpecialOrderType specialOrderType, bool isChecked = false)
    : BaseTodoItem(isChecked, TaskPriority.SpecialOrders)
{
    public SpecialOrderType SpecialOrderType { get; } = specialOrderType;

    public override string Text()
    {
        return SpecialOrderType switch
        {
            SpecialOrderType.Standard => I18n.Items_SpecialOrdersBoard_Standard_Text(),
            SpecialOrderType.Qi => I18n.Items_SpecialOrdersBoard_Qi_Text(),
            _ => throw new InvalidEnumArgumentException(nameof(SpecialOrderType), (int)SpecialOrderType, typeof(SpecialOrderType))
        };
    }

    public override void OnUpdateTicked(UpdateTickedEventArgs e)
    {
        if (!IsChecked)
        {
            SpecialOrder leftOrder = Game1.player.team.GetAvailableSpecialOrder(0, SpecialOrderType.ToStardewSpecialOrderTypeString());
            SpecialOrder rightOrder = Game1.player.team.GetAvailableSpecialOrder(1, SpecialOrderType.ToStardewSpecialOrderTypeString());

            bool noOrderIsAvailable = leftOrder is null && rightOrder is null;
            bool alreadyAcceptedOrder = Game1.player.team.acceptedSpecialOrderTypes.Contains(SpecialOrderType.ToStardewSpecialOrderTypeString());

            if (noOrderIsAvailable || alreadyAcceptedOrder)
            {
                this.MarkCompleted();
            }
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is SpecialOrdersBoardTodoItem otherItem && this.SpecialOrderType == otherItem.SpecialOrderType;
    }

    public override int GetHashCode()
    {
        return (this.GetType(), this.SpecialOrderType).GetHashCode();
    }
}
