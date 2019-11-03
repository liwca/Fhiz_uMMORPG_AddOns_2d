using UnityEngine;
using UnityEngine.UI;

// =======================================================================================
// NPC DIALOGUE - HEALER
// =======================================================================================
public partial class UCE_UI_Healer_NpcDialogue : MonoBehaviour
{
    public GameObject panel;
    public GameObject healerPanel;
    public Button healerButton;

    // -----------------------------------------------------------------------------------
    // Update
    // -----------------------------------------------------------------------------------
    public void Update()
    {
        Player player = Player.localPlayer;
        if (!player) return;

        if (!panel.activeSelf) return;

        // use collider point(s) to also work with big entities
        if (panel.activeSelf &&
            player.target != null && player.target is Npc &&
            Utils.ClosestDistance(player.collider, player.target.collider) <= player.interactionRange)
        {
            Npc npc = (Npc)player.target;

            healerButton.gameObject.SetActive(npc.healingServices.Valid(player));
            healerButton.onClick.SetListener(() =>
            {
                healerPanel.SetActive(true);
                panel.SetActive(false);
            });
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================