// =======================================================================================
// Created and maintained by iMMO
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/iMMOban
// =======================================================================================

using UnityEngine;
using Mirror;
using System;
using System.Collections;

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
    // Connect_UCE_DatabaseCleaner
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_DatabaseCleaner()
    {
#if _MYSQL
 		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS account_lastonline (
 			account VARCHAR(32) NOT NULL,
 			lastOnline VARCHAR(64) NOT NULL,
            PRIMARY KEY(`account`)
 		    )");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS account_lastonline (
 			account TEXT NOT NULL,
 			lastOnline TEXT NOT NULL
 		)");
#endif
    }

    // -----------------------------------------------------------------------------------
    // UCE_DatabaseCleanerAccountLastOnline
    // -----------------------------------------------------------------------------------
    public void UCE_DatabaseCleanerAccountLastOnline(string accountName)
    {
        if (string.IsNullOrWhiteSpace(accountName)) return;
#if _MYSQL
 		ExecuteNonQueryMySql("DELETE FROM account_lastonline WHERE account=@name", new MySqlParameter("@name", accountName));
        ExecuteNonQueryMySql("INSERT INTO account_lastonline VALUES (@account, @lastOnline)",
			new MySqlParameter("@lastOnline", DateTime.UtcNow.ToString("s")),
			new MySqlParameter("@account", accountName));
#elif _SQLITE
        ExecuteNonQuery("DELETE FROM account_lastonline WHERE account=@name", new SqliteParameter("@name", accountName));
        ExecuteNonQuery("INSERT INTO account_lastonline VALUES (@account, @lastOnline)",
            new SqliteParameter("@lastOnline", DateTime.UtcNow.ToString("s")),
            new SqliteParameter("@account", accountName));
#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================