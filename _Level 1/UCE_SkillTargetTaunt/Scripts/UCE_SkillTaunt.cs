// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;

// =======================================================================================
// TAUNT SKILL
// =======================================================================================
[CreateAssetMenu(menuName = "uMMORPG Skill/UCE Skill Taunt", order = 999)]
public class UCE_SkillTaunt : ScriptableSkill
{
    [Header("-=-=-=- UCE Taunt Skill -=-=-=-")]
    public LinearFloat successChance;
    public string tauntMessage = "You taunted: ";
    public string failedMessage = "You failed to taunt: ";

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
        Player player = (Player)caster;

        if (player.target != null && player.target is Monster)
        {
            Monster monster = player.target.GetComponent<Monster>();

            if (UnityEngine.Random.value <= successChance.Get(skillLevel))
            {
                monster.UCE_OnAggro(player, 1);
                player.UCE_TargetAddMessage(tauntMessage + monster.name);
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