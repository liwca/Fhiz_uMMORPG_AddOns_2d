// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;

// =======================================================================================
// UCE DEATH SKILL
// =======================================================================================
[System.Serializable]
public partial class UCE_DeathSkill
{
    [SerializeField] public ScriptableSkill[] deathSkill;
    public int deathSkillMinLevel = 1;
    public int deathSkillMaxLevel = 2;
    [SerializeField] [Range(0, 1)] public float deathSkillChance = 1.0f;
}

// =======================================================================================