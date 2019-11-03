// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// =======================================================================================
// UCE ATTRIBUTE
// =======================================================================================
[CreateAssetMenu(fileName = "New UCE WorldEvent", menuName = "UCE Templates/New UCE WorldEvent", order = 999)]
public partial class UCE_WorldEventTemplate : ScriptableObject
{
    [Header("[EVENT THRESHOLDS (checked top to bottom)]")]
    public UCE_WorldEventData[] thresholdData;

    // -----------------------------------------------------------------------------------
    // Caching
    // -----------------------------------------------------------------------------------
    private static Dictionary<int, UCE_WorldEventTemplate> cache;

    public static Dictionary<int, UCE_WorldEventTemplate> dict
    {
        get
        {
            return cache ?? (cache = Resources.LoadAll<UCE_WorldEventTemplate>("").ToDictionary(
                x => x.name.GetStableHashCode(), x => x)
            );
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================