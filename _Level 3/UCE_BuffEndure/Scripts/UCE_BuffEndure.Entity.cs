// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using System.Linq;
using UnityEngine;

// =======================================================================================
// ENTITY
// =======================================================================================
public partial class Entity
{
    public int health
    {
        get { return Mathf.Min(_health, healthMax); } // min in case hp>hpmax after buff ends etc.
        set
        {
#if _iMMOBUFFENDURE
            if (buffs.Any(x => x.endure))
                _health = Mathf.Clamp(value, 1, healthMax);
            else
#endif
                _health = Mathf.Clamp(value, 0, healthMax);
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================