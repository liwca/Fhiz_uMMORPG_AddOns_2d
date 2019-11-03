// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;

// =======================================================================================
// SKILL DEBUFF
// =======================================================================================
[CreateAssetMenu(menuName = "uMMORPG Skill/UCE Skill Debuff", order = 999)]
public class UCE_SkillDebuff : HealSkill
{
    [Header("-=-=-=- UCE Skill Debuff -=-=-=-")]
    [Tooltip("% Chance to remove the Buff from the target")]
    public LinearFloat successChance;
    public BuffSkill[] removeBuffs;

    [Header("-=-=-=- New Buff on Target? -=-=-=-")]
    public BuffSkill applyBuff;
    public LinearInt buffLevel;
    public LinearFloat buffChance;

    [Header("-=-=-=- Targeting -=-=-=-")]
    public bool canHealSelf = true;
    public bool canHealOthers = false;

    // -----------------------------------------------------------------------------------
    // CorrectedTarget
    // -----------------------------------------------------------------------------------
    private Entity CorrectedTarget(Entity caster)
    {
        // targeting nothing? then try to cast on self
        if (caster.target == null)
            return canHealSelf ? caster : null;

        // targeting self?
        if (caster.target == caster)
            return canHealSelf ? caster : null;

        // targeting someone of same type? buff them or self
        if (caster.target.GetType() == caster.GetType())
        {
            if (canHealOthers)
                return caster.target;
            else if (canHealSelf)
                return caster;
            else
                return null;
        }
        else if (caster.target is Monster)
        {
            if (canHealOthers)
                return caster.target;
        }

        // no valid target? try to cast on self or don't cast at all
        return canHealSelf ? caster : null;
    }

    // -----------------------------------------------------------------------------------
    // CheckTarget
    // -----------------------------------------------------------------------------------
    public override bool CheckTarget(Entity caster)
    {
        // correct the target
        caster.target = CorrectedTarget(caster);

        // can only buff the target if it's alive
        return caster.target != null && caster.target.isAlive;
    }

    // -----------------------------------------------------------------------------------
    // CheckDistance
    // -----------------------------------------------------------------------------------
    public override bool CheckDistance(Entity caster, int skillLevel, out Vector2 destination)
    {
        // target still around?
        if (caster.target != null)
        {
            destination = caster.target.collider.ClosestPointOnBounds(caster.transform.position);
            return Utils.ClosestDistance(caster.collider, caster.target.collider) <= castRange.Get(skillLevel);
        }
        destination = caster.transform.position;
        return false;
    }

    // -----------------------------------------------------------------------------------
    //
    // -----------------------------------------------------------------------------------
    public override void Apply(Entity caster, int skillLevel)
    {
        // apply only to alive people
        if (caster.target != null && caster.target.isAlive)
        {
            foreach (BuffSkill removeBuff in removeBuffs)
                caster.target.UCE_RemoveBuff(removeBuff, successChance.Get(skillLevel));

            caster.target.health += healsHealth.Get(skillLevel);
            caster.target.mana += healsMana.Get(skillLevel);

            caster.target.UCE_ApplyBuff(applyBuff, buffLevel.Get(skillLevel), buffChance.Get(skillLevel));

            SpawnEffect(caster, caster.target);
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================