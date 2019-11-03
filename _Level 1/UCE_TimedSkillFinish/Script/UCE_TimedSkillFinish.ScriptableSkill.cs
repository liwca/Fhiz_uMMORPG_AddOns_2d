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
// SCRIPTABLE SKILL
// =======================================================================================
public abstract partial class ScriptableSkill : ScriptableObject
{
    [Header("[UCE Timed Skill Finish]")]
    public float applySkillEarlier = 0;
}