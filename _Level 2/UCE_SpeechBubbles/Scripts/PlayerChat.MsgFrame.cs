// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;

// =======================================================================================
//
// =======================================================================================
public partial class PlayerChat
{
    private MsgFrame msgFrame;
    private bool fakeMsgsFinished;

    // -----------------------------------------------------------------------------------
    // Awake
    // -----------------------------------------------------------------------------------
    public void Awake()
    {
        msgFrame = Instantiate(Resources.Load<GameObject>("MsgFrame"), this.transform).GetComponent<MsgFrame>();
    }

    // -----------------------------------------------------------------------------------
    // OnStartLocalPlayer_MsgFrame
    // -----------------------------------------------------------------------------------
    public void OnStartLocalPlayer_MsgFrame()
    {
        fakeMsgsFinished = true;
    }

    // -----------------------------------------------------------------------------------
    // OnSubmit_MsgFrame
    // -----------------------------------------------------------------------------------
    [DevExtMethods("OnSubmit")]
    public void OnSubmit_MsgFrame(string text)
    {
        if (!fakeMsgsFinished) return;

        //local chat
        if (!text.StartsWith("/"))
        {
            // find the space that separates the name and the message
            int i = text.IndexOf(": ");
            if (i >= 0)
            {
                text = text.Substring(i + 1);
            }

            msgFrame.ShowMessage(text);
        }
    }

    // -----------------------------------------------------------------------------------
    // RpcMsgLocal_MsgFrame
    // -----------------------------------------------------------------------------------
    public void RpcMsgLocal_MsgFrame(string sender, string msg)
    {
        Player p = Player.onlinePlayers[sender];

        if (p)
            p.GetComponent<PlayerChat>().msgFrame.ShowMessage(msg);
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================