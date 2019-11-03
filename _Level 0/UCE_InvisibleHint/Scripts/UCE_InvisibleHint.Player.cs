// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;
using Mirror;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

// =======================================================================================
// PLAYER
// =======================================================================================
public partial class Player
{
    protected UCE_UI_InvisibleHint _UCE_UI_InvisibleHint;

    // -----------------------------------------------------------------------------------
    // UCE_InvisibleHint_Show
    // -----------------------------------------------------------------------------------
    public void UCE_InvisibleHint_Show(string message, float hideAfter)
    {
        if (_UCE_UI_InvisibleHint == null)
            _UCE_UI_InvisibleHint = FindObjectOfType<UCE_UI_InvisibleHint>();

        _UCE_UI_InvisibleHint.Show(message);

        if (hideAfter > 0)
            Invoke("UCE_InvisibleHint_Hide", hideAfter);
    }

    // -----------------------------------------------------------------------------------
    // UCE_InvisibleHint_Hide
    // -----------------------------------------------------------------------------------
    public void UCE_InvisibleHint_Hide()
    {
        if (_UCE_UI_InvisibleHint == null)
            _UCE_UI_InvisibleHint = FindObjectOfType<UCE_UI_InvisibleHint>();

        _UCE_UI_InvisibleHint.Hide();
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================