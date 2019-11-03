using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

// =======================================================================================
// LogoutTimer
// =======================================================================================
public partial class Player
{
    [Header("[UCE LOGOUT TIMER]")]
    [Range(1, 9999)] public float logoutWarningTime = 30f;
    [Range(1, 9999)] public float logoutKickTime = 60f;

    protected float _logoutTimer = 0;

    // -----------------------------------------------------------------------------------
    // Update
    // -----------------------------------------------------------------------------------
    [ClientCallback]
    [DevExtMethods("Update")]
    private void Update_UCE_LogoutTimer()
    {
        Player player = Player.localPlayer;
        if (!player) return;

        if (player.state == "IDLE")
        {
            _logoutTimer += cacheTimerInterval;
        }
        else
        {
            _logoutTimer = 0;
            UCE_UI_LogoutTimer_Popup.singleton.Hide();
        }

        if (_logoutTimer > logoutKickTime)
            NetworkManagerMMO.Quit();
        else if (_logoutTimer > logoutWarningTime)
            UCE_UI_LogoutTimer_Popup.singleton.Show();
    }

    // -----------------------------------------------------------------------------------
}