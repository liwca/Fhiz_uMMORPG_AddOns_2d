// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using Mirror;

// =======================================================================================
// ENTITY
// =======================================================================================
public partial class Entity
{
    // -----------------------------------------------------------------------------------
    // UCE_EventSkillFinished
    // -----------------------------------------------------------------------------------
    public bool UCE_EventSkillFinished()
    {
        return
                0 <= currentSkill &&
                currentSkill < skills.Count &&
                skills[currentSkill].data.applySkillEarlier != 0 &&
                   skills[currentSkill].UCE_CastTimePassed() >= skills[currentSkill].data.applySkillEarlier;
    }

    // -----------------------------------------------------------------------------------
    // UCE_FinishCastSkillEarly
    // -----------------------------------------------------------------------------------
    public void UCE_FinishCastSkillEarly(Skill skill)
    {
        if (CastCheckSelf(skill, false) && CastCheckTarget(skill))
        {
            skill.Apply(this);
            mana -= skill.manaCosts;
            skills[currentSkill] = skill;
        }
        else
        {
            currentSkill = -1;
        }
    }

    // -----------------------------------------------------------------------------------
    // UCE_FinishCastSkillLate
    // -----------------------------------------------------------------------------------
    public void UCE_FinishCastSkillLate(Skill skill)
    {
        RpcSkillCastFinished(skill);
        skill.cooldownEnd = NetworkTime.time + skill.cooldown;
        skills[currentSkill] = skill;
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================