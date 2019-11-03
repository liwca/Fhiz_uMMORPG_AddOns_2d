// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;

// =======================================================================================
// CONVERT SKILL
// =======================================================================================
[CreateAssetMenu(menuName = "uMMORPG Skill/UCE Skill Convert", order = 999)]
public class UCE_SkillConvert : ScriptableSkill
{
    [Header("-=-=-=- UCE Convert Skill -=-=-=-")]
    public LinearFloat successChance;

    [Tooltip("[Optional] Maximum level of the target (0 to disable)")]
    public LinearInt maxTargetLevel;

    [Tooltip("[Optional] Uses player level as a base to calculate max Level of monster")]
    public bool basePlayerLevel;
    public string convertMessage = "You converted: ";
    public string failedMessage = "You failed to convert: ";
    public string errorMessage = "You cannot convert that target!";

    // -----------------------------------------------------------------------------------
    // CheckTarget
    // -----------------------------------------------------------------------------------
    public override bool CheckTarget(Entity caster)
    {
        return caster.target != null && caster.CanAttack(caster.target);
    }

    // -----------------------------------------------------------------------------------
    // CheckDistance
    // -----------------------------------------------------------------------------------
    public override bool CheckDistance(Entity caster, int skillLevel, out Vector2 destination)
    {
        if (caster.target != null && caster.target is Monster)
        {
            int maxLevel = maxTargetLevel.Get(skillLevel);

            if (basePlayerLevel)
                maxLevel += caster.level;

            Monster monster = caster.target.GetComponent<Monster>();

            if (monster.level <= maxLevel)
            {
                destination = caster.target.collider.ClosestPointOnBounds(caster.transform.position);
                return Utils.ClosestDistance(caster.collider, caster.target.collider) <= castRange.Get(skillLevel);
            }
        }

        ((Player)caster).UCE_TargetAddMessage(errorMessage);
        destination = caster.transform.position;
        return false;
    }

    // -----------------------------------------------------------------------------------
    // Apply
    // -----------------------------------------------------------------------------------
    public override void Apply(Entity caster, int skillLevel)
    {
        Player player = (Player)caster;

        if (player.target != null && player.target is Monster)
        {
            int maxLevel = maxTargetLevel.Get(skillLevel);

            if (basePlayerLevel)
                maxLevel += player.level;

            Monster monster = player.target.GetComponent<Monster>();

            if (monster.level <= maxLevel && UnityEngine.Random.value <= successChance.Get(skillLevel))
            {
                monster.UCE_setRealm(player.hashRealm, player.hashAlly);
                player.UCE_TargetAddMessage(convertMessage + monster.name);
            }
            else
            {
                player.UCE_TargetAddMessage(failedMessage + monster.name);
            }
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================