// =======================================================================================
// Created and maintained by iMMO
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/iMMOban
// =======================================================================================

using UnityEngine;

// =======================================================================================
// UCE STAT MODIFIER
// =======================================================================================
[System.Serializable]
public class UCE_StatModifier
{
#if _iMMOATTRIBUTES

    [Header("[-=-=- UCE ATTRIBUTE MODIFIERS -=-=-]")]
    public UCE_AttributeModifier[] UCE_AttributeModifiers = { };
#endif
#if _iMMOELEMENTS

    [Header("[-=-=- UCE ELEMENTAL RESISTANCES -=-=-]")]
    public UCE_ElementModifier[] elementalResistances;
#endif

    [Header("[-=-=- UCE MAIN STAT MODIFIERS -=-=-]")]
    public int healthBonus;
    public int manaBonus;
    public int damageBonus;
    public int defenseBonus;

    [Header("[-=-=- UCE SECONDARY STAT MODIFIERS -=-=-]")]
    public float blockChanceBonus;
    public float criticalChanceBonus;
#if _iMMOATTRIBUTES
    public float bonusBlockFactor;
    public float bonusCriticalFactor;
    public float bonusDrainHealthFactor;
    public float bonusDrainManaFactor;
    public float bonusReflectDamageFactor;
    public float bonusDefenseBreakFactor;
    public float bonusBlockBreakFactor;
    public float bonusCriticalEvasion;
    public float bonusAccuracy;
    public float bonusResistance;
    public float bonusAbsorbHealthFactor;
    public float bonusAbsorbManaFactor;
#endif

    // -----------------------------------------------------------------------------------
    // hasModifier
    // -----------------------------------------------------------------------------------
    public bool hasModifier
    {
        get
        {
            return
                    (
#if _iMMOATTRIBUTES
                    UCE_AttributeModifiers.Length > 0 ||
#endif
#if _iMMOELEMENTS
                    elementalResistances.Length > 0 ||
#endif
#if _iMMOATTRIBUTES
                    bonusBlockFactor != 0 ||
                    bonusCriticalFactor != 0 ||
                    bonusDrainHealthFactor != 0 ||
                    bonusDrainManaFactor != 0 ||
                    bonusReflectDamageFactor != 0 ||
                    bonusDefenseBreakFactor != 0 ||
                    bonusBlockBreakFactor != 0 ||
                    bonusCriticalEvasion != 0 ||
                    bonusAccuracy != 0 ||
                    bonusResistance != 0 ||
                    bonusAbsorbHealthFactor != 0 ||
                    bonusAbsorbManaFactor != 0 ||
#endif
                    healthBonus != 0 ||
                    manaBonus != 0 ||
                    damageBonus != 0 ||
                    defenseBonus != 0 ||
                    blockChanceBonus != 0 ||
                    criticalChanceBonus != 0
                    );
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================