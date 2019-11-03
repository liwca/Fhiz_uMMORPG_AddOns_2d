using UnityEngine;
using Mirror;
using System;
using System.Linq;
using System.Collections;

// =======================================================================================
// UCE_IndicatorProjector
// =======================================================================================
public partial class UCE_IndicatorProjector : MonoBehaviour
{
    public GameObject indicatorProjector;
    public float hideAfter;

    // -----------------------------------------------------------------------------------
    // Show
    // -----------------------------------------------------------------------------------
    public void Show()
    {
        if (indicatorProjector)
        {
            indicatorProjector.gameObject.SetActive(true);
            Invoke("Hide", hideAfter);
        }
    }

    // -----------------------------------------------------------------------------------
    // Hide
    // -----------------------------------------------------------------------------------
    public void Hide()
    {
        if (indicatorProjector)
            indicatorProjector.gameObject.SetActive(false);
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================