using StardewValley;
using StardewValley.Locations;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;

namespace AutomaticTodoList;

internal static class GameExtensions
{
    public static int GetNumberOfReadyMachinesExcludingBuildings(this GameLocation location)
    {
        int num = 0;
        foreach (StardewValley.Object obj in location.objects.Values)
        {
            if (obj.IsConsideredReadyMachineForComputer())
            {
                if (obj is ItemPedestal itemPedestal)
                {
                    // skip if this "machine" is a pedestal
                    continue;
                }

                num++;
            }
        }

        return num;
    }

    public static int GetTotalCropsReadyForHarvestExcludingForagables(this GameLocation location)
    {
        int num = 0;
        foreach (TerrainFeature value in location.terrainFeatures.Values)
        {
            if (
                value is HoeDirt hoeDirt && hoeDirt.readyForHarvest() && // existing checks
                hoeDirt.crop is not null && !hoeDirt.crop.forageCrop.Value // added to exclude foragables
            )
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

    public static bool IsInPassiveFestivalLocation(this Character character, string festivalID)
    {
        return festivalID switch
        {
            "NightMarket" => character.currentLocation is BeachNightMarket,
            "DesertFestival" => character.currentLocation is DesertFestival,
            "TroutDerby" => character.currentLocation is Forest,
            "SquidFest" => character.currentLocation is Beach,
            _ => false
        };
    }
}
