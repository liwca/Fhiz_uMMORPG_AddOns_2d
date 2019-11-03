using Mirror;
using UnityEngine;

// =======================================================================================
// TRAVELROUTE - BOX COLLIDER
// =======================================================================================
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(NetworkIdentity))]
public class UCE_AreaBox_Travelroute : NetworkBehaviour
{
    [Header("-=-=-=- UCE TRAVELROUTES -=-=-=-")]
    [Tooltip("[Optional] One click deactivation")]
    public bool isActive = true;
    protected UCE_UI_Travelroute instance;

    [Tooltip("[Required] Travelroutes available on enter")]
    public UCE_Travelroute[] Travelroutes;

    [Tooltip("[Optional] Travelroutes unlocked on enter")]
    public UCE_Unlockroute[] Unlockroutes;

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
    // OnTriggerEnter
    // @Client
    // -----------------------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D co)
    {
        Player player = co.GetComponentInParent<Player>();

        if (player != null && player.isAlive && isActive)
        {
            player.UCE_myTravelrouteArea = this;
            player.UCE_UnlockTravelroutes();

            if (instance == null)
                instance = FindObjectOfType<UCE_UI_Travelroute>();

            instance.Show();
        }
    }

    // -----------------------------------------------------------------------------------
    // OnTriggerExit
    // @Client
    // -----------------------------------------------------------------------------------
    private void OnTriggerExit2D(Collider2D co)
    {
        Player player = co.GetComponentInParent<Player>();

        if (player != null && player.isAlive && isActive)
        {
            player.UCE_myTravelrouteArea = null;

            if (instance == null)
                instance = FindObjectOfType<UCE_UI_Travelroute>();

            instance.Hide();
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================