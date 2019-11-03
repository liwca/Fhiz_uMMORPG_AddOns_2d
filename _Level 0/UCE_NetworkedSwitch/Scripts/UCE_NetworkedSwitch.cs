// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;
using Mirror;
using System.Linq;

// =======================================================================================
// NETWORKED SWITCH
// =======================================================================================
public partial class UCE_NetworkedSwitch : UCE_InteractableObject
{
    public GameObject[] activatedObjects;
    public bool visible = true;

    // -----------------------------------------------------------------------------------
    // OnInteractClient
    // @Client
    // -----------------------------------------------------------------------------------
    [ClientCallback]
    public override void OnInteractClient(Player player) { }

    // -----------------------------------------------------------------------------------
    // OnInteractServer
    // @Server
    // -----------------------------------------------------------------------------------
    [ServerCallback]
    public override void OnInteractServer(Player player)
    {
        visible = !visible;

        foreach (GameObject go in activatedObjects)
            go.GetComponent<UCE_ActivateableObject>().Toggle(visible);
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================