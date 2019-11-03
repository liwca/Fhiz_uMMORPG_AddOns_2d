using System.Linq;

// =======================================================================================
// ITEM
// =======================================================================================
public partial struct Item
{
    // -----------------------------------------------------------------------------------
    // UCE_GetHonorCurrency
    // -----------------------------------------------------------------------------------
    public long UCE_GetHonorCurrency(UCE_Tmpl_HonorCurrency honorCurrency)
    {
        return data.currencyCosts.FirstOrDefault(x => x.honorCurrency.name == honorCurrency.name).amount;
    }
}