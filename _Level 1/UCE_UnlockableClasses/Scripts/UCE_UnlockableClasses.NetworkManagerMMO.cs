using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// =======================================================================================
// NETWORK MANAGER MMO
// =======================================================================================
public partial class NetworkManagerMMO
{
    [Header("-=-=- UCE DEFAULT UNLOCKED CLASSES -=-=-")]
    public UCE_Tmpl_UnlockableClasses defaultUnlockedClasses;

    [HideInInspector] public List<string> availableClasses = new List<string>();

    // -----------------------------------------------------------------------------------
    // OnClientConnect_UCE_UnlockableClasses
    // -----------------------------------------------------------------------------------
    [DevExtMethods("OnClientConnect")]
    public void OnClientConnect_UCE_UnlockableClasses(NetworkConnection connection)
    {
        NetworkClient.RegisterHandler<ClassesAvailableMsg>(OnClientReceiveClassesAvailable);
    }

    // -----------------------------------------------------------------------------------
    // OnClientReceiveClassesAvailable
    // @Client
    // -----------------------------------------------------------------------------------
    public void OnClientReceiveClassesAvailable(NetworkConnection connection, ClassesAvailableMsg message)
    {
        if (!defaultUnlockedClasses) return;

        availableClasses = new List<string>();

        foreach (Player player in defaultUnlockedClasses.defaultClasses)
        {
            availableClasses.Add(player.name);
        }

        availableClasses.AddRange(message.unlockedClasses);
    }

    // -----------------------------------------------------------------------------------
    // OnServerLogin_UCE_UnlockableClasses
    // @Server
    // -----------------------------------------------------------------------------------
    [DevExtMethods("OnServerLogin")]
    public void OnServerLogin_UCE_UnlockableClasses(NetworkConnection conn, LoginMsg netMsg)
    {
        if (!defaultUnlockedClasses) return;

        List<string> unlockedClasses = Database.singleton.UCE_GetUnlockedClasses(netMsg.account);
        ClassesAvailableMsg message = new ClassesAvailableMsg { unlockedClasses = unlockedClasses.ToArray() };

        conn.Send(message);
    }

    // -----------------------------------------------------------------------------------
    // UCE_HasUnlockedClass
    // -----------------------------------------------------------------------------------
    public bool UCE_HasUnlockedClass(Player player)
    {
        if (player == null || player.name == "") return false;

        return (availableClasses.Any(s => s == player.name));
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================