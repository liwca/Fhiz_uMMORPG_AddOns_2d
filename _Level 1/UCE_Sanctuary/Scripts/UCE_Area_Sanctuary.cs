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
// SIMPLE SANCTUARY
// =======================================================================================
[RequireComponent(typeof(Collider2D))]
public class UCE_Area_Sanctuary : NetworkBehaviour
{
    [Header("-=-=- UCE SANCTUARY -=-=-")]
    [Tooltip("One click deactivation")]
    public bool isActive = true;

    [Tooltip("[Optional] Seconds spent offline to recover 1 Health")]
    public int SecondsPerHealth = 60;

    [Tooltip("[Optional] Seconds spent offline to recover 1 Mana")]
    public int SecondsPerMana = 60;

    [Tooltip("[Optional] Seconds spent offline to gain 1 Experience")]
    public int SecondsPerExp = 60;

    [Tooltip("[Optional] Seconds spent offline to gain 1 Skill Experience")]
    public int SecondsPerSkillExp = 60;

    [Tooltip("[Optional] Seconds spent offline to gain 1 Gold")]
    public int SecondsPerGold = 60;

    [Tooltip("[Optional] Seconds spent offline to gain 1 Coin")]
    public int SecondsPerCoins = 60;

    [Tooltip("[Optional] Max. Exp gain cap offline per session (set 0 to disable)")]
    public int MaxExp = 5;

    [Tooltip("[Optional] Max. Skill Exp gain cap offline per session (set 0 to disable)")]
    public int MaxSkillExp = 5;

    [Tooltip("[Optional] Max. Gold gain cap offline per session (set 0 to disable)")]
    public int MaxGold = 5;

    [Tooltip("[Optional] Max. Coins gain cap offline per session (set 0 to disable)")]
    public int MaxCoins = 5;

#if _iMMOHONORSHOP
    public UCE_Sanctuary_HonorCurrency[] honorCurrencies;
#endif

    [Header("-=-=- Messages -=-=-")]
    public string messageOnEnter = "You just entered a Sanctuary!";
    public string MSG_HEALTH = "[Sanctuary] Recovered health while being offline: ";
    public string MSG_MANA = "[Sanctuary] Recovered mana while being offline: ";
    public string MSG_STAMINA = "[Sanctuary] Recovered stamina while being offline: ";
    public string MSG_EXP = "[Sanctuary] Gained experience while being offline: ";
    public string MSG_SKILLEXP = "[Sanctuary] Gained skill experience while being offline: ";
    public string MSG_GOLD = "[Sanctuary] Earned gold while being offline: ";
    public string MSG_COINS = "[Sanctuary] Earned Coins while being offline: ";
#if _iMMOHONORSHOP
    public string MSG_CURRENCY = "[Sanctuary] Earned while being offline: ";
#endif

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
    // -----------------------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D co)
    {
        Player player = co.GetComponentInParent<Player>();
        if (player && isActive)
        {
            player.UCE_RecoverOverTime(this);
#if _iMMOTOOLS
            player.UCE_ShowPopup(messageOnEnter);
#endif
        }
    }
}

// -----------------------------------------------------------------------------------

// =======================================================================================