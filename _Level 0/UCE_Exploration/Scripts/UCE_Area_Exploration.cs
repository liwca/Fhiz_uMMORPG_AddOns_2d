// =======================================================================================
// Created and maintained by iMMO
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;
using Mirror;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// =======================================================================================
// EXPLORATION AREA
// =======================================================================================
[RequireComponent(typeof(CircleCollider2D))]
public class UCE_Area_Exploration : NetworkBehaviour
{
    [Header("[-=-=-=- UCE Exploration Area -=-=-=-]")]
    [Tooltip("[Optional] Always show the area name on enter (even if already explored)")]
    public bool noticeOnEnter = true;

    [Header("[Requirements]")]
    public UCE_Requirements explorationRequirements;

    [Header("[Rewards]")]
    public UCE_InteractionRewards explorationRewards;

    [Header("[Popups]")]
    [Tooltip("[Optional] When first explored - shown together with the area name")]
    public UCE_PopupClass explorePopup;

    [Tooltip("[Optional] Shown when entered - together with area name")]
    public UCE_PopupClass enterPopup;

    [Header("[Editor]")]
    public Color gizmoColor = new Color(0, 1, 1, 0.25f);
    public Color gizmoWireColor = new Color(1, 1, 1, 0.8f);

    // -----------------------------------------------------------------------------------
    // OnDrawGizmos
    // @Editor
    // -----------------------------------------------------------------------------------
    private void OnDrawGizmos()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(collider.offset, collider.radius);
        Gizmos.color = gizmoWireColor;
        Gizmos.DrawWireSphere(collider.offset, collider.radius);
        Gizmos.matrix = Matrix4x4.identity;
    }

    // -----------------------------------------------------------------------------------
    // OnTriggerEnter
    // -----------------------------------------------------------------------------------
    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D co)
    {
        Player player = co.GetComponentInParent<Player>();
        if (player && explorationRequirements.isActive && explorationRequirements.checkRequirements(player))
        {
            player.UCE_myExploration = this;
            player.UCE_ExploreArea();
        }
    }

    // -----------------------------------------------------------------------------------
    // OnTriggerExit
    // -----------------------------------------------------------------------------------
    [ServerCallback]
    private void OnTriggerExit2D(Collider2D co)
    {
        Player player = co.GetComponentInParent<Player>();
        if (player && explorationRequirements.isActive && explorationRequirements.checkRequirements(player))
        {
            player.UCE_myExploration = null;
            player.UCE_MinimapSceneText(SceneManager.GetActiveScene().name);
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================