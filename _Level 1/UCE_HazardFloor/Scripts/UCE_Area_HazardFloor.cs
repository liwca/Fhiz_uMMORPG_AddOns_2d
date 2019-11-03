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
// HAZARD FLOOR
// =======================================================================================
[RequireComponent(typeof(Collider2D))]
public class UCE_Area_HazardFloor : NetworkBehaviour
{
    [Header("-=-=-=- Hazard Floor Settings -=-=-=-")]
    public UCE_HazardBuff[] onEnterBuff;
    public TargetBuffSkill[] onExitBuff;

    [Header("-=-=-=- Popups -=-=-=-")]
    public UCE_PopupClass enterPopup;
    public UCE_PopupClass exitPopup;

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

    // -----------------------------------------------------------------------------------
    // Start
    // -----------------------------------------------------------------------------------
    public virtual void Start()
    {
        foreach (UCE_HazardBuff buff in onEnterBuff)
        {
            if (buff.buff != null)
            {
                buff.protectiveRequirements.setParent(this.gameObject);
            }
        }
    }

    // -------------------------------------------------------------------------------
    // OnTriggerEnter
    // -------------------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D co)
    {
        Entity entity = co.GetComponentInParent<Entity>();
        if (entity)
        {
            entity.UCE_HazardFloorEnter(onEnterBuff);

            if (entity is Player)
                ((Player)entity).UCE_ShowPopup(enterPopup.message, enterPopup.iconId, enterPopup.soundId);
        }
    }

    // -------------------------------------------------------------------------------
    // OnTriggerExit
    // -------------------------------------------------------------------------------
    private void OnTriggerExit2D(Collider2D co)
    {
        Entity entity = co.GetComponentInParent<Entity>();
        if (entity)
        {
            entity.UCE_HazardFloorLeave(onExitBuff);

            if (entity is Player)
                ((Player)entity).UCE_ShowPopup(exitPopup.message, exitPopup.iconId, exitPopup.soundId);
        }
    }

    // -------------------------------------------------------------------------------
}

// =======================================================================================