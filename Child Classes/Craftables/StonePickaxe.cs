using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePickaxe : Craftable
{
    private Craftable _craftable;

    void Awake()
    {
        _craftable = GetComponent<Craftable>();
        Craftables.Add(Type, _craftable);
        SetInitialValues();
    }

    void Start()
    {
        // Maybe enables the mining of ore.
        // Can start digging bronze ore at the dig site?
        // So should either enable dig site to mine some ores, 
        // Or unlock a new building responsible with mining ores.
        // Lets go with unlocking a new building for now.
        SetDescriptionText("");
    }
}
