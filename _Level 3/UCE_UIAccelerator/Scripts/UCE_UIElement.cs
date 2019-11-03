// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;

// =======================================================================================
// UCE_UIElement
// =======================================================================================
public abstract class UCE_UIElement : MonoBehaviour
{
    [Header("[-=-=- UCE UI Element -=-=-]")]
    [SerializeField] private bool throttleUpdate = true;
    [SerializeField] [Range(0.01f, 3f)] private float updateInterval = 0.25f;

    protected float fInterval;

    // -----------------------------------------------------------------------------------
    // Start
    // -----------------------------------------------------------------------------------
    private void Update()
    {
        if (!throttleUpdate || (throttleUpdate && Time.time > fInterval))
        {
            UCE_SlowUpdate();
            fInterval = Time.time + updateInterval;
        }
    }

    // -----------------------------------------------------------------------------------
    // UCE_SlowUpdate
    // -----------------------------------------------------------------------------------
    protected virtual void UCE_SlowUpdate() { }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================