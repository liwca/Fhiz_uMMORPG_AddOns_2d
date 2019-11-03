// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using System.Collections.Generic;
using UnityEngine;

// =======================================================================================
// TARGET CURATIVE SKILL
// =======================================================================================
[CreateAssetMenu(menuName = "UCE Skills/UCE Target Curative Skill", order = 999)]
public class UCE_TargetCurativeSkill : UCE_CurativeSkill
{
    // -----------------------------------------------------------------------------------
    // CheckTarget
    // -----------------------------------------------------------------------------------
    public override bool CheckTarget(Entity caster)
    {
        return caster.target != null || affectSelf;
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
    // Apply
    // -----------------------------------------------------------------------------------
    public override void Apply(Entity caster, int skillLevel)
    {
        List<Entity> targets = new List<Entity>();

        if (SpawnEffectOnMainTargetOnly)
            SpawnEffect(caster, caster.target);

        if (caster.target is Player && ((Player)caster).UCE_SameCheck((Player)caster.target, affectSelf, affectPlayers, affectOwnParty, affectOwnGuild, affectOwnRealm, reverseTargeting))
        {
            if (reviveChance.Get(skillLevel) > 0 && !caster.target.isAlive)
                targets.Add(caster.target);
            else if (reviveChance.Get(skillLevel) <= 0 && caster.target.isAlive)
                targets.Add(caster.target);
        }
        else if (caster.target is Monster && affectEnemies)
        {
            if (reviveChance.Get(skillLevel) > 0 && !caster.target.isAlive)
                targets.Add(caster.target);
            else if (reviveChance.Get(skillLevel) <= 0 && caster.target.isAlive)
                targets.Add(caster.target);
        }

        if (castRadius.Get(skillLevel) > 0)
        {
            if (caster is Player)
                targets.AddRange(((Player)caster).UCE_GetCorrectedTargetsInSphere(caster.target.transform, castRadius.Get(skillLevel), reviveChance.Get(skillLevel) > 0, affectSelf, affectOwnParty, affectOwnGuild, affectOwnRealm, reverseTargeting, affectPlayers, affectEnemies));
            else
                targets.AddRange(caster.UCE_GetCorrectedTargetsInSphere(caster.target.transform, castRadius.Get(skillLevel), reviveChance.Get(skillLevel) > 0, affectSelf, affectOwnParty, affectOwnGuild, affectOwnRealm, reverseTargeting, affectPlayers, affectEnemies));
        }

        ApplyToTargets(targets, caster, skillLevel);

        targets.Clear();
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================