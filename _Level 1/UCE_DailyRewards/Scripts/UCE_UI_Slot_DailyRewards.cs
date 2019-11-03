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
// DAILY REWARDS UI SLOT
// ===================================================================================
public partial class UCE_UI_Slot_DailyRewards : MonoBehaviour
{
    public Image slotIcon;
    public Text slotText;

    // -----------------------------------------------------------------------------------
    // AddMessage
    // -----------------------------------------------------------------------------------
    public void AddMessage(string msg, Color color, Sprite icon = null)
    {
        slotText.color = color;
        slotText.text = msg;
        slotIcon.sprite = icon;
    }
}

// =======================================================================================