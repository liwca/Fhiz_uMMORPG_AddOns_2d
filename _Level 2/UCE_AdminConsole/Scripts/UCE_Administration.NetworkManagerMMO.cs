// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using System.Collections.Generic;
using UnityEngine;

// =======================================================================================
// UCE ADMINISTRATION - NETWORK MANAGER MMO
// =======================================================================================
public partial class NetworkManagerMMO
{
    private List<Monster> cacheMonster = new List<Monster>();
    private List<Npc> cacheNpc = new List<Npc>();

    // -----------------------------------------------------------------------------------
    // OnServerCharacterCreate_UCE_Administration
    // -----------------------------------------------------------------------------------
    [DevExtMethods("OnServerCharacterCreate")]
    private void OnServerCharacterCreate_UCE_Administration(CharacterCreateMsg message, Player player)
    {
        if (Database.singleton.GetAccountCount() <= 1)
            Database.singleton.SetAdminAccount(player.account, 255);
    }

    // -----------------------------------------------------------------------------------
    // cachedMonsters
    // -----------------------------------------------------------------------------------
    public List<Monster> cachedMonsters()
    {
        if (cacheMonster.Count <= 0)
        {
            foreach (GameObject mon in spawnPrefabs)
            {
                if (mon.GetComponent<Monster>() != null)
                    cacheMonster.Add(mon.GetComponent<Monster>());
            }
        }

        return cacheMonster;
    }

    // -----------------------------------------------------------------------------------
    // cachedNpcs
    // -----------------------------------------------------------------------------------
    public List<Npc> cachedNpcs()
    {
        if (cacheNpc.Count <= 0)
        {
            foreach (GameObject npc in spawnPrefabs)
            {
                if (npc.GetComponent<Npc>() != null)
                    cacheNpc.Add(npc.GetComponent<Npc>());
            }
        }

        return cacheNpc;
    }
}

// =======================================================================================