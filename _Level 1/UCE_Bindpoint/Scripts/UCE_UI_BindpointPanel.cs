// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// =======================================================================================
// UCE_UI_BindpointPanel
// =======================================================================================
public partial class UCE_UI_BindpointPanel : MonoBehaviour
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
        if (player.target != null && player.target is Npc &&
            Utils.ClosestDistance(player.collider, player.target.collider) <= player.interactionRange)
        {
            acceptButton.onClick.SetListener(() =>
            {
                player.UCE_myBindpoint.position = new Vector3(((Npc)player.target).bindpoint.position.x, ((Npc)player.target).bindpoint.position.y, ((Npc)player.target).bindpoint.position.z);
                player.UCE_myBindpoint.SceneName = SceneManager.GetActiveScene().name;

                player.Cmd_UCE_SetBindpoint();
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
}