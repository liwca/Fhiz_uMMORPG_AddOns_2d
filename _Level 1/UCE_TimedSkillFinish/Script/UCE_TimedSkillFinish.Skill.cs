// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;
using Mirror;

// =======================================================================================
// SKILL
// =======================================================================================
public partial struct Skill
{
    // -----------------------------------------------------------------------------------
    // UCE_CastTimePassed
    // -----------------------------------------------------------------------------------
    public double UCE_CastTimePassed() => (NetworkTime.time - castTimeEnd) + castTime;
}