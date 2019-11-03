using UnityEngine;
using UnityEngine.UI;

// =======================================================================================
// NPC DIALOGUE
// =======================================================================================
public partial class UCE_UI_HonorShop_NpcDialogue : MonoBehaviour
{
    public GameObject panel;
    public GameObject shopPanel;
    public Button shopButton;

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

            shopButton.gameObject.SetActive(npc.offersShop);
            shopButton.onClick.SetListener(() =>
            {
                shopPanel.SetActive(true);
                panel.SetActive(false);
            });
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================