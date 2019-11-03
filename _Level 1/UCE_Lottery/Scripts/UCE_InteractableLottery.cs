// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using Mirror;
using UnityEngine;

// =======================================================================================
// INTERACTABLE LOTTERY
// =======================================================================================
public partial class UCE_InteractableLottery : UCE_InteractableObject
{
    [Header("-=-=-=- UCE Lottery Object -=-=-=-")]
    public UCE_InteractionRewards rewards;

    // -----------------------------------------------------------------------------------
    // OnInteractServer
    // @Server
    // -----------------------------------------------------------------------------------
    [ServerCallback]
    public override void OnInteractServer(Player player)
    {
        rewards.gainRewards(player);
    }
}

// =======================================================================================