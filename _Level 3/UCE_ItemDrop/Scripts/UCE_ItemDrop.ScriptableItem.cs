// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;

// ===================================================================================
// ITEM DROP - SCRIPTABLE ITEM
// ===================================================================================
public partial class ScriptableItem
{
    [Header("[-=-=- UCE ITEM DROP -=-=-]")]
    [Range(0, 1)]
    [Tooltip("Required] Players only: Chance to drop the item from inventory to the ground when dead.")]
    public float dropChance = 0f;

    [Tooltip("[Required] Drop prefab that is used to represent the item, requires a ItemDrop component on it.")]
    public UCE_ItemDrop dropPrefab;

    // -----------------------------------------------------------------------------------
}