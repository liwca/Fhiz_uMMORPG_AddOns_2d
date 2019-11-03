// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using Mirror;

// =======================================================================================
// NETWORK MANAGER
// =======================================================================================
public partial class NetworkManagerMMO
{
    // -----------------------------------------------------------------------------------
    // OnServerDisconnect
    // @Server
    // -----------------------------------------------------------------------------------
    [DevExtMethods("OnServerDiconnect")]
    public void OnServerDisconnect_UCE_GuildUCE_warehouse(NetworkConnection conn)
    {
        /*
            if (conn.identity != null)
                Database.singleton.UCE_SaveGuildWarehouse(conn.identity.GetComponent<Player>());
        */
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================