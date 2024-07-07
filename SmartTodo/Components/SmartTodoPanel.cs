using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;

namespace SmartTodo.Components
{
    /// <summary>Manages the Smart Todo List.</summary>
    /// <remarks>Initializes a new instance of the <see cref="SmartTodoPanel"/> class.</remarks>
    internal class SmartTodoPanel(Vector2 position, TodoItem[] todoItems) : IClickableMenu
    {
        private static string TitleText { get; } = "Smart Todo List";

        private static string DividerText { get; } = "----------";

        private Vector2 BoxPosition = position;
        private readonly int GutterLength = 4 * Game1.pixelZoom;

        private int LineSpacing { get; } = 2;

        private TodoItem[] Items { get; } = todoItems;

        public override void draw(SpriteBatch b)
        {

            var spriteFont = Game1.smallFont;

            // find the longest text
            int maxWidth = (int)spriteFont.MeasureString(TitleText).X;
            foreach (TodoItem item in Items)
            {
                int todoItemWidth = (int)spriteFont.MeasureString(item.Text).X;
                if (todoItemWidth > maxWidth)
                {
                    maxWidth = todoItemWidth;
                }
            }

            int width = maxWidth + (GutterLength * 2);
            int lineHeight = (int)spriteFont.MeasureString(TitleText).Y;
            int height = (GutterLength * 2) + (Items.Length + 2) * (lineHeight + LineSpacing);


            this.DrawTextureBox(width, height);
            this.DrawTitleText(out int nextYPosition);
            this.DrawTodoItems(nextYPosition);

            base.draw(b);
        }

        private void DrawTextureBox(int width, int height)
        {

            drawTextureBox(
                Game1.spriteBatch,
                Game1.menuTexture,
                new Rectangle(0, 256, 60, 60),
                (int)BoxPosition.X,
                (int)BoxPosition.Y,
                width,
                height,
                Color.White * 1
            );
        }
        private void DrawTitleText(out int nextYPosition)
        {
            var spriteBatch = Game1.spriteBatch;
            var spriteFont = Game1.smallFont;

            var titleTextPosition = new Vector2(BoxPosition.X + GutterLength, BoxPosition.Y + GutterLength);

            Utility.drawTextWithShadow(spriteBatch, TitleText, spriteFont, titleTextPosition, Game1.textColor);

            int titleTextHeight = (int)spriteFont.MeasureString(TitleText).Y;
            var dividerPosition = new Vector2(titleTextPosition.X, titleTextPosition.Y + titleTextHeight + LineSpacing);
            Utility.drawTextWithShadow(spriteBatch, DividerText, spriteFont, dividerPosition, Game1.textColor);

            nextYPosition = (int)dividerPosition.Y + (int)spriteFont.MeasureString(DividerText).Y + this.LineSpacing;
        }

        private void DrawTodoItems(int initialYPosition)
        {
            var spriteBatch = Game1.spriteBatch;
            var spriteFont = Game1.smallFont;

            var currentPosition = new Vector2(BoxPosition.X + GutterLength, initialYPosition);

            foreach (TodoItem item in Items)
            {
                item.draw(spriteBatch, currentPosition);
                currentPosition.Y += (int)spriteFont.MeasureString(item.Text).Y + this.LineSpacing;
            }
        }
    }
}
