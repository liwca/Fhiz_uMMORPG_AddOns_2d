// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;

// =======================================================================================
// EQUIPMENT SET TEMPLATE
// =======================================================================================
[CreateAssetMenu(fileName = "UCE EquipmentSet", menuName = "UCE Item/UCE EquipmentSet", order = 999)]
public class UCE_EquipmentSetTemplate : ScriptableObject
{
    [Header("-=-=- EquipmentSet -=-=-")]
    [Tooltip("A number of set items must be equipped for the partial bonus to be active. All set items must be equipped in order for the complete set bonus to be effective.")]
    public EquipmentItem[] setItems;

    [Header("-=-=- Partial Set Bonus -=-=-"), Tooltip("This number of set items must be equipped for the partial bonus to be active.")]
    [Range(0, 99)] public int partialSetItemsCount;

    public UCE_StatModifier partialStatModifiers;
    public UCE_StatModifier completeStatModifiers;

    // -----------------------------------------------------------------------------------
    // UCE_hasPartialSetBonus
    // -----------------------------------------------------------------------------------
    public bool UCE_hasPartialSetBonus
    {
        get
        {
            return partialStatModifiers.hasModifier;
        }
    }

    // -----------------------------------------------------------------------------------
    // UCE_hasCompleteSetBonus
    // -----------------------------------------------------------------------------------
    public bool UCE_hasCompleteSetBonus
    {
        get
        {
            return completeStatModifiers.hasModifier;
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================