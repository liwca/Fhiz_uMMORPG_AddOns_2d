// =======================================================================================
// Created and maintained by iMMO
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;
using UnityEngine.UI;

// =======================================================================================
// UCE REMEMBER ME
// =======================================================================================
public partial class UILogin
{
    public Toggle remember;

    // -----------------------------------------------------------------------------------
    // Awake
    // Set our listener to save our account name, grab our account name if remember is checked.
    // -----------------------------------------------------------------------------------
    private void Awake()
    {
        if (remember && remember.isOn && PlayerPrefs.HasKey("Account"))
            accountInput.text = PlayerPrefs.GetString("Account");
    }

    // -----------------------------------------------------------------------------------
    // Save Account
    // Save our account name when login is clicked.
    // -----------------------------------------------------------------------------------
    private void SaveAccount()
    {
        PlayerPrefs.SetString("Account", accountInput.text);
    }
}