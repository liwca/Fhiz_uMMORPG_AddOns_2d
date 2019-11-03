using UnityEngine;
using UnityEngine.UI;

// =======================================================================================
// SHORTCUTS
// =======================================================================================
public partial class UCE_UI_Mail_Shortcuts : MonoBehaviour
{
    public Button mailInboxButton;
    public Image mailButtonImage;
    public GameObject mailInboxPanel;

    public Sprite mailRead;
    public Sprite mailUnread;

    // -----------------------------------------------------------------------------------
    // Update
    // -----------------------------------------------------------------------------------
    public void Update()
    {
        mailInboxButton.onClick.SetListener(() =>
        {
            mailInboxPanel.SetActive(!mailInboxPanel.activeSelf);
        });

        Player player = Player.localPlayer;
        if (player == null) return;

        if (player.UnreadMailCount() > 0)
            mailButtonImage.sprite = mailUnread;
        else
            mailButtonImage.sprite = mailRead;
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================