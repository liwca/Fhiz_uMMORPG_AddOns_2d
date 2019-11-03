// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using System;
using UnityEngine;

// -----------------------------------------------------------------------------------
// UCE_PlaceableObjectUpgradeCost
// -----------------------------------------------------------------------------------
[Serializable]
public partial class UCE_PlaceableObjectUpgradeCost
{
    [Tooltip("Minimum level of owner in order to upgrade")]
    public int minLevel;

    [Tooltip("Next level upgrade: Duration in seconds")]
    public float duration;

    [Tooltip("Next level upgrade: Gold Cost")]
    public long gold;

    [Tooltip("Next level upgrade: Coins Cost")]
    public long coins;

    [Tooltip("Next level upgrade: Items Cost")]
    public UCE_ItemRequirement[] requiredItems;
}

// =======================================================================================