using UnityEngine;
using UnityEngine.UI;

// =======================================================================================
// SHORTCUTS
// =======================================================================================
public partial class UCE_UI_Leaderboard_Shortcuts : MonoBehaviour
{
    public Button LeaderboardButton;
    public GameObject LeaderboardPanel;

    // -----------------------------------------------------------------------------------
    // Update
    // -----------------------------------------------------------------------------------
    public void Update()
    {
        LeaderboardButton.onClick.SetListener(() =>
        {
            LeaderboardPanel.SetActive(!LeaderboardPanel.activeSelf);
        });
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================