// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

// =======================================================================================
// UCE SHARED INSTANCE MANAGER
// =======================================================================================
public class UCE_SharedInstanceManager : MonoBehaviour
{
    public UCE_SharedInstanceEntry[] sharedInstances;

    // -----------------------------------------------------------------------------------
    // getAvailableSharedInstances
    // Retrieve a list of shared instances the player is allowed to see
    // -----------------------------------------------------------------------------------
    public List<UCE_SharedInstanceEntry> getAvailableSharedInstances(Player player, int instanceCategory)
    {
        return sharedInstances.Where(x => x.instanceCategory == instanceCategory && x.canPlayerSee(player)).ToList();
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================