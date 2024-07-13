using StardewValley;
using StardewValley.Locations;
using StardewValley.TerrainFeatures;

namespace AutomaticTodoList
{

    public enum TaskPriority
    {
        // high
        Birthday,
        ToolPickup,
        ReadyMachines,
        HarvestableCrops,
        WaterableCrops,
        TravelingMerchant,
        SpecialOrders,
        BulletinBoard,
        Test,
        Default,
        // low
    }

    public enum Frequency
    {
        OnceADay,
        EveryTimeChange,
        EverySecond,
        EveryTick,
    }

    public enum SpecialOrderType
    {
        Standard,
        Qi,
    }

    internal static class GameHelper
    {
        public static IEnumerator<GameLocation> LocationsEnumerator()
        {
            // copy the list in case the reference changes between enumerator steps
            var locations = new List<GameLocation>(Game1.locations);

            foreach (GameLocation location in locations)
            {
                yield return location;
            }
        }
    }

    internal static class GameExtensions
    {

        public static int GetNumberOfReadyMachinesExcludingBuildings(this GameLocation location)
        {
            int num = 0;
            foreach (StardewValley.Object value in location.objects.Values)
            {
                if (value.IsConsideredReadyMachineForComputer())
                {
                    num++;
                }
            }

            return num;
        }

        public static int GetTotalUnwateredCropsExcludingGinger(this GameLocation location)
        {
            if (location.IsGingerIslandLocation())
            {
                int num = 0;
                foreach (TerrainFeature feature in location.terrainFeatures.Values)
                {
                    if (feature is HoeDirt hoeDirt &&
                        hoeDirt.crop is not null &&
                        hoeDirt.needsWatering() &&
                        !hoeDirt.isWatered() &&
                        !hoeDirt.crop.IsGinger()
                    )
                    {
                        num++;
                    }
                }

                return num;
            }
            else
            {
                return location.getTotalUnwateredCrops();
            }
        }

        public static bool IsGingerIslandLocation(this GameLocation location)
        {
            return location is IslandLocation;
        }

        public static bool IsGinger(this Crop crop)
        {
            return crop is not null && crop.forageCrop.Value && crop.whichForageCrop.Value == "2";
        }

        public static string ToStardewSpecialOrderTypeString(this SpecialOrderType type)
        {
            return type switch
            {
                SpecialOrderType.Standard => "",
                SpecialOrderType.Qi => "Qi",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}