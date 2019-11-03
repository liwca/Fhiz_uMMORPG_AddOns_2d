// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using Mirror;
using UnityEngine;

// ===================================================================================
// UCE TIMEGATE
// ===================================================================================
[RequireComponent(typeof(BoxCollider2D))]
public class UCE_Area_Timegate : NetworkBehaviour
{
    [Header("[-=-=-=- UCE TIMEGATE -=-=-=-]")]
    [Tooltip("One click deactivation")]
    public bool isActive = true;

    [Tooltip("[Required] Any on scene Transform or GameObject OR off scene coordinates (requires UCE Network Zones AddOn)")]
    public UCE_TeleportationTarget teleportationTarget;

    [Tooltip("Maximum number of visits while the gate is open")]
    public int maxVisits = 10;

    [Tooltip("Minimum number of hours that must pass between visits while open")]
    public int hoursBetweenVisits = 10;

    [Tooltip("The day this timegate will open (set 0 to disable)"), Range(0, 31)]
    public int dayStart = 1;

    [Tooltip("The day this timegate will close (set 0 to disable)"), Range(0, 31)]
    public int dayEnd = 1;

    [Tooltip("The month this timegate is open (set 0 to disable)"), Range(0, 12)]
    public int activeMonth = 1;

    protected UCE_UI_Timegate _UCE_UI_Timegate;

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
    private void OnTriggerEnter2D(Collider2D co)
    {
        Player player = co.GetComponentInParent<Player>();
        if (player && isActive && teleportationTarget.Valid)
        {
            player.UCE_myTimegate = this;

            if (!_UCE_UI_Timegate)
                _UCE_UI_Timegate = FindObjectOfType<UCE_UI_Timegate>();

            _UCE_UI_Timegate.Show();
        }
    }

    // -------------------------------------------------------------------------------
    // OnTriggerExit
    // -------------------------------------------------------------------------------
    private void OnTriggerExit2D(Collider2D co)
    {
        Player player = co.GetComponentInParent<Player>();
        if (player && isActive && teleportationTarget.Valid)
        {
            player.UCE_myTimegate = null;
            if (!_UCE_UI_Timegate)
                _UCE_UI_Timegate = FindObjectOfType<UCE_UI_Timegate>();

            _UCE_UI_Timegate.Hide();
        }
    }

    // -------------------------------------------------------------------------------
}

// =======================================================================================