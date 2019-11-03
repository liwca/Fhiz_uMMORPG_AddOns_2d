// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;

// =======================================================================================
// PLAYER
// =======================================================================================
public partial class Monster
{
    [Header("-=-=-=- UCE FACTION -=-=-=-")]
    public UCE_Tmpl_Faction myFaction;

    // -----------------------------------------------------------------------------------
    // UCE_checkFactionThreshold
    // -----------------------------------------------------------------------------------
    public bool UCE_checkFactionThreshold(Entity entity)
    {
#if _iMMOPVP
        if (
            entity is Player &&
            myFaction != null &&
            ((Player)entity).UCE_GetFactionRating(myFaction) <= myFaction.aggressiveThreshold)
        {
            return false;
        }
#endif
        return true;
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================