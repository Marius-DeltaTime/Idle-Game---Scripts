using System.Collections.Generic;
using UnityEngine;

// Decrease cost of a random Craftable by a certain %.
public class rPassive6 : RarePassive
{
    private RarePassive _rarePassive;
    private CraftingType craftingTypeChosen;
    private float permanentAmount = 0.036f, prestigeAmount = 0.180f;

    private void Awake()
    {
        _rarePassive = GetComponent<RarePassive>();
        RarePassives.Add(Type, _rarePassive);
    }
    public void ChooseRandomCrafting()
    {
        List<CraftingType> craftingTypesInCurrentRun = new List<CraftingType>();

        foreach (var craft in Craftable.Craftables)
        {
            if (craft.Value.isUnlocked)
            {
                craftingTypesInCurrentRun.Add(craft.Key);
            }
        }
        if (craftingTypesInCurrentRun.Count >= Prestige.craftablesUnlockedInPreviousRun.Count)
        {
            _index = Random.Range(0, craftingTypesInCurrentRun.Count);
            craftingTypeChosen = craftingTypesInCurrentRun[_index];
        }
        else
        {
            _index = Random.Range(0, Prestige.craftablesUnlockedInPreviousRun.Count);
            craftingTypeChosen = Prestige.craftablesUnlockedInPreviousRun[_index];
        }
    }
    private void AddToPrestigeCache(float percentageAmount, CraftingType craftingType)
    {
        if (!PrestigeCache.prestigeBoxCraftableCostSubtraction.ContainsKey(craftingType))
        {
            PrestigeCache.prestigeBoxCraftableCostSubtraction.Add(craftingType, percentageAmount);
        }
        else
        {
            PrestigeCache.prestigeBoxCraftableCostSubtraction[craftingType] += percentageAmount;
        }
    }
    private void AddToPermanentCache(float percentageAmount, CraftingType craftingType)
    {
        if (!PermanentCache.permanentBoxCraftableCostSubtraction.ContainsKey(craftingType))
        {
            PermanentCache.permanentBoxCraftableCostSubtraction.Add(craftingType, percentageAmount);
        }
        else
        {
            PermanentCache.permanentBoxCraftableCostSubtraction[craftingType] += percentageAmount;
        }
    }
    private void ModifyStatDescription(float percentageAmount)
    {
        description = string.Format("Decrease cost of crafting '{0}' by {1}%", Craftable.Craftables[craftingTypeChosen].actualName, percentageAmount * 100);
    }
    public override void InitializePermanentStat()
    {
        ChooseRandomCrafting();
        ModifyStatDescription(permanentAmount);
        AddToPermanentCache(permanentAmount, craftingTypeChosen);
    }
    public override void InitializePrestigeStat()
    {
        ChooseRandomCrafting();
        ModifyStatDescription(prestigeAmount);
    }
    public override void InitializePrestigeButtonCrafting(CraftingType craftingType)
    {
        AddToPrestigeCache(prestigeAmount, craftingType);
    }
    public override CraftingType ReturnCraftingType()
    {
        return craftingTypeChosen;
    }
}
