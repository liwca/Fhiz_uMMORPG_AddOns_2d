// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;

// =======================================================================================
// EQUIPMENT ITEM TEMPLATE
// =======================================================================================
public partial class EquipmentItem
{
    [Header("-=-=- Equipment Set Bonus -=-=-")]
    [Tooltip("Put any EquipmentSet here to make this item part of a larger set.")]
    public UCE_EquipmentSetTemplate equipmentSet;

    [Header("-=-=- Individual Set Bonus -=-=-")]
    [Tooltip("All items must be equipped in order for this individual set bonus to be effective.")]
    public EquipmentItem[] setItems;

    [Tooltip("This is the individual set bonus granted.")]
    public UCE_StatModifier individualStatModifiers;

    // -----------------------------------------------------------------------------------
    // UCE_hasIndividualSetBonus
    // -----------------------------------------------------------------------------------
    public bool UCE_hasIndividualSetBonus
    {
        get
        {
            return individualStatModifiers != null && individualStatModifiers.hasModifier;
        }
    }
}

// =======================================================================================