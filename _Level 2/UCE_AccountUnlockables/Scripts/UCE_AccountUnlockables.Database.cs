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

using Mono.Data.Sqlite; 						// copied from Unity/Mono/lib/mono/2.0 to Plugins

#endif

// =======================================================================================
// DATABASE (SQLite / mySQL Hybrid)
// =======================================================================================
public partial class Database
{
    // -----------------------------------------------------------------------------------
    // Connect_UCE_AccountUnlockables
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    public void Connect_UCE_AccountUnlockables()
    {
#if _MYSQL
		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS account_unlockables (
 			account VARCHAR(32) NOT NULL,
 			unlockable VARCHAR(32) NOT NULL
 		)");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS account_unlockables (
 			account TEXT NOT NULL,
 			unlockable TEXT NOT NULL
 		)");
#endif
    }

    // -----------------------------------------------------------------------------------
    // UCE_GetAccountUnlockables
    // -----------------------------------------------------------------------------------
    public List<string> UCE_GetAccountUnlockables(string accountName)
    {
        List<string> unlockables = new List<string>();

#if _MYSQL
		var table = ExecuteReaderMySql("SELECT unlockable FROM account_unlockables WHERE `account`=@account", new MySqlParameter("@account", accountName));
#elif _SQLITE
        var table = ExecuteReader("SELECT unlockable FROM account_unlockables WHERE `account`=@account", new SqliteParameter("@account", accountName));
#endif

        foreach (var row in table)
            unlockables.Add((string)row[0]);

        return unlockables;
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_UCE_AccountUnlockables
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    public void CharacterLoad_UCE_AccountUnlockables(Player player)
    {
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT unlockable FROM account_unlockables WHERE `account`=@account", new MySqlParameter("@account", player.account));
#elif _SQLITE
        var table = ExecuteReader("SELECT unlockable FROM account_unlockables WHERE `account`=@account", new SqliteParameter("@account", player.account));
#endif

        foreach (var row in table)
            player.UCE_accountUnlockables.Add((string)row[0]);
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_AccountUnlockables
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    public void CharacterSave_UCE_AccountUnlockables(Player player)
    {
#if _MYSQL
		ExecuteNonQueryMySql("DELETE FROM account_unlockables WHERE `account`=@account", new MySqlParameter("@account", player.account));
		for (int i = 0; i < player.UCE_accountUnlockables.Count; ++i) {
			ExecuteNonQueryMySql("INSERT INTO account_unlockables VALUES (@account, @classname)",
 				new MySqlParameter("@account", player.account),
 				new MySqlParameter("@unlockable", player.UCE_accountUnlockables[i]));
 		}
#elif _SQLITE
        ExecuteNonQuery("DELETE FROM account_unlockables WHERE `account`=@account", new SqliteParameter("@account", player.account));
        for (int i = 0; i < player.UCE_accountUnlockables.Count; ++i)
        {
            ExecuteNonQuery("INSERT INTO account_unlockables VALUES (@account, @classname)",
                 new SqliteParameter("@account", player.account),
                 new SqliteParameter("@unlockable", player.UCE_accountUnlockables[i]));
        }
#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================