// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;

// =======================================================================================
// Teleportation Destination
// =======================================================================================
[System.Serializable]
public partial class UCE_TeleportationDestination
{
    [Tooltip("[Optional] All of those requirements must be met in order for the teleporter to be active")]
    public UCE_Requirements teleportationRequirement;

    [Tooltip("[Required] Any on scene Transform or GameObject OR off scene coordinates (requires UCE Network Zones AddOn)")]
    public UCE_TeleportationTarget teleportationTarget;
}

// =======================================================================================