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
// UI RESPAWN
// =======================================================================================
public partial class UCE_UI_Bindpoint_RespawnDialogue : MonoBehaviour
{
    public GameObject panel;
    public Button RespawnToBindpointButton;

    // -----------------------------------------------------------------------------------
    // Update
    // -----------------------------------------------------------------------------------
    private void Update()
    {
        Player player = Player.localPlayer;
        if (!player) return;

        if (!panel.activeSelf) return;

        RespawnToBindpointButton.gameObject.SetActive(player.UCE_myBindpoint.Valid);
        RespawnToBindpointButton.onClick.SetListener(() => { player.Cmd_UCE_RespawnToBindpoint(); });
    }

    // -----------------------------------------------------------------------------------
}