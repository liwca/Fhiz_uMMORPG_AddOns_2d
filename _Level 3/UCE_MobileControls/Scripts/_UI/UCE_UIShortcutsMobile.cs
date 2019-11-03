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
// UIShortcuts
// =======================================================================================
public partial class UIShortcuts : MonoBehaviour
{
    [Header("Link the new show/close Buttons here:")]
    public GameObject showButtonObject;
    public Button closeButton;
    private Button showButton;
    private bool panelVisible;

    private void Start()
    {
        if (showButtonObject)
        {
            showButtonObject.SetActive(false);
            showButton = showButtonObject.GetComponent<Button>();
        }
    }

    private void Update_Mobile()
    {
        Player player = Player.localPlayer;

        if (!player || !showButtonObject || !closeButton)
            return;

        panel.SetActive(panelVisible);
        showButtonObject.SetActive(!panelVisible);

        // Stue Show Button
        showButton.onClick.SetListener(() =>
        {
            panel.SetActive(true);
            panelVisible = true;
        });

        // Stue Close Button
        closeButton.onClick.SetListener(() =>
        {
            panel.SetActive(false);
            panelVisible = false;
        });
    }
}