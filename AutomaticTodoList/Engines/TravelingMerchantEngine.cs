using AutomaticTodoList.Components.TodoItems;
using AutomaticTodoList.Models;
using StardewValley;
using StardewValley.Locations;

namespace AutomaticTodoList.Engines
{
    internal class TravelingMerchantEngine(
        Action<string, StardewModdingAPI.LogLevel> log,
        Func<bool> isEnabled
    ) : BaseEngine<TravelingMerchantTodoItem>(log, isEnabled, Frequency.OnceADay)
    {
        public override void UpdateItems()
        {
            // check if it's a traveling merchant day
            if (((Forest)Game1.getLocationFromName("Forest")).ShouldTravelingMerchantVisitToday())
            {
                items.Add(new TravelingMerchantTodoItem());
            }
        }
    }
}