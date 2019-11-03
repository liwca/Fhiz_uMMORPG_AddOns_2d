// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

// ===================================================================================
// UCE NETWORK PORTAL (BOX)
// ===================================================================================
public class UCE_NetworkZonePortalBox : UCE_InteractableAreaBox
{
    [Header("[-=-=- UCE NETWORK ZONE PORTAL -=-=-]")]
    public SceneLocation targetScene;

    // -----------------------------------------------------------------------------------
    // OnInteractClient
    // @Client
    // -----------------------------------------------------------------------------------
    [ClientCallback]
    public override void OnInteractClient(Player player)
    {
        UCE_UI_Tools.FadeOutScreen(false, 0.25f);
    }

    // -----------------------------------------------------------------------------------
    // OnInteractServer
    // @Server
    // -----------------------------------------------------------------------------------
    [ServerCallback]
    public override void OnInteractServer(Player player)
    {
        player.UCE_OnPortal(targetScene);
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================