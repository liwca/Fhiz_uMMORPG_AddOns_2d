// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using System;

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
    protected long accountCount = -1;

    // -----------------------------------------------------------------------------------
    // Connect_UCE_Administration
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_Administration()
    {
#if _MYSQL
		ExecuteNonQueryMySql(@"
                        CREATE TABLE IF NOT EXISTS account_admin (
					    `account` VARCHAR(32) NOT NULL,
                        admin INTEGER(4) NOT NULL DEFAULT 0,
                            PRIMARY KEY(`account`)
                        ) CHARACTER SET=utf8mb4");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS 'account_admin' (
						'account' TEXT NOT NULL PRIMARY KEY,
						'admin' INTEGER NOT NULL DEFAULT 0)");
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_UCE_Administration
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    private void CharacterLoad_UCE_Administration(Player player)
    {
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT admin FROM account_admin WHERE `account`=@account", new MySqlParameter("@account", player.account));
		if (table.Count == 1) {
            var row = table[0];
            player.UCE_adminLevel = (int)(row[0]);
        }
#elif _SQLITE
        var table = ExecuteReader("SELECT admin FROM account_admin WHERE account=@account", new SqliteParameter("@account", player.account));
        if (table.Count == 1)
        {
            var row = table[0];
            player.UCE_adminLevel = Convert.ToInt32(row[0]);
        }
#endif
    }

    // -----------------------------------------------------------------------------------
    // IsBannedAccount
    // -----------------------------------------------------------------------------------
    public bool IsBannedAccount(string account, string password)
    {
        if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(password)) return false;
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT password, banned FROM accounts WHERE name=@name", new MySqlParameter("@name", account));
#elif _SQLITE
        var table = ExecuteReader("SELECT password, banned FROM accounts WHERE name=@name", new SqliteParameter("@name", account));
#endif
        if (table.Count == 1)
        {
            // account exists. check password and ban status.
            var row = table[0];
            return (long)row[1] == 1;
        }
        return false;
    }

    // -----------------------------------------------------------------------------------
    // GetAccountName
    // -----------------------------------------------------------------------------------
    public string GetAccountName(string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName)) return "";
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT account FROM characters WHERE name=@name", new MySqlParameter("@name", playerName));
#elif _SQLITE
        var table = ExecuteReader("SELECT account FROM characters WHERE name=@name", new SqliteParameter("@name", playerName));
#endif
        if (table.Count == 1)
        {
            var row = table[0];
            return (string)row[0];
        }
        return "";
    }

    // -----------------------------------------------------------------------------------
    // GetAccountCount
    // -----------------------------------------------------------------------------------
    public long GetAccountCount()
    {
#if _MYSQL
		return (long)ExecuteScalarMySql("SELECT count(*) FROM accounts");
#elif _SQLITE
        return (long)ExecuteScalar("SELECT count(*) FROM accounts");
#endif
    }

    // -----------------------------------------------------------------------------------
    // BanAccount
    // -----------------------------------------------------------------------------------
    public void BanAccount(string account)
    {
        if (string.IsNullOrWhiteSpace(account)) return;
#if _MYSQL
		ExecuteNonQueryMySql("UPDATE accounts SET banned = '1' WHERE name =@name", new MySqlParameter("@name", account));
#elif _SQLITE
        ExecuteNonQuery("UPDATE accounts SET banned = '1' WHERE name =@name", new SqliteParameter("@name", account));
#endif
    }

    // -----------------------------------------------------------------------------------
    // UnbanAccount
    // -----------------------------------------------------------------------------------
    public void UnbanAccount(string account)
    {
        if (string.IsNullOrWhiteSpace(account)) return;
#if _MYSQL
		ExecuteNonQueryMySql("UPDATE accounts SET banned = '0' WHERE name =@name", new MySqlParameter("@name", account));
#elif _SQLITE
        ExecuteNonQuery("UPDATE accounts SET banned = '0' WHERE name =@name", new SqliteParameter("@name", account));
#endif
    }

    // -----------------------------------------------------------------------------------
    // SetAdminAccount
    // -----------------------------------------------------------------------------------
    public void SetAdminAccount(string accountName, int adminLevel)
    {
        if (string.IsNullOrWhiteSpace(accountName)) return;
#if _MYSQL
		ExecuteNonQueryMySql("REPLACE account_admin VALUES (@account,@admin)",
        	new MySqlParameter("@account", accountName),
        	new MySqlParameter("@admin", adminLevel));
#elif _SQLITE
        ExecuteNonQuery("INSERT OR REPLACE INTO account_admin VALUES (@account, @admin)",
            new SqliteParameter("@account", accountName),
            new SqliteParameter("@admin", adminLevel));
#endif
    }

    // -----------------------------------------------------------------------------------
    // SetCharacterDeleted
    // -----------------------------------------------------------------------------------
    public void SetCharacterDeleted(string playerName, bool deleted)
    {
        if (string.IsNullOrWhiteSpace(playerName)) return;
        int del = (deleted == true ? 1 : 0);
#if _MYSQL
		ExecuteNonQueryMySql("UPDATE accounts SET deleted = '@deleted' WHERE name =@name",
        	new MySqlParameter("@deleted", del),
        	new MySqlParameter("@name", playerName));
#elif _SQLITE
        ExecuteNonQuery("UPDATE accounts SET deleted = '@deleted' WHERE name =@name",
            new SqliteParameter("@deleted", del),
            new SqliteParameter("@name", playerName));
#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================