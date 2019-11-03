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

#if _iMMOCRAFTING

// =======================================================================================
// CRAFTING PROFESSION TEMPLATE
// =======================================================================================
[CreateAssetMenu(fileName = "New UCE Crafting Profession", menuName = "UCE Templates/New UCE Crafting Profession", order = 999)]
public class UCE_CraftingProfessionTemplate : ScriptableObject
{
    [Header("-=-=-=- UCE Crafting Profession -=-=-=-")]
    public int[] levels;
    public string[] categories;
    public Sprite image;
    public string playerAnimation;

    [Tooltip("[Optional] Sound effect that is played, when the player starts crafting.")]
    public AudioClip startPlayerSound;

    [Tooltip("[Optional] Sound effect that is played, when the player finishes crafting.")]
    public AudioClip stopPlayerSound;
    [TextArea(1, 30)] public string toolTip;

    // -----------------------------------------------------------------------------------
    // Caching
    // -----------------------------------------------------------------------------------
    private static Dictionary<int, UCE_CraftingProfessionTemplate> cache;

    public static Dictionary<int, UCE_CraftingProfessionTemplate> dict
    {
        get
        {
            return cache ?? (cache = Resources.LoadAll<UCE_CraftingProfessionTemplate>("").ToDictionary(
                x => x.name.GetStableHashCode(), x => x)
            );
        }
    }

    // -----------------------------------------------------------------------------------
}

#endif

// =======================================================================================