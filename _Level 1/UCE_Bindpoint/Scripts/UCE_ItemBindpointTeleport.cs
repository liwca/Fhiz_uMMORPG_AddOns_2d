using UnityEngine;

// =======================================================================================
// PORTABLE TELEPORT - ITEM
// =======================================================================================
[CreateAssetMenu(menuName = "uMMORPG Item/UCE Bindpoint Teleport", order = 998)]
public class UCE_ItemBindpointTeleport : UsableItem
{
    [Header("-=-=-=- Teleport to current Bindpoint -=-=-=-")]
    [Tooltip("Decrease amount by how many each use (can be 0)?")]
    public int decreaseAmount = 1;

    // -----------------------------------------------------------------------------------
    // Use
    // @Server
    // -----------------------------------------------------------------------------------
    public override void Use(Player player, int inventoryIndex)
    {
        ItemSlot slot = player.inventory[inventoryIndex];

        // -- Only activate if enough charges left + a bindpoint has been set
        if (
            player.UCE_myBindpoint.Valid &&
            (decreaseAmount == 0 || slot.amount >= decreaseAmount)
            )
        {
            // always call base function too
            base.Use(player, inventoryIndex);

            // -- Decrease Amount
            if (decreaseAmount != 0)
            {
                slot.DecreaseAmount(decreaseAmount);
                player.inventory[inventoryIndex] = slot;
            }

            // -- Activate Teleport
            player.agent.Warp(player.UCE_myBindpoint.position);
        }
    }

    // -----------------------------------------------------------------------------------
}