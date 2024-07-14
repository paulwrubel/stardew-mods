using AutomaticTodoList.Models;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;

namespace AutomaticTodoList.Components.TodoItems;

/// <summary>A TravelingMerchantTodoItem todo item.</summary>
/// <remarks>Initializes a new instance of the <see cref="TravelingMerchantTodoItem"/> class.</remarks>
/// <param name="text">The text of the todo item.</param>
internal class TravelingMerchantTodoItem(bool isChecked = false)
    : BaseTodoItem(isChecked, TaskPriority.TravelingMerchant)
{
    public override string Text()
    {
        return I18n.Items_TravelingMerchant_Text();
    }

    public override void OnMenuChanged(MenuChangedEventArgs e)
    {
        if (!IsChecked)
        {
            if (e.NewMenu is ShopMenu shopMenu && shopMenu.ShopId == Game1.shop_travelingCart)
            {
                this.MarkCompleted();
            }
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is TravelingMerchantTodoItem;
    }

    public override int GetHashCode()
    {
        return this.GetType().GetHashCode();
    }
}
