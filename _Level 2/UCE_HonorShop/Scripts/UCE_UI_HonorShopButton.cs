using UnityEngine;

// =======================================================================================
// UCE UI HONOR SHOP BUTTON
// =======================================================================================
public partial class UCE_UI_HonorShopButton : MonoBehaviour
{
    public GameObject honorShopCurrencyPanel;

    // -----------------------------------------------------------------------------------
    // ShowHonorShopCurrencyPanel
    // -----------------------------------------------------------------------------------
    public void ShowHonorShopCurrencyPanel()
    {
        if (honorShopCurrencyPanel.activeInHierarchy)
            honorShopCurrencyPanel.SetActive(false);
        else
            honorShopCurrencyPanel.SetActive(true);
    }

    // -----------------------------------------------------------------------------------
}