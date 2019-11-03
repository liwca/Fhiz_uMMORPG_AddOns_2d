using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// =======================================================================================
// HONOR CURRENCY - TEMPLATE
// =======================================================================================
[CreateAssetMenu(fileName = "UCE HonorCurrency", menuName = "UCE Templates/New UCE HonorCurrency", order = 999)]
public class UCE_Tmpl_HonorCurrency : ScriptableObject
{
    public Sprite image;

    [Tooltip("[Optional] This currency is only dropped if all criteria are met")]
    public UCE_Requirements dropRequirements;

    [Tooltip("[Optional] Currency amount is awarded per level of the target")]
    public bool perLevel;

    [Tooltip("[Optional] Will share a fraction of this currency with online party members")]
    public bool shareWithParty;

    [Tooltip("[Optional] Will share a fraction of this currency with online guild members")]
    public bool shareWithGuild;
#if _iMMOPVP

    [Tooltip("[Optional] Will share a fraction of this currency with online realm members")]
    public bool FromHostileRealmsOnly;
    public bool shareWithRealm;
#endif

    // -----------------------------------------------------------------------------------
    // Caching
    // -----------------------------------------------------------------------------------
    private static Dictionary<int, UCE_Tmpl_HonorCurrency> cache;

    public static Dictionary<int, UCE_Tmpl_HonorCurrency> dict
    {
        get
        {
            return cache ?? (cache = Resources.LoadAll<UCE_Tmpl_HonorCurrency>("").ToDictionary(
                item => item.name.GetStableHashCode(), item => item)
            );
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================