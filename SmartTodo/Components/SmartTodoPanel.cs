using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SmartTodo.Models;
using StardewValley;
using StardewValley.Menus;

namespace SmartTodo.Components
{
    /// <summary>Manages the Smart Todo List.</summary>
    /// <remarks>Initializes a new instance of the <see cref="SmartTodoPanel"/> class.</remarks>
    internal class SmartTodoPanel(Vector2 position, Func<ICollection<ITodoItem>> getItems) : IClickableMenu
    {
        private static string TitleText { get; } = "Todo List";

        private static string DividerText { get; } = "----------";

        private Vector2 BoxPosition = position;
        private readonly int GutterLength = 4 * Game1.pixelZoom;

        private int LineSpacing { get; } = 2;

        public override void draw(SpriteBatch b)
        {
            var items = getItems();

            var spriteFont = Game1.smallFont;

            // find the longest text
            int maxWidth = (int)spriteFont.MeasureString(TitleText).X;
            foreach (ITodoItem item in items)
            {
                int todoItemWidth = (int)spriteFont.MeasureString(item.Text).X;
                if (todoItemWidth > maxWidth)
                {
                    maxWidth = todoItemWidth;
                }
            }

            int width = maxWidth + (GutterLength * 2);
            int lineHeight = (int)spriteFont.MeasureString(TitleText).Y;
            int height = (GutterLength * 2) + (items.Count + 1) * (lineHeight + LineSpacing) + 4;


            this.DrawTextureBox(width, height);
            this.DrawTitleText(maxWidth, out int nextYPosition);
            this.DrawTodoItems(nextYPosition, items);

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
        private void DrawTitleText(int totalWidth, out int nextYPosition)
        {
            var spriteBatch = Game1.spriteBatch;
            var titleFont = Game1.smallFont;

            Vector2 titleTextSize = titleFont.MeasureString(TitleText);

            int xOffset = (totalWidth - (int)titleTextSize.X) / 2;

            var titleTextPosition = new Vector2(BoxPosition.X + GutterLength + xOffset, BoxPosition.Y + GutterLength);

            Utility.drawTextWithShadow(spriteBatch, TitleText, titleFont, titleTextPosition, Game1.textColor);

            var dividerPosition = new Vector2(BoxPosition.X + GutterLength, titleTextPosition.Y + (int)titleTextSize.Y + LineSpacing);

            Utility.drawLineWithScreenCoordinates(
                (int)dividerPosition.X, (int)dividerPosition.Y,
                (int)dividerPosition.X + totalWidth, (int)dividerPosition.Y,
                spriteBatch,
                Game1.textColor,
                thickness: 1
            );
            // Utility.drawTextWithShadow(spriteBatch, DividerText, spriteFont, dividerPosition, Game1.textColor);

            nextYPosition = (int)dividerPosition.Y + (int)4 + this.LineSpacing;
        }

        private void DrawTodoItems(int initialYPosition, ICollection<ITodoItem> items)
        {
            var spriteBatch = Game1.spriteBatch;
            var spriteFont = Game1.smallFont;

            var currentPosition = new Vector2(BoxPosition.X + GutterLength, initialYPosition);

            foreach (ITodoItem item in items)
            {
                item.Draw(spriteBatch, currentPosition);
                currentPosition.Y += (int)spriteFont.MeasureString(item.Text).Y + this.LineSpacing;
            }
        }
    }
}
