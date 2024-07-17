using AutomaticTodoList.Components.TodoItems;
using AutomaticTodoList.Models;
using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Objects;

namespace AutomaticTodoList.Engines;

internal class QueenOfSauceEngine(
    Action<string, StardewModdingAPI.LogLevel> log,
    Func<bool> isEnabled
) : BaseEngine<QueenOfSauceTodoItem>(log, isEnabled, Frequency.OnceADay)
{
    private static readonly VirtualTV TV = new();

    public override void UpdateItems()
    {
        uint daysPlayed = Game1.stats.DaysPlayed;

        // see if recipes even apply yet
        if (daysPlayed < 5)
        {
            return;
        }

        // get the week number
        int weekNum = (int)(daysPlayed % 224U / 7U);
        if (daysPlayed % 224U == 0U)
        {
            weekNum = 32;
        }

        // check the day of the week
        DayOfWeek dayOfWeek = (DayOfWeek)(Game1.dayOfMonth % 7);

        switch (dayOfWeek)
        {
            case DayOfWeek.Sunday:
                // the assigned week is correct since they're just in order
                break;
            case DayOfWeek.Wednesday:
                // we need to run the game's rerun function to see what's on TV today
                if (Game1.player.team.lastDayQueenOfSauceRerunUpdated.Value != Game1.Date.TotalDays)
                {
                    Game1.player.team.lastDayQueenOfSauceRerunUpdated.Set(Game1.Date.TotalDays);
                    Game1.player.team.queenOfSauceRerunWeek.Set(TV.GetRerunWeek());
                }
                weekNum = Game1.player.team.queenOfSauceRerunWeek.Value;
                break;
            default:
                // not a Queen of Sauce day
                return;
        }

        // Dictionary of recipes
        Dictionary<string, string> cookingChannelRecipes = DataLoader.Tv_CookingChannel(Game1.temporaryContent);
        if (!cookingChannelRecipes.TryGetValue($"{weekNum}", out string? translation) || translation is null)
        {
            // couldn't find recipe for some reason
            return;
        }

        // Split the recipe info info
        string recipeName = translation.Split('/')[0];

        if (!Game1.player.cookingRecipes.ContainsKey(recipeName))
        {
            items.Add(new QueenOfSauceTodoItem(recipeName));
        }
    }
}
