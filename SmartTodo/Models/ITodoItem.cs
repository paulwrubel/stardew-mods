using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SmartTodo.Models
{
    public interface ITodoItem
    {
        string Text { get; set; }

        bool IsChecked { get; set; }

        void Draw(SpriteBatch b, Vector2 position);

        void OnUpdateTicked();
    }
}