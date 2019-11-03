// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using Mirror;
using UnityEngine;

// ===================================================================================
// LOOT RULES - MONSTER
// ===================================================================================
public partial class Monster
{
    [Header("[-=-=-=- Loot Rules (Victor can always loot) -=-=-=-]")]
    [Tooltip("After x seconds loot rules are set to 'LootEverybody'. Set to 0 to disable")]
    public float LiftRulesAfter;
    public bool LootVictorParty;
    public bool LootVictorGuild;
#if _iMMOPVP
    public bool LootVictorRealm;
#endif
    public bool LootEverybody;

    [SyncVar] protected bool lootRulesLifted;

    // -----------------------------------------------------------------------------------
    // Update_UCE_LootRules
    // -----------------------------------------------------------------------------------
    [ServerCallback]
    [DevExtMethods("Update")]
    public void Update_UCE_LootRules()
    {
        if (LiftRulesAfter > 0 && NetworkTime.time > (deathTimeEnd - deathTime) + LiftRulesAfter)
            lootRulesLifted = true;
        else
            lootRulesLifted = false;
    }

    // -----------------------------------------------------------------------------------
    // UCE_ValidateTaggedLooting
    // -----------------------------------------------------------------------------------
    public bool UCE_ValidateTaggedLooting(Player player)
    {
        if (LootEverybody ||
        lastAggressor == null ||
        lastAggressor == player ||
        (LiftRulesAfter > 0 && lootRulesLifted)
        ) return true;
        if (LootVictorParty && UCE_ValidateTaggedLootingParty(player)) return true;
        if (LootVictorGuild && UCE_ValidateTaggedLootingGuild(player)) return true;
#if _iMMOPVP
        if (LootVictorRealm && UCE_ValidateTaggedLootingRealm(player)) return true;
#endif
        return false;
    }

    // -----------------------------------------------------------------------------------
}