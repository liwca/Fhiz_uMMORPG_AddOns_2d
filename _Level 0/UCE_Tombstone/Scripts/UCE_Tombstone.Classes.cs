// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;

// =======================================================================================
// UCE TOMBSTONE
// =======================================================================================
[System.Serializable]
public partial class UCE_Tombstone
{
    public GameObject[] tombstone;
    [Range(0, 1)] public float tombstoneChance = 1.0f;
    [Range(0, 1)] public float tombstoneFallHeight = 0.5f;
}

// =======================================================================================