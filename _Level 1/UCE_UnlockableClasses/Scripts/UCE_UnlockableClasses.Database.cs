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
    // Connect_UCE_UnlockableClasses
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    public void Connect_UCE_UnlockableClasses()
    {
#if _MYSQL
		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS account_unlockedclasses (
 			account VARCHAR(32) NOT NULL,
 			classname VARCHAR(32) NOT NULL
 		)");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS account_unlockedclasses (
 			account TEXT NOT NULL,
 			classname TEXT NOT NULL
 		)");
#endif
    }

    // -----------------------------------------------------------------------------------
    // UCE_GetUnlockedClasses
    // -----------------------------------------------------------------------------------
    public List<string> UCE_GetUnlockedClasses(string accountName)
    {
        List<string> unlockedClasses = new List<string>();
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT classname FROM account_unlockedclasses WHERE `account`=@account", new MySqlParameter("@account", accountName));
#elif _SQLITE
        var table = ExecuteReader("SELECT classname FROM account_unlockedclasses WHERE `account`=@account", new SqliteParameter("@account", accountName));
#endif
        foreach (var row in table)
        {
            unlockedClasses.Add((string)row[0]);
        }
        return unlockedClasses;
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_UCE_UnlockableClasses
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    public void CharacterLoad_UCE_UnlockableClasses(Player player)
    {
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT classname FROM account_unlockedclasses WHERE `account`=@account", new MySqlParameter("@account", player.account));
#elif _SQLITE
        var table = ExecuteReader("SELECT classname FROM account_unlockedclasses WHERE `account`=@account", new SqliteParameter("@account", player.account));
#endif
        foreach (var row in table)
        {
            player.UCE_unlockedClasses.Add((string)row[0]);
        }
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_UnlockableClasses
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    public void CharacterSave_UCE_UnlockableClasses(Player player)
    {
#if _MYSQL
		ExecuteNonQueryMySql("DELETE FROM account_unlockedclasses WHERE `account`=@account", new MySqlParameter("@account", player.account));
		for (int i = 0; i < player.UCE_unlockedClasses.Count; ++i) {
			ExecuteNonQueryMySql("INSERT INTO account_unlockedclasses VALUES (@account, @classname)",
 				new MySqlParameter("@account", player.account),
 				new MySqlParameter("@classname", player.UCE_unlockedClasses[i]));
 		}
#elif _SQLITE
        ExecuteNonQuery("DELETE FROM account_unlockedclasses WHERE `account`=@account", new SqliteParameter("@account", player.account));
        for (int i = 0; i < player.UCE_unlockedClasses.Count; ++i)
        {
            ExecuteNonQuery("INSERT INTO account_unlockedclasses VALUES (@account, @classname)",
                 new SqliteParameter("@account", player.account),
                 new SqliteParameter("@classname", player.UCE_unlockedClasses[i]));
        }
#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================