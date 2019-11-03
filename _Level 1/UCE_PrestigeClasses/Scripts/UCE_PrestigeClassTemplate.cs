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
// PRESTIGE CLASS TEMPLATE
// =======================================================================================
[CreateAssetMenu(fileName = "New UCE Prestige Class", menuName = "UCE Templates/New UCE Prestige Class", order = 999)]
public class UCE_PrestigeClassTemplate : ScriptableObject
{
    // -----------------------------------------------------------------------------------
    // Cache
    // -----------------------------------------------------------------------------------
    private static Dictionary<int, UCE_PrestigeClassTemplate> cache;

    public static Dictionary<int, UCE_PrestigeClassTemplate> dict
    {
        get
        {
            // not loaded yet?
            if (cache == null)
            {
                // get all ScriptableItems in resources
                UCE_PrestigeClassTemplate[] items = Resources.LoadAll<UCE_PrestigeClassTemplate>("");

                // check for duplicates, then add to cache
                List<string> duplicates = items.ToList().FindDuplicates(item => item.name);
                if (duplicates.Count == 0)
                {
                    cache = items.ToDictionary(item => item.name.GetStableHashCode(), item => item);
                }
            }
            return cache;
        }
    }
}

// =======================================================================================