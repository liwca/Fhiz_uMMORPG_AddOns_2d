using Mirror;
using UnityEngine;

public partial class PlayerChat
{
    #region Functions

    //Send messages from UCE_Help to player client chat window only.
    [Client]
    [DevExtMethods("AddMessageClient")]
    private void AddMessageClient_Report(ChatMessage mi)
    {
        UIChat chat = GameObject.Find("Chat").GetComponent<UIChat>();
        chat.AddMessage(mi);          //Sends messages from UCE_Help to the chat window.
    }

    #endregion Functions
}