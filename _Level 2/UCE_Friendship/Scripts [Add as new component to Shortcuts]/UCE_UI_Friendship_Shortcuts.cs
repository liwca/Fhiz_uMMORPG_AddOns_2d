using UnityEngine;
using UnityEngine.UI;

// =======================================================================================
// SHORTCUTS
// =======================================================================================
public partial class UCE_UI_Friendship_Shortcuts : MonoBehaviour
{
    public Button FriendshipButton;
    public GameObject FriendshipPanel;

    // -----------------------------------------------------------------------------------
    // Update
    // -----------------------------------------------------------------------------------
    public void Update()
    {
        FriendshipButton.onClick.SetListener(() =>
        {
            FriendshipPanel.SetActive(!FriendshipPanel.activeSelf);
        });
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================