// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;
using UnityEngine.UI;

// ===================================================================================
// UCE UI FRIENDLIST
// ===================================================================================
public class UCE_UI_FriendList : MonoBehaviour
{
    public KeyCode hotKey = KeyCode.F;
    public GameObject panel;
    public UCE_UI_FriendRow friendRow;
    public Transform content;
    public InputField InputFieldAddFriend;
    public Button ButtonAddFriend;

    // -----------------------------------------------------------------------------------
    // Start
    // -----------------------------------------------------------------------------------
    private void Start()
    {
        ButtonAddFriend.onClick.SetListener(() => AddFriendButtonEvent());
    }

    // -----------------------------------------------------------------------------------
    // Update
    // -----------------------------------------------------------------------------------
    private void Update()
    {
        Player player = Player.localPlayer;
        if (!player) return;

        if (Input.GetKeyDown(hotKey) && !UIUtils.AnyInputActive())
            panel.SetActive(!panel.activeSelf);

        Prepare();
    }

    // -----------------------------------------------------------------------------------
    // AddFriendButtonEvent
    // -----------------------------------------------------------------------------------
    public void AddFriendButtonEvent()
    {
        Player player = Player.localPlayer;
        if (!player) return;

        if (!string.IsNullOrWhiteSpace(InputFieldAddFriend.text))
            player.Cmd_UCE_AddFriend(InputFieldAddFriend.text);
    }

    // -----------------------------------------------------------------------------------
    // Prepare
    // -----------------------------------------------------------------------------------
    public void Prepare()
    {
        Player player = Player.localPlayer;
        if (!player) return;

        UIUtils.BalancePrefabs(friendRow.gameObject, player.UCE_Friends.Count, content);

        for (int i = 0; i < player.UCE_Friends.Count; i++)
        {
            UCE_UI_FriendRow friendRow = content.GetChild(i).GetComponent<UCE_UI_FriendRow>();
            friendRow.SetData(i);
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================