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
// UCE UI NPC PLAYER WAREHOUSE DIALOGUE
// =======================================================================================
public class UCE_UI_NpcPlayerWarehouseDialogue : MonoBehaviour
{
    public GameObject npcDialoguePanel;
    public Button playerWarehouseButton;
    public UCE_UI_PlayerWarehouse playerWarehousePanel;
    public GameObject inventoryPanel;

    // -----------------------------------------------------------------------------------
    // Update
    // -----------------------------------------------------------------------------------
    private void Update()
    {
        Player player = Player.localPlayer;
        if (!player) return;

        if (player.target != null && player.target is Npc &&
            Utils.ClosestDistance(player.collider, player.target.collider) <= player.interactionRange)
        {
            Npc npc = (Npc)player.target;

            playerWarehouseButton.gameObject.SetActive(npc.offersPlayerWarehouse);

            playerWarehouseButton.onClick.SetListener(() =>
            {
                npcDialoguePanel.SetActive(false);
                inventoryPanel.SetActive(true);
                playerWarehousePanel.Show();
            });
        }
        else
        {
            playerWarehousePanel.Hide();
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================