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
// BINDPOINT AREA
// =======================================================================================
[RequireComponent(typeof(Collider2D))]
public class UCE_Area_Bindpoint : NetworkBehaviour
{
    [Header("-=-=-=- Bindpoint -=-=-=-")]
    public Transform bindpoint;

    [Header("-=-=-=- Popups -=-=-=-")]
    public UCE_PopupClass enterPopup;

    [Header("[EDITOR]")]
    public Color gizmoColor = new Color(0, 1, 1, 0.25f);
    public Color gizmoWireColor = new Color(1, 1, 1, 0.8f);

    // -----------------------------------------------------------------------------------
    // OnDrawGizmos
    // @Editor
    // -----------------------------------------------------------------------------------
    private void OnDrawGizmos()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        // we need to set the gizmo matrix for proper scale & rotation
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(collider.offset, collider.size);
        Gizmos.color = gizmoWireColor;
        Gizmos.DrawWireCube(collider.offset, collider.size);
        Gizmos.matrix = Matrix4x4.identity;
    }

    // -------------------------------------------------------------------------------
    // OnTriggerEnter
    // -------------------------------------------------------------------------------
    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D co)
    {
        Player player = co.GetComponentInParent<Player>();
        if (player && bindpoint != null && player.UCE_myBindpoint.name != bindpoint.gameObject.name)
        {
            player.UCE_SetBindpointFromArea(bindpoint.gameObject.name, bindpoint.position.x, bindpoint.position.y, bindpoint.position.z);
            player.UCE_ShowPopup(enterPopup.message, enterPopup.iconId, enterPopup.soundId);
        }
    }

    // -------------------------------------------------------------------------------
}

// =======================================================================================