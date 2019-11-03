using UnityEngine;
using Mirror;
using System.Collections;

// ===================================================================================
// JUKEBOX AREA
// ===================================================================================
[RequireComponent(typeof(BoxCollider2D))]
public class UCE_Area_Jukebox : MonoBehaviour
{
    [Header("[-=-=- UCE JUKEBOX AREA -=-=-]")]
    [Tooltip("The audio clip that is played while the player is inside this area")]
    public AudioClip areaMusicClip;

    [Tooltip("The duration it takes to fade the music in/out when entering/leaving the area")]
    public float fadeInFadeOut = 2.0f;

    [Range(0, 1)] public float adjustedVolume;

    [Tooltip("Set to true if you want the music to loop while the player stays inside the area")]
    public bool loop = true;

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
        if (!NetworkClient.active || areaMusicClip == null) return;

        Player player = Player.localPlayer;
        if (!player) return;

        Player entity = co.GetComponentInParent<Player>();

        if (entity != null && entity == player)
            UCE_Jukebox.singleton.PlayBGM(areaMusicClip, fadeInFadeOut, adjustedVolume, loop);
    }

    // -------------------------------------------------------------------------------
    // OnTriggerExit
    // -------------------------------------------------------------------------------
    private void OnTriggerExit2D(Collider2D co)
    {
        if (!NetworkClient.active || areaMusicClip == null) return;

        Player player = Player.localPlayer;
        if (!player) return;

        Player entity = co.GetComponentInParent<Player>();

        if (entity != null && entity == player)
            UCE_Jukebox.singleton.revertBGM(areaMusicClip, fadeInFadeOut, adjustedVolume);
    }

    // -------------------------------------------------------------------------------
}

// =======================================================================================