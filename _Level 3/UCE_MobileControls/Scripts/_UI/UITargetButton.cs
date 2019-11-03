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
// UITargetButton
// =======================================================================================
public class UITargetButton : MonoBehaviour
{
    private Button targetButton;

    private void Start()
    {
        targetButton = GetComponent<Button>();
        if (targetButton != null)
            targetButton.onClick.SetListener(() => SelectTarget());
    }

    private void SelectTarget()
    {
        Player player = Player.localPlayer;
        if (!player) return;
        player.TargetNearestButton();
    }
}