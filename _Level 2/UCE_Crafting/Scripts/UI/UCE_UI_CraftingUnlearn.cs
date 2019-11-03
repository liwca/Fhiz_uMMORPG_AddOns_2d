// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;
using UnityEngine.UI;

#if _iMMOCRAFTING

// =======================================================================================
// UCE UI CRAFTING UNLEARN
// =======================================================================================
public partial class UCE_UI_CraftingUnlearn : MonoBehaviour
{
    public GameObject panel;
    public GameObject parentPanel;
    public Text text;

    public string unlearnText = "Do you want to unlearn: ";

    [HideInInspector] public UCE_Tmpl_Recipe recipe;

    // -----------------------------------------------------------------------------------
    // Show
    // -----------------------------------------------------------------------------------
    public void Show(UCE_Tmpl_Recipe newRecipe)
    {
        recipe = newRecipe;
        text.text = unlearnText + recipe.name;
        panel.SetActive(true);
    }

    // -----------------------------------------------------------------------------------
    // onClickUnlearn
    // -----------------------------------------------------------------------------------
    public void onClickUnlearn()
    {
        Player player = Player.localPlayer;
        if (!player) return;

        player.Cmd_UCE_Crafting_unlearnRecipe(recipe.name);

        panel.SetActive(false);
        parentPanel.SetActive(false);
    }

    // -----------------------------------------------------------------------------------
    // onClickCancel
    // -----------------------------------------------------------------------------------
    public void onClickCancel()
    {
        panel.SetActive(false);
    }

    // -----------------------------------------------------------------------------------
}

#endif