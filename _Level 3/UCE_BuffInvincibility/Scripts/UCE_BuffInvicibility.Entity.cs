// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using System.Linq;

// =======================================================================================
// ENTITY
// =======================================================================================
public partial class Entity
{
    public bool _invincible = false;

    // -----------------------------------------------------------------------------------
    // invincible
    // -----------------------------------------------------------------------------------
    public virtual bool invincible
    {
        get
        {
#if _iMMOBUFFINVINCIBILITY
            return _invincible || buffs.Any(x => x.invincibility);
#else
			return _invincible;
#endif
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================