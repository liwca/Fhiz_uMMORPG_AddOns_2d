// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;

#if _iMMONPCRESTRICTIONS

// ===================================================================================
// NPC RESTRICTIONS
// ===================================================================================
public partial class Npc
{
    [Header("[NPC RESTRICTIONS]")]
    public UCE_Requirements npcRestrictions;

    protected UCE_UI_NpcAccessRequirement instance;

    // -----------------------------------------------------------------------------------
    // UCE_ValidateNpcRestrictions
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Awake")]
    public void Awake_UCE_NpcRestrictions()
    {
        if (instance == null)
            instance = FindObjectOfType<UCE_UI_NpcAccessRequirement>();
    }

    // -----------------------------------------------------------------------------------
    // UCE_ValidateNpcRestrictions
    // -----------------------------------------------------------------------------------
    public bool UCE_ValidateNpcRestrictions(Player player)
    {
        bool bValid = npcRestrictions.checkRequirements(player);
        if (!bValid)
            instance.Show(this);
        return bValid;
    }

    // -----------------------------------------------------------------------------------
    // UCE_ValidateNpcRestrictions
    // -----------------------------------------------------------------------------------
    public void ConfirmAccess()
    {
        UINpcDialogue.singleton.Show();
    }

    // -----------------------------------------------------------------------------------
}

#endif