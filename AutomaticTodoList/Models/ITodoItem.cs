using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI.Events;

namespace AutomaticTodoList.Models
{
    public interface ITodoItem
    {
        string Text { get; set; }

        bool IsChecked { get; set; }

        int Priority { get; set; }

        void OnDayStarted(DayStartedEventArgs e);

        void OnTimeChanged(TimeChangedEventArgs e);

        void OnOneSecondUpdateTicked(OneSecondUpdateTickedEventArgs e);

        void OnUpdateTicked(UpdateTickedEventArgs e);

        void Draw(SpriteBatch b, Vector2 position);


    }
}