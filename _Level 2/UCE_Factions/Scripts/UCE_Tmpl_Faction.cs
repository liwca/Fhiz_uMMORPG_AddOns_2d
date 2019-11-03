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
// PRESTIGE CLASS - TEMPLATE
// =======================================================================================
[CreateAssetMenu(fileName = "UCE_Tmpl_Faction", menuName = "UCE Templates/New UCE Faction", order = 998)]
public class UCE_Tmpl_Faction : ScriptableObject
{
    [Header("-=-=-=- UCE FACTION -=-=-=-")]
    public Sprite image;
    [TextArea(1, 10)] public string description;

    public UCE_FactionRank[] ranks;
#if _iMMOPVP

    [Tooltip("Monsters set to 'aggressive' will only attack a player when their faction ranking falls below this threshold.")]
    public float aggressiveThreshold;
#endif

    // -----------------------------------------------------------------------------------
    // getRank
    // -----------------------------------------------------------------------------------
    public string getRank(int rating)
    {
        foreach (UCE_FactionRank rank in ranks)
            if (rank.min <= rating && rank.max >= rating) return rank.name;

        return "???";
    }

    // -----------------------------------------------------------------------------------
    // getRank
    // -----------------------------------------------------------------------------------
    public string getRank(int min, int max)
    {
        foreach (UCE_FactionRank rank in ranks)
            if (min >= rank.min && max <= rank.max) return rank.name;

        return "???";
    }

    // -----------------------------------------------------------------------------------
    // checkAggressive
    // -----------------------------------------------------------------------------------
    public bool checkAggressive(int rating)
    {
#if _iMMOPVP
        return (rating <= aggressiveThreshold);
#else
		return false;
#endif
    }

    // -----------------------------------------------------------------------------------
    // Caching
    // -----------------------------------------------------------------------------------
    private static Dictionary<int, UCE_Tmpl_Faction> cache;

    public static Dictionary<int, UCE_Tmpl_Faction> dict
    {
        get
        {
            // load if not loaded yet
            return cache ?? (cache = Resources.LoadAll<UCE_Tmpl_Faction>("").ToDictionary(
                item => item.name.GetStableHashCode(), item => item)
            );
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================