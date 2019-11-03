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
public partial class Entity
{
    [Header("-=-=-=- UCE FACTIONS -=-=-=-")]
    public UCE_FactionModifier[] factionModifiers;

    // -----------------------------------------------------------------------------------
    // OnDeath_UCE_Factions
    // -----------------------------------------------------------------------------------
    [DevExtMethods("OnDeath")]
    private void OnDeath_UCE_Factions()
    {
        if (lastAggressor != null && lastAggressor is Player)
        {
            Player player = (Player)lastAggressor;

            foreach (UCE_FactionModifier factionModifier in factionModifiers)
            {
                if (factionModifier.faction != null && factionModifier.amount != 0)
                {
                    player.UCE_AddFactionRating(factionModifier.faction, factionModifier.amount);
                }
            }
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================