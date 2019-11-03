// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using System;
using System.Linq;
using UnityEngine;

// =======================================================================================
// ENTITY
// =======================================================================================
public partial class Entity
{
    // -----------------------------------------------------------------------------------
    // gold
    // -----------------------------------------------------------------------------------
    public long gold
    {
        get { return _gold; }
        set
        {
#if _iMMOBUFFGOLD
            float fGoldFactor = buffs.Sum(x => x.boostGold);
            if (fGoldFactor != 0 && value != _gold)
            {
                long diff = Math.Max(value, _gold) - Math.Min(value, _gold);
                diff = _gold + (long)Mathf.Round(diff * fGoldFactor);
                _gold = Math.Max(diff, 0);
            }
            else
            {
                _gold = Math.Max(value, 0);
            }
#else
			_gold = Math.Max(value, 0);
#endif
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================