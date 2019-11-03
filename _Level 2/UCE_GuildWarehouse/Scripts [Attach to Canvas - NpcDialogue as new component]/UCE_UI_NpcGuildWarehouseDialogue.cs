// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;
using UnityEngine.UI;

// =======================================================================================
// UCE UI NPC GUILD WAREHOUSE DIALOGUE
// =======================================================================================
public class UCE_UI_NpcGuildWarehouseDialogue : MonoBehaviour
{
    public GameObject npcDialoguePanel;
    public Button guildWarehouseButton;
    public UCE_UI_GuildWarehouse guildWarehousePanel;
    public GameObject inventoryPanel;

    private bool init = false;

    // -----------------------------------------------------------------------------------
    // Update
    // -----------------------------------------------------------------------------------
    private void Update()
    {
        Player player = Player.localPlayer;
        if (!player) return;

        if (player.target != null &&
            player.target is Npc &&
            Utils.ClosestDistance(player.collider, player.target.collider) <= player.interactionRange)
        {
            Init();

            Npc npc = (Npc)player.target;

            guildWarehouseButton.gameObject.SetActive(npc.checkWarehouseAccess(player));
            guildWarehouseButton.interactable = player.InGuild() && !npc.guildWarehouseBusy.Contains(player.guild.name);

            if (guildWarehouseButton.interactable)
            {
                guildWarehouseButton.onClick.SetListener(() =>
                {
                    player.Cmd_UCE_BusyGuildWarehouse(player.target.gameObject, player.guild.name);
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    guildWarehousePanel.Show();
                    inventoryPanel.SetActive(true);
                });
            }
        }
        else
        {
            Reset();
            guildWarehousePanel.Hide();
        }
    }

    // -----------------------------------------------------------------------------------
    // Init
    // -----------------------------------------------------------------------------------
    private void Init()
    {
        if (init) return;

        Player player = Player.localPlayer;
        if (!player) return;

        init = true;

        Npc npc = (Npc)player.target;

        player.Cmd_UCE_AccessGuildWarehouse(player.target.gameObject);
    }

    // -----------------------------------------------------------------------------------
    // Reset
    // -----------------------------------------------------------------------------------
    private void Reset()
    {
        if (!init) return;

        init = false;
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================