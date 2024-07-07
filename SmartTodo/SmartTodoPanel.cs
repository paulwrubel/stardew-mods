using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Menus;
using StardewValley.SDKs.Steam;
using xTile.Format;
namespace SmartTodo
{
    /// <summary>Manages the Smart Todo List.</summary>
    internal class SmartTodoPanel : IClickableMenu
    {
        private static string TitleText { get; } = "Smart Todo List";

        private static string DividerText { get; } = "----------";

        private Vector2 BoxPosition;
        private readonly int GutterLength;

        private int LineSpacing { get; } = 2;

        private TodoItem[] Items { get; }


        /// <summary>Initializes a new instance of the <see cref="SmartTodoPanel"/> class.</summary>
        public SmartTodoPanel(Vector2 position, TodoItem[] todoItems)
        {
            this.BoxPosition = position;
            this.GutterLength = 4 * Game1.pixelZoom;

            Items = todoItems;
        }

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

            int lineHeight = (int)spriteFont.MeasureString(TitleText).Y;
            int height = (GutterLength * 2) + (Items.Length + 2) * (lineHeight + LineSpacing);

            this.DrawTextureBox(maxWidth, height);
            this.DrawTitleText(out int nextYPosition);
            this.DrawTodoItems(nextYPosition);

            base.draw(b);
        }

        private void DrawTextureBox(int width, int height)
        {
            int outerWidth = width + 2 * GutterLength;
            int outerHeight = height + Game1.tileSize / 3;

            drawTextureBox(
                Game1.spriteBatch,
                Game1.menuTexture,
                new Rectangle(0, 256, 60, 60),
                (int)BoxPosition.X,
                (int)BoxPosition.Y,
                outerWidth,
                outerHeight + Game1.tileSize / 16,
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
            var dividerPosition = new Vector2(BoxPosition.X + GutterLength, BoxPosition.Y + GutterLength + titleTextHeight + LineSpacing);
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