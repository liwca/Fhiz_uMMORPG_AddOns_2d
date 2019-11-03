using UnityEngine;

// =======================================================================================
// UseItem
// =======================================================================================
public class UseItem : MonoBehaviour
{
    public static void Use(UCE_Tmpl_PayPalProduct product)
    {
        Player player = Player.localPlayer;
        if (!player) return;

        if (product != null)
        {
            player.Cmd_UCE_PayPal_PurchaseCoins(product.name.GetStableHashCode());
        }
    }
}