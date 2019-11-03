// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using System;
using UnityEngine;

#if _iMMOHARVESTING

public enum UCE_HarvestingResult { None, Failure, Success, CriticalSuccess }

// ---------------------------------------------------------------------------------------
// UCE_HarvestingTool
// ---------------------------------------------------------------------------------------
[Serializable]
public partial class UCE_HarvestingTool
{
    [Header("-=-=- Harvesting Tool -=-=-")]
    [Tooltip("[Required] Required item for the harvesting process and modifiers below")]
    public ScriptableItem requiredItem;

    [Tooltip("[Optional] Does this item have to be equipped to be effective (any slot)?")]
    public bool equippedItem;

    [Tooltip("[Optional] Is this item destroyed during the harvesting process (0 disable, 0.5=50%, 1=100%)?")]
    [Range(0, 1)] public float toolDestroyChance;

    [Tooltip("[Optional] Modify basic success chance when this tool is present?")]
    [Range(0, 1)] public float modifyProbability;

    [Tooltip("[Optional] Modify critical success chance when this tool is present?")]
    [Range(0, 1)] public float modifyCriticalProbability;

    [Tooltip("[Optional] Modify profession experience gained when this tool is present?")]
    public int modifyExperienceMin;
    public int modifyExperienceMax;

    [Tooltip("[Optional] +/- Modify the harvesting process duration (in Seconds)")]
    public float modifyDuration;
}

#endif

// =======================================================================================