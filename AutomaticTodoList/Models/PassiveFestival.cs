using StardewValley;
using StardewValley.GameData;
using StardewValley.TokenizableStrings;

namespace AutomaticTodoList;

internal class PassiveFestival(string id, PassiveFestivalData data)
{
    public string ID { get; } = id;

    public PassiveFestivalData Data { get; } = data;

    public string GetI18nDisplayName()
    {
        int day = Game1.dayOfMonth - Data.StartDay + 1;

        if (this.Data.DisplayName != "")
        {
            string parsedI18nName = TokenParser.ParseText(Data.DisplayName);

            // this is probably the Night Market or the Desert Festival, so use the article text
            //
            // if this is custom content, we just have to cross our fingers that this is correct
            return I18n.Items_PassiveFestival_TextWithArticle(parsedI18nName, day);
        }

        return this.ID switch
        {
            "TroutDerby" => I18n.Items_PassiveFestival_TextWithArticle(
                Game1.content.LoadString("Strings\\1_6_Strings:TroutDerby"),
                day
            ),
            "SquidFest" => I18n.Items_PassiveFestival_TextWithoutArticle(
                Game1.content.LoadString("Strings\\1_6_Strings:SquidFest"),
                day
            ),

            // this triggers only for custom content without a DisplayName, so there's nothing we can do
            _ => I18n.Items_PassiveFestival_TextWithArticle(this.ID, day)
        };
    }

    public override bool Equals(object? obj)
    {
        return obj is PassiveFestival otherFestival && this.ID == otherFestival.ID;
    }

    public override int GetHashCode()
    {
        return (this.GetType(), this.ID).GetHashCode();
    }
}