using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;

namespace AutomaticTodoList.Components.UI;

internal class TextRow(string text, Vector2 position)
{
    public void Draw(SpriteBatch b)
    {
        Utility.drawTextWithShadow(b, text, Game1.smallFont, position, Game1.textColor);
    }
}