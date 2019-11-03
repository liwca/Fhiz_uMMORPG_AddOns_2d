// =======================================================================================
// Created and maintained by iMMO
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;

// =======================================================================================
// BONUS SKILL
// =======================================================================================
public abstract partial class BonusSkill
{
    [Header("[-=-=- UCE BUFF -=-=-]")]
    [Tooltip("Buff cannot be removed via debuffing, it must time-out by itself instead.")]
    public bool cannotRemove;

    [Tooltip("Blocks negative status effects being applied, while buff is active")]
    public bool blockNerfs;

    [Tooltip("Blocks positive status effects being applied, while buff is active")]
    public bool blockBuffs;

#if _iMMOBUFFBLOCKHEALTHRECOVERY

    [Tooltip("Blocks health recovery (and only recovery), while buff is active")]
    public bool blockHealthRecovery;
#endif
#if _iMMOBUFFBLOCKMANARECOVERY

    [Tooltip("Blocks mana recovery (and only recovery), while buff is active")]
    public bool blockManaRecovery;
#endif
#if _iMMOBUFFENDURE

    [Tooltip("Prevents losing the final Healthpoint, while buff is active (= cannot die)")]
    public bool endure;
#endif
#if _iMMOBUFFEXPERIENCE

    [Tooltip("Increases the amount of experience gained by this factor, while buff is active (0.5=50%, 1.5=150% etc.)")]
    public float boostExperience;
#endif
#if _iMMOBUFFGOLD

    [Tooltip("Increases the amount of gold gained by this factor, while buff is active (0.5=50%, 1.5=150% etc.)")]
    public float boostGold;
#endif
#if _iMMOBUFFINVINCIBILITY

    [Tooltip("Completely invulnerable while buff is active.")]
    public bool invincibility;
#endif
}

// =======================================================================================