// =======================================================================================
// Created and maintained by iMMO
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// * Instructions.......................: https://indie-mmo.net/knowledge-base/
// =======================================================================================
using Mirror;
using UnityEngine;

// GUILD WAREHOUSE - NPC

public partial class Npc
{
    [Header("[-=-=-=- UCE GUILD WAREHOUSE -=-=-=-]")]
    public bool offersGuildWarehouse = false;

    public bool accessViceOrMasterOnly;
    [HideInInspector] public SyncListString guildWarehouseBusy = new SyncListString();

    // -----------------------------------------------------------------------------------
    // checkWarehouseAccess
    // -----------------------------------------------------------------------------------
    public bool checkWarehouseAccess(Player player)
    {
        if (!offersGuildWarehouse || !player.InGuild()) return false;
        if (!accessViceOrMasterOnly) return true;

        return player.guild.CanNotify(player.name);
    }

    // -----------------------------------------------------------------------------------
}