using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Researchable
{
    private Researchable _researchable;

    // Fire should unlock Cooking, Smelting and FireHardenedWeapons
    // Fire Hardened Weapons should only be unlocked if weapons and fire has been researched.
    // Unlock after having 100 knowledge.
    void Awake()
    {
        _researchable = GetComponent<Researchable>();
        Researchables.Add(Type, _researchable);

        SetInitialValues();
    }
    void Start()
    {
        SetDescriptionText("Unlock new technologies");
    }
}
