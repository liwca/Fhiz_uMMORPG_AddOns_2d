// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;

#if _iMMOTOOLS && _FHIZCURSEDEQUIPMENT

// =======================================================================================
// SKILL TARGET UNCURSE
// =======================================================================================
[CreateAssetMenu(menuName = "UCE Skills/UCE Skill Target Uncurse", order = 999)]
public class UCE_SkillTargetUncurse : HealSkill
{
    [Header("-=-=-=- UCE Target Uncurse Skill -=-=-=-")]
    [Tooltip("Remove cursed items with a curse level less or equal to")]
    public int maxCurseLevel;

    [Header("-=-=-=- Buff on Target -=-=-=-")]
    public BuffSkill applyBuff;
    public LinearInt buffLevel;
    public LinearFloat buffChance;

    [Header("-=-=-=- Targeting -=-=-=-")]
    public bool canHealSelf = true;
    public bool canHealOthers = false;

    // -----------------------------------------------------------------------------------
    //
    // -----------------------------------------------------------------------------------
    // helper function to determine the target that the skill will be cast on
    // (e.g. cast on self if targeting a monster that isn't healable)
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

        // no valid target? try to cast on self or don't cast at all
        return canHealSelf ? caster : null;
    }

    // -----------------------------------------------------------------------------------
    //
    // -----------------------------------------------------------------------------------
    public override bool CheckTarget(Entity caster)
    {
        // correct the target
        caster.target = CorrectedTarget(caster);

        // can only buff the target if its alive
        return caster.target != null && caster.target.isAlive;
    }

    // -----------------------------------------------------------------------------------
    //
    // -----------------------------------------------------------------------------------
    // (has corrected target already)
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
    // (has corrected target already)
    public override void Apply(Entity caster, int skillLevel)
    {
        // apply only to alive people
        if (caster.target != null && caster.target is Player && caster.target.isAlive)
        {
            ((Player)caster.target).UCE_UnequipCursedEquipment(maxCurseLevel);

            caster.target.health += healsHealth.Get(skillLevel);
            caster.target.mana += healsMana.Get(skillLevel);

            caster.target.UCE_ApplyBuff(applyBuff, buffLevel.Get(skillLevel), buffChance.Get(skillLevel));

            // show effect on target
            SpawnEffect(caster, caster.target);
        }
    }

    // -----------------------------------------------------------------------------------
}

#endif

// =======================================================================================