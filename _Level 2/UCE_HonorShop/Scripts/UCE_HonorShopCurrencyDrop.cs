using System;

// =======================================================================================
// UCE_HonorShopCurrencyDrop
// =======================================================================================
[Serializable]
public partial struct UCE_HonorShopCurrencyDrop
{
    public UCE_Tmpl_HonorCurrency honorCurrency;
    public long amountMin;
    public long amountMax;

    public long amount
    {
        get
        {
            return (long)UnityEngine.Random.Range(amountMin, amountMax);
        }
    }
}

// =======================================================================================