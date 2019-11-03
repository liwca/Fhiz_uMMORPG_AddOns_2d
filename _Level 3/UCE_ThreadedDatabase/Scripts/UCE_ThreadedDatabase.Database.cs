// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using System.Collections.Generic;

#if _MYSQL

using MySql.Data;								// From MySql.Data.dll in Plugins folder
using MySql.Data.MySqlClient;                   // From MySql.Data.dll in Plugins folder

#elif _SQLITE
#endif

// =======================================================================================
// DATABASE (SQLite / mySQL Hybrid)
// =======================================================================================
public partial class Database
{
    // -----------------------------------------------------------------------------------
    // CharacterSaveMany
    // -----------------------------------------------------------------------------------
    public void CharacterSaveMany(IEnumerable<Player> players, bool online = true)
    {
#if _MYSQL
     		UCE_LoomManager.Loom.QueueOnMainThread(() =>
    			{
    				CharacterSaveMany_mySQL(players, online);
    			});
#elif _SQLITE
        UCE_LoomManager.Loom.QueueOnMainThread(() =>
            {
                CharacterSaveMany_SQLite(players, online);
            });
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterSaveMany_SQLite
    // -----------------------------------------------------------------------------------
#if _SQLITE

    [DevExtMethods("CharacterSaveMany")]
    public void CharacterSaveMany_SQLite(IEnumerable<Player> players, bool online = true)
    {
        foreach (Player player in players)
        {
            if (player != null)
                CharacterSave(player, online, false);
        }
    }

#endif

    // -----------------------------------------------------------------------------------
    // CharacterSaveMany_mySQL
    // -----------------------------------------------------------------------------------
#if _MYSQL
    [DevExtMethods("CharacterSaveMany")]
    public  void CharacterSaveMany_mySQL(IEnumerable<Player> players, bool online = true)
    {
        Transaction(command =>
        {
            foreach (Player player in players) {
            	if (player != null)
                	CharacterSave(player, online, command);
            }
        });
    }

#endif

    // -----------------------------------------------------------------------------------
}

// =======================================================================================