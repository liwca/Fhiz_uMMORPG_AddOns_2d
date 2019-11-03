using System;
using UnityEngine;

// =======================================================================================
// UCE_Sanctuary_HonorCurrency
// =======================================================================================
#if _iMMOHONORSHOP

[Serializable]
public partial struct UCE_Sanctuary_HonorCurrency
{
    public UCE_Tmpl_HonorCurrency honorCurrency;

    [Tooltip("[Optional] Seconds spent offline to gain 1 Honor Currency unit")]
    public int SecondsPerUnit;

    [Tooltip("[Optional] Max. Honor Currency cap offline per session (set 0 to disable)")]
    public int MaxUnits;
}

#endif
// =======================================================================================