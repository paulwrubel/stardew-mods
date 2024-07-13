
using StardewValley;
using StardewValley.Locations;
using StardewValley.TerrainFeatures;

namespace AutomaticTodoList;

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
}
