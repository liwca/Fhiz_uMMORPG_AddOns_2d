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
// UCE UI GUILD WAREHOUSE UPGRADE PANEL
// =======================================================================================
public partial class UCE_UI_GuildWarehouseUpgradePanel : MonoBehaviour
{
    public GameObject panel;
    public Button acceptButton;
    public Button declineButton;

    // -----------------------------------------------------------------------------------
    // Update
    // -----------------------------------------------------------------------------------
    private void Update()
    {
        Player player = Player.localPlayer;
        if (!player) return;

        // use collider point(s) to also work with big entities
        if (panel.activeSelf &&
            player.target != null && player.target is Npc &&
            Utils.ClosestDistance(player.collider, player.target.collider) <= player.interactionRange)
        {
            acceptButton.interactable = player.UCE_CanUpgradeGuildWarehouse();

            acceptButton.onClick.SetListener(() =>
            {
                player.Cmd_UCE_UpgradeGuildWarehouse();
                panel.SetActive(false);
            });

            declineButton.onClick.SetListener(() =>
            {
                panel.SetActive(false);
            });
        }
        else panel.SetActive(false);
    }

    // -----------------------------------------------------------------------------------
    // Show
    // -----------------------------------------------------------------------------------
    public void Show()
    {
        Player player = Player.localPlayer;
        if (!player) return;

        if (!panel.activeSelf)
        {
            panel.SetActive(true);
        }
    }

    // -----------------------------------------------------------------------------------
    // Show
    // -----------------------------------------------------------------------------------
    public void Hide()
    {
        Player player = Player.localPlayer;
        if (!player) return;

        if (panel.activeSelf)
        {
            panel.SetActive(false);
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================