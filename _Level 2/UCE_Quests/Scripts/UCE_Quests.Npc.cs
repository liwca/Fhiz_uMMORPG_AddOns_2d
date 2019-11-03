// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// =======================================================================================
// NPC
// =======================================================================================
public partial class Npc
{
    [Header("[-=-=-=- UCE Quests -=-=-=-]")]
    public UCE_ScriptableQuest[] UCE_quests;

    // =============================== CORE SCRIPT REWRITES ==============================

    // -----------------------------------------------------------------------------------
    // UpdateOverlays
    // -----------------------------------------------------------------------------------
    protected override void UpdateOverlays()
    {
        base.UpdateOverlays();
    }

    // -----------------------------------------------------------------------------------
    // UpdateClient_UCE_quests
    // -----------------------------------------------------------------------------------
    [Client]
    [DevExtMethods("UpdateClient")]
    protected void UpdateClient_UCE_quests()
    {
        if (questOverlay != null)
        {
            Player player = Player.localPlayer;

            if (player != null)
            {
                if (UCE_quests.Any(q => player.UCE_CanCompleteQuest(q.name)))
                    questOverlay.text = "!";
                else if (UCE_quests.Any(player.UCE_CanAcceptQuest))
                    questOverlay.text = "?";
                else
                    questOverlay.text = "";
            }
        }
    }

    // -----------------------------------------------------------------------------------
    // UCE_questsVisibleFor
    // -----------------------------------------------------------------------------------
    public List<UCE_ScriptableQuest> UCE_QuestsVisibleFor(Player player)
    {
        return UCE_quests.Where(q =>
                                        player.UCE_CanAcceptQuest(q) ||
                                        player.UCE_CanCompleteQuest(q.name) ||
                                        player.UCE_HasActiveQuest(q.name)
                                ).ToList();
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================