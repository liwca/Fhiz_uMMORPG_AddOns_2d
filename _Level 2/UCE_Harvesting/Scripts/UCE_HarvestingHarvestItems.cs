// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;

// =======================================================================================
// HARVEST ITEMS
// =======================================================================================
[System.Serializable]
public class UCE_HarvestingHarvestItems
{
    public ScriptableItem template;
    [Range(0, 1)] public float probability;
    [Range(1, 999)] public int minAmount = 1;
    [Range(1, 999)] public int maxAmount = 1;
}

// =======================================================================================