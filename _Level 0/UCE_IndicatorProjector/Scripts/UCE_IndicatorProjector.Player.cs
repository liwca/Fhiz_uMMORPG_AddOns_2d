using UnityEngine;
using Mirror;
using System;
using System.Linq;
using System.Collections;

// =======================================================================================
// PLAYER
// =======================================================================================
public partial class Player
{
    // -----------------------------------------------------------------------------------
    // OnSelect_UCE_IndicatorProjector
    // @Client
    // -----------------------------------------------------------------------------------
    [Client]
    private void OnSelect_UCE_IndicatorProjector(Entity entity)
    {
        if (entity != null &&
            entity.GetComponent<UCE_IndicatorProjector>() != null &&
            Utils.ClosestDistance(collider, entity.collider) <= interactionRange &&
            state == "IDLE"
            )
        {
            UCE_IndicatorProjector ip = entity.GetComponent<UCE_IndicatorProjector>();

            ip.Show();
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================