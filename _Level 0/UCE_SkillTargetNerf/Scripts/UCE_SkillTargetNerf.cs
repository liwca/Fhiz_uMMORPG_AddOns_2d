// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using Mirror;
using UnityEngine;

// =======================================================================================
// SKILL TARGET NERF
// =======================================================================================

[CreateAssetMenu(menuName = "uMMORPG Skill/UCE Skill Target Nerf", order = 999)]
public class UCE_SkillTargetNerf : DamageSkill
{
    [Header("-=-=-=- Skill Effect on Caster -=-=-=-")]
    public OneTimeTargetSkillEffect effect;

    [Header("-=-=-=- New Buff on Target? -=-=-=-")]
    public BuffSkill applyBuff;
    public LinearInt buffLevel;
    public LinearFloat buffChance;

    public override bool CheckTarget(Entity caster)
    {
        // target exists, alive, not self, oktype?
        return caster.target != null && caster.CanAttack(caster.target);
    }

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

    // helper function to spawn the skill effect on caster
    // (used by all the buff implementations and to load them after saving)
    public void SpawnEffect(Entity caster, Entity spawnTarget)
    {
        if (effect != null)
        {
            GameObject go = Instantiate(effect.gameObject, caster.effectMount.transform.position, Quaternion.identity);
            go.transform.LookAt(new Vector3(spawnTarget.transform.position.x, spawnTarget.transform.position.y, spawnTarget.transform.position.z));
            go.GetComponent<OneTimeTargetSkillEffect>().target = caster;
            NetworkServer.Spawn(go);
        }
    }

    // events for client sided effects /////////////////////////////////////////
    // [Client]
    public override void OnCastStarted(Entity caster)
    {
        base.OnCastStarted(caster);

        SpawnEffect(caster, caster.target);
    }

    public override void Apply(Entity caster, int skillLevel)
    {
        // deal damage directly with base damage + skill damage
        caster.DealDamageAt(caster.target,
                            caster.damage + damage.Get(skillLevel),
                            stunChance.Get(skillLevel),
                            stunTime.Get(skillLevel));

        // apply the buff
        caster.target.UCE_ApplyBuff(applyBuff, buffLevel.Get(skillLevel), buffChance.Get(skillLevel));
    }
}