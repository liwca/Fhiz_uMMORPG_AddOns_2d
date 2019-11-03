// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;

// =======================================================================================
// SKILL CATEGORY BUTTON
// =======================================================================================
public partial class UCE_UI_SkillCategoryButton : MonoBehaviour
{
    public GameObject panel;
    public string category;

    // -----------------------------------------------------------------------------------
    // OnClick
    // -----------------------------------------------------------------------------------
    public void OnClick()
    {
        UCE_UI_Skills co = panel.GetComponent<UCE_UI_Skills>();

        if (co)
        {
            co.changeCategory(category);
        }
    }

    // -----------------------------------------------------------------------------------
}