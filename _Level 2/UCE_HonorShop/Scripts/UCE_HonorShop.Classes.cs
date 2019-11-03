using System;

// =======================================================================================
// UCE_HonorShopCategory
// =======================================================================================
[Serializable]
public partial struct UCE_HonorShopCategory
{
    public string categoryName;
    public UCE_Tmpl_HonorCurrency honorCurrency;
    public ScriptableItem[] items;
}

// =======================================================================================