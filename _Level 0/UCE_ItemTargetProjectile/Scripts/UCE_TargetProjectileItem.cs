// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using System.Text;
using UnityEngine;

// =======================================================================================
// TARGET PROJECTILE - ITEM
// =======================================================================================
[CreateAssetMenu(menuName = "uMMORPG Item/UCE TargetProjectileItem", order = 999)]
public class UCE_TargetProjectileItem : UsableItem
{
    [Header("-=-=-=- UCE Target Projectile Item -=-=-=-")]
    public TargetProjectileSkill projectile;
    public int level;

    // -----------------------------------------------------------------------------------
    // CheckDistance
    // -----------------------------------------------------------------------------------
    public bool CheckDistance(Entity caster, int skillLevel, out Vector2 destination)
    {
        if (caster.target != null)
        {
            destination = caster.target.collider.ClosestPointOnBounds(caster.transform.position);
            return Utils.ClosestDistance(caster.collider, caster.target.collider) <= projectile.castRange.Get(skillLevel);
        }
        destination = caster.transform.position;
        return false;
    }

    // -----------------------------------------------------------------------------------
    // Use
    // @Server
    // -----------------------------------------------------------------------------------
    public override void Use(Player player, int inventoryIndex)
    {
        Vector2 destination;

        if (projectile != null &&
            level > 0 &&
            player.target != null &&
            player.CanAttack(player.target) &&
            CheckDistance(player, level, out destination)
            )
        {
            // always call base function too
            base.Use(player, inventoryIndex);

            // launch projectile
            projectile.Apply(player, level);

            // decrease amount
            ItemSlot slot = player.inventory[inventoryIndex];
            slot.DecreaseAmount(1);
            player.inventory[inventoryIndex] = slot;
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================