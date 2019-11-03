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
// RADIUS CURATIVE SKILL
// =======================================================================================
[CreateAssetMenu(menuName = "UCE Skills/UCE Radius Curative Skill", order = 999)]
public class UCE_RadiusCurativeSkill : UCE_CurativeSkill
{
    // -----------------------------------------------------------------------------------
    // CheckTarget
    // -----------------------------------------------------------------------------------
    public override bool CheckTarget(Entity caster)
    {
        return true;
    }

    // -----------------------------------------------------------------------------------
    // CheckDistance
    // -----------------------------------------------------------------------------------
    public override bool CheckDistance(Entity caster, int skillLevel, out Vector2 destination)
    {
        destination = caster.transform.position;
        return true;
    }

    // -----------------------------------------------------------------------------------
    // Apply
    // -----------------------------------------------------------------------------------
    public override void Apply(Entity caster, int skillLevel)
    {
        List<Entity> targets = new List<Entity>();

        if (affectSelf)
            targets.Add(caster);

        if (caster is Player)
            targets.AddRange(((Player)caster).UCE_GetCorrectedTargetsInSphere(caster.transform, castRadius.Get(skillLevel), reviveChance.Get(skillLevel) > 0, affectSelf, affectOwnParty, affectOwnGuild, affectOwnRealm, reverseTargeting, affectPlayers, affectEnemies));
        else
            targets.AddRange(caster.UCE_GetCorrectedTargetsInSphere(caster.transform, castRadius.Get(skillLevel), reviveChance.Get(skillLevel) > 0, affectSelf, affectOwnParty, affectOwnGuild, affectOwnRealm, reverseTargeting, affectPlayers, affectEnemies));

        ApplyToTargets(targets, caster, skillLevel);

        targets.Clear();
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================