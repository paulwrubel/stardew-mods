using Microsoft.Xna.Framework;
using AutomaticTodoList.Models;
using StardewValley;
using StardewValley.Menus;
using Microsoft.Xna.Framework.Graphics;

namespace AutomaticTodoList.Components.UI;

/// <summary>Manages the Automatic Todo List Panel UI.</summary>
/// <remarks>Initializes a new instance of the <see cref="AutomaticTodoListPanel"/> class.</remarks>
internal class AutomaticTodoListPanel(
    Func<int> visibleItemCount,
    Func<ICollection<ITodoItem>> getItems
)
{
    private readonly string TitleText = I18n.Panel_Title();
    private const int GutterLength = 4 * Game1.pixelZoom;

    private const int LineSpacing = 2;

    private static readonly SpriteFont Font = Game1.smallFont;

    public void Draw(SpriteBatch b, Vector2 position)
    {
        var items = getItems();

        bool showOverflowIndicator = items.Count > visibleItemCount();

        // find the longest text, which determines the width of the panel
        int maxTextWidth = (int)Font.MeasureString(TitleText).X;
        foreach (ITodoItem item in items)
        {
            int todoItemWidth = (int)Font.MeasureString(item.Text()).X;
            if (todoItemWidth > maxTextWidth)
            {
                maxTextWidth = todoItemWidth;
            }
        }

        int numRows =
            1 + // the title row
            Math.Min(items.Count, visibleItemCount()) + // the todo items
            (showOverflowIndicator ? 1 : 0); // the optional overflow indicator

        // draw the surrounding box
        DrawTextureBox(b, position, maxTextWidth, numRows, out Vector2 titlePosition);

        // draw the title text and dividing line
        DrawTitleTextAndDividingLine(b, titlePosition, maxTextWidth, out Vector2 todoItemPosition);

        // draw the todo items
        DrawTodoItems(b, todoItemPosition, items, out Vector2 overflowIndicatorPosition);

        // draw the overflow indicator
        if (showOverflowIndicator)
        {
            DrawOverflowIndicator(b, overflowIndicatorPosition, items.Count - visibleItemCount());
        }
    }

    private void DrawTextureBox(SpriteBatch b, Vector2 position, int maxTextWidth, int numRows, out Vector2 nextContentPosition)
    {
        // assume the size of each text line
        int lineHeight = (int)Font.MeasureString(TitleText).Y;

        // find the dimensions of the content inside the panel
        Vector2 contentDimensions = new(
            maxTextWidth,
            4 + // the dividing line between the title and the items
            numRows * (lineHeight + LineSpacing) // each todo item + title text + overflow indicator
        );

        // add the border dimensions
        Vector2 dimensions = contentDimensions + new Vector2(GutterLength * 2, GutterLength * 2);

        // draw the texture box
        IClickableMenu.drawTextureBox(
            b,
            Game1.menuTexture,
            new Rectangle(0, 256, 60, 60), // not sure what these numbers end up meaning, if anything
            (int)position.X,
            (int)position.Y,
            (int)dimensions.X,
            (int)dimensions.Y,
            Color.White
        );

        nextContentPosition = new Vector2(position.X + GutterLength, position.Y + GutterLength);
    }
    private void DrawTitleTextAndDividingLine(SpriteBatch b, Vector2 position, int totalWidth, out Vector2 nextContentPosition)
    {
        CenteredTextRow titleRow = new(TitleText, position, totalWidth);
        titleRow.Draw(b);

        var dividerPosition = new Vector2(position.X, position.Y + (int)Font.MeasureString(TitleText).Y + LineSpacing);

        Utility.drawLineWithScreenCoordinates(
            (int)dividerPosition.X, (int)dividerPosition.Y,
            (int)dividerPosition.X + totalWidth, (int)dividerPosition.Y,
            b,
            Game1.textColor,
            thickness: 1
        );

        nextContentPosition = new Vector2(dividerPosition.X, (int)dividerPosition.Y + 4 + LineSpacing);
    }

    private void DrawTodoItems(SpriteBatch b, Vector2 position, ICollection<ITodoItem> items, out Vector2 nextContentPosition)
    {
        Vector2 currentPosition = position;

        foreach (ITodoItem item in items.Take(visibleItemCount()))
        {
            TodoItemTextRow itemRow = new(item, currentPosition);
            itemRow.Draw(b);
            currentPosition.Y += (int)Font.MeasureString(item.Text()).Y + LineSpacing;
        }

        nextContentPosition = currentPosition;
    }

    private static void DrawOverflowIndicator(SpriteBatch b, Vector2 position, int numRemaining)
    {
        TextRow overflowIndicatorRow = new(I18n.Panel_OverflowIndicator(numRemaining), position);
        overflowIndicatorRow.Draw(b);
    }
}
