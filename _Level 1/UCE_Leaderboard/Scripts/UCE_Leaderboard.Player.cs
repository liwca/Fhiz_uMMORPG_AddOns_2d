// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using Mirror;

// ===================================================================================
// Player
// ===================================================================================
public partial class Player
{
    public SyncListUCE_LeaderboardPlayer currentOnlinePlayers = new SyncListUCE_LeaderboardPlayer();

    protected int maxPlayers = 50;

    // -----------------------------------------------------------------------------------
    // Cmd_UCE_AllPlayersOnline
    // @Client -> @Server
    // -----------------------------------------------------------------------------------
    [Command]
    public void Cmd_UCE_AllPlayersOnline()
    {
        // we wrap it in another function, because we want to call it only server-side
        UCE_UpdatePlayersOnline();
    }

    // -----------------------------------------------------------------------------------
    // UCE_UpdatePlayersOnline
    // @Server
    // -----------------------------------------------------------------------------------
    [ServerCallback]
    public void UCE_UpdatePlayersOnline()
    {
        currentOnlinePlayers.Clear();

        int i = 0;

        foreach (Player plyr in onlinePlayers.Values)
        {
            UCE_LeaderboardPlayer ldplyr = new UCE_LeaderboardPlayer(plyr.name, plyr.level, plyr.gold);

            currentOnlinePlayers.Add(ldplyr);

            i++;

            if (i == maxPlayers) break;
        }
    }

    // -----------------------------------------------------------------------------------
}

// ===================================================================================