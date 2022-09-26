using System.Collections.Generic;
using UnityEngine;

//ePassive3: Start next/ each run with a amount number of a random Building.
public class lPassive3 : LegendaryPassive
{
    private LegendaryPassive _legendaryPassive;
    private BuildingType buildingTypeChosen;
    private uint permanentAmount = 7, prestigeAmount = 35;

    private void Awake()
    {
        _legendaryPassive = GetComponent<LegendaryPassive>();
        LegendaryPassives.Add(Type, _legendaryPassive);
    }
    private void ChooseRandomBuilding()
    {
        List<BuildingType> buildingTypesInCurrentRun = new List<BuildingType>();

        foreach (var building in Building.Buildings)
        {
            if (building.Value.isUnlocked)
            {
                buildingTypesInCurrentRun.Add(building.Key);
            }
        }
        if (buildingTypesInCurrentRun.Count >= Prestige.buildingsUnlockedInPreviousRun.Count)
        {
            _index = Random.Range(0, buildingTypesInCurrentRun.Count);
            buildingTypeChosen = buildingTypesInCurrentRun[_index];
        }
        else
        {
            _index = Random.Range(0, Prestige.buildingsUnlockedInPreviousRun.Count);
            buildingTypeChosen = Prestige.buildingsUnlockedInPreviousRun[_index];
        }
    }
    private void AddToPrestigeCache(uint selfCountIncreaseAmount)
    {
        if (!PrestigeCache.prestigeBoxBuildingCountAddition.ContainsKey(buildingTypeChosen))
        {
            PrestigeCache.prestigeBoxBuildingCountAddition.Add(buildingTypeChosen, selfCountIncreaseAmount);
        }
        else
        {
            PrestigeCache.prestigeBoxBuildingCountAddition[buildingTypeChosen] += selfCountIncreaseAmount;
        }
    }
    private void AddToPermanentCache(uint selfCountIncreaseAmount)
    {
        if (!PermanentCache.permanentBoxBuildingCountAddition.ContainsKey(buildingTypeChosen))
        {
            PermanentCache.permanentBoxBuildingCountAddition.Add(buildingTypeChosen, selfCountIncreaseAmount);
        }
        else
        {
            PermanentCache.permanentBoxBuildingCountAddition[buildingTypeChosen] += selfCountIncreaseAmount;
        }
    }
    private void ModifyStatDescription(uint selfCountIncreaseAmount)
    {
        if (selfCountIncreaseAmount > 1)
        {
            description = string.Format("Start with {0} additional {1}'s", selfCountIncreaseAmount, Building.Buildings[buildingTypeChosen].actualName);
        }
        else
        {
            description = string.Format("Start with an additional '{1}'", selfCountIncreaseAmount, Building.Buildings[buildingTypeChosen].actualName);
        }
    }
    public override void InitializePermanentStat()
    {
        ChooseRandomBuilding();
        ModifyStatDescription(permanentAmount);
        AddToPermanentCache(permanentAmount);
    }
    public override void InitializePrestigeStat()
    {
        ChooseRandomBuilding();
        ModifyStatDescription(prestigeAmount);
    }
    public override void InitializePrestigeButtonBuilding(BuildingType buildingType)
    {
        AddToPrestigeCache(prestigeAmount);
    }
    public override BuildingType ReturnBuildingType()
    {
        return buildingTypeChosen;
    }
}
