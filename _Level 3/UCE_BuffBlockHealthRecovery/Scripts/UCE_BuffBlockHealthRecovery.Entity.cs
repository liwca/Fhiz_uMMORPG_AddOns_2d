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
    public bool _healthRecovery = true; // can be disabled in combat etc.

    // -----------------------------------------------------------------------------------
    // healthRecovery
    // -----------------------------------------------------------------------------------
    public virtual bool healthRecovery
    {
        get
        {
#if _iMMOBUFFBLOCKHEALTHRECOVERY
            return healthRecoveryRate < 0 || (_healthRecovery && !buffs.Any(x => x.blockHealthRecovery));
#else
			return _healthRecovery;
#endif
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================