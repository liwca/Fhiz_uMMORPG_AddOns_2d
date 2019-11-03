// =======================================================================================
// Created and maintained by iMMO
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
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
// NETWORK MANAGER MMO
// =======================================================================================
public partial class NetworkManagerMMO
{
    public UCE_Tmpl_DatabaseCleaner DatabaseCleaner;

    // -----------------------------------------------------------------------------------
    // OnStartServer_UCE_DatabaseCleaner
    // -----------------------------------------------------------------------------------
    [DevExtMethods("OnStartServer")]
    public void OnStartServer_UCE_DatabaseCleaner()
    {
        if (DatabaseCleaner && DatabaseCleaner.isActive)
        {
            var i = 0;

            // ---------- Prune outdated accounts
            if (DatabaseCleaner.PruneInactiveAfterDays > 0 || DatabaseCleaner.PruneBannedAfterDays > 0)
            {
#if _MYSQL
					var table = Database.singleton.ExecuteReaderMySql("SELECT account, lastOnline FROM account_lastonline");
#elif _SQLITE
                var table = Database.singleton.ExecuteReader("SELECT account, lastOnline FROM account_lastonline");
#endif

                foreach (var row in table)
                {
                    var accountName = (string)row[0];
                    var lastOnline = (string)row[1];

                    if (!string.IsNullOrWhiteSpace(accountName))
                    {
                        DateTime time = DateTime.Parse(lastOnline);
                        var HoursPassed = (DateTime.UtcNow - time).TotalDays;

                        // ---------- Prune outdated accounts
                        if (DatabaseCleaner.PruneInactiveAfterDays > 0 && HoursPassed > DatabaseCleaner.PruneInactiveAfterDays)
                        {
                            UCE_DatabaseCleanup(accountName);
                            i++;
                        }

                        // ---------- Prune banned accounts
                        if (DatabaseCleaner.PruneBannedAfterDays > 0 && HoursPassed > DatabaseCleaner.PruneBannedAfterDays)
                        {
#if _MYSQL
								bool banned = (bool)Database.singleton.ExecuteScalarMySql("SELECT banned FROM accounts WHERE name=@name", new MySqlParameter("@name", accountName));
								if (banned) {
									UCE_DatabaseCleanup(accountName);
									i++;
								}
#elif _SQLITE
                            var banned = (long)Database.singleton.ExecuteScalar("SELECT banned FROM accounts WHERE name=@name", new SqliteParameter("@name", accountName));
                            if (banned == 1)
                            {
                                UCE_DatabaseCleanup(accountName);
                                i++;
                            }
#endif
                        }
                    }
                }
            }

            // ---------- Prune empty accounts (no characters)
            if (DatabaseCleaner.PruneEmptyAccounts)
            {
#if _MYSQL
					var table2 = Database.singleton.ExecuteReaderMySql("SELECT name FROM accounts");
#elif _SQLITE
                var table2 = Database.singleton.ExecuteReader("SELECT name FROM accounts");
#endif

                foreach (var row in table2)
                {
                    var accountChars = Database.singleton.CharactersForAccount((string)row[0]);
                    if (accountChars.Count < 1)
                    {
                        UCE_DatabaseCleanup((string)row[0]);
                        i++;
                    }
                }
            }

            Debug.Log("DatabaseCleaner checking accounts ...pruned [" + i + "] account(s)");
        }
        else
        {
            Debug.LogWarning("DatabaseCleaner: Either inactive or ScriptableObject not found!");
        }
    }

    // -----------------------------------------------------------------------------------
    // OnServerDisconnect_UCE_DatabaseCleaner
    // -----------------------------------------------------------------------------------
    [DevExtMethods("OnServerDisconnect")]
    private void OnServerDisconnect_UCE_DatabaseCleaner(NetworkConnection conn)
    {
        if (conn.playerController != null)
        {
            var accountName = conn.playerController.gameObject.GetComponent<Player>().account;
            Database.singleton.UCE_DatabaseCleanerAccountLastOnline(accountName);
        }
    }

    // -----------------------------------------------------------------------------------
    // UCE_DatabaseCleanup
    // -----------------------------------------------------------------------------------
    public void UCE_DatabaseCleanup(string accountName)
    {
        var accountChars = Database.singleton.CharactersForAccount(accountName);

#if _MYSQL

		foreach (string accountChar in accountChars) {
			if (0 < (long)Database.singleton.ExecuteScalarMySql("SELECT count(*) FROM character_buffs WHERE `character`=@name", new MySqlParameter("@name", accountChar)))
				Database.singleton.ExecuteNonQueryMySql("DELETE FROM character_buffs WHERE `character`=@name", new MySqlParameter("@name", accountChar));

			if (0 < (long)Database.singleton.ExecuteScalarMySql("SELECT count(*) FROM character_inventory WHERE `character`=@name", new MySqlParameter("@name", accountChar)))
				Database.singleton.ExecuteNonQueryMySql("DELETE FROM character_inventory WHERE `character`=@name", new MySqlParameter("@name", accountChar));

			if (0 < (long)Database.singleton.ExecuteScalarMySql("SELECT count(*) FROM character_equipment WHERE `character`=@name", new MySqlParameter("@name", accountChar)))
				Database.singleton.ExecuteNonQueryMySql("DELETE FROM character_equipment WHERE `character`=@name", new MySqlParameter("@name", accountChar));

			if (0 < (long)Database.singleton.ExecuteScalarMySql("SELECT count(*) FROM character_skills WHERE `character`=@name", new MySqlParameter("@name", accountChar)))
				Database.singleton.ExecuteNonQueryMySql("DELETE FROM character_skills WHERE `character`=@name", new MySqlParameter("@name", accountChar));

			if (0 < (long)Database.singleton.ExecuteScalarMySql("SELECT count(*) FROM character_quests WHERE `character`=@name", new MySqlParameter("@name", accountChar)))
				Database.singleton.ExecuteNonQueryMySql("DELETE FROM character_quests WHERE `character`=@name", new MySqlParameter("@name", accountChar));

			if (0 < (long)Database.singleton.ExecuteScalarMySql("SELECT count(*) FROM character_orders WHERE `character`=@name", new MySqlParameter("@name", accountChar)))
				Database.singleton.ExecuteNonQueryMySql("DELETE FROM character_orders WHERE `character`=@name", new MySqlParameter("@name", accountChar));

			foreach (string charTable in DatabaseCleaner.characterTables) {
				if (charTable != "") {
					if (0 < (long)Database.singleton.ExecuteScalarMySql("SELECT count(*) FROM "+charTable))
						Database.singleton.ExecuteNonQueryMySql("DELETE FROM "+charTable+" WHERE `character`=@name", new MySqlParameter("@name", accountChar));
				}
			}

			foreach (string accountTable in DatabaseCleaner.accountTables) {
				if (accountTable != "") {
					if (0 < (long)Database.singleton.ExecuteScalarMySql("SELECT count(*) FROM "+accountTable))
						Database.singleton.ExecuteNonQueryMySql("DELETE FROM "+accountTable+" WHERE account=@name", new MySqlParameter("@name", accountName));
				}
			}
		}

		if (0 < (long)Database.singleton.ExecuteScalarMySql("SELECT count(*) FROM characters"))
			Database.singleton.ExecuteNonQueryMySql("DELETE FROM characters WHERE account=@name", new MySqlParameter("@name", accountName));

		if (0 < (long)Database.singleton.ExecuteScalarMySql("SELECT count(*) FROM accounts"))
			Database.singleton.ExecuteNonQueryMySql("DELETE FROM accounts WHERE name=@name", new MySqlParameter("@name", accountName));

#elif _SQLITE

        foreach (string accountChar in accountChars)
        {
            Database.singleton.ExecuteNonQuery("BEGIN");
            Database.singleton.ExecuteNonQuery("DELETE FROM character_buffs WHERE character=@name", new SqliteParameter("@name", accountChar));
            Database.singleton.ExecuteNonQuery("DELETE FROM character_inventory WHERE character=@name", new SqliteParameter("@name", accountChar));
            Database.singleton.ExecuteNonQuery("DELETE FROM character_equipment WHERE character=@name", new SqliteParameter("@name", accountChar));
            Database.singleton.ExecuteNonQuery("DELETE FROM character_skills WHERE character=@name", new SqliteParameter("@name", accountChar));
            Database.singleton.ExecuteNonQuery("DELETE FROM character_quests WHERE character=@name", new SqliteParameter("@name", accountChar));
            Database.singleton.ExecuteNonQuery("DELETE FROM character_orders WHERE character=@name", new SqliteParameter("@name", accountChar));

            foreach (string charTable in DatabaseCleaner.characterTables)
            {
                if (charTable != "")
                {
                    if (0 < (long)Database.singleton.ExecuteScalar("SELECT count(*) FROM sqlite_master WHERE type='table' AND name='" + charTable + "'"))
                        Database.singleton.ExecuteNonQuery("DELETE FROM " + charTable + " WHERE character=@name", new SqliteParameter("@name", accountChar));
                }
            }

            foreach (string accountTable in DatabaseCleaner.accountTables)
            {
                if (accountTable != "")
                {
                    if (0 < (long)Database.singleton.ExecuteScalar("SELECT count(*) FROM sqlite_master WHERE type='table' AND name='" + accountTable + "'"))
                        Database.singleton.ExecuteNonQuery("DELETE FROM " + accountTable + " WHERE account=@name", new SqliteParameter("@name", accountName));
                }
            }

            Database.singleton.ExecuteNonQuery("END");
        }

        Database.singleton.ExecuteNonQuery("DELETE FROM characters WHERE account=@name", new SqliteParameter("@name", accountName));
        Database.singleton.ExecuteNonQuery("DELETE FROM accounts WHERE name=@name", new SqliteParameter("@name", accountName));
#endif

        Debug.Log("DatabaseCleaner deleted characters of account [" + accountName + "] and all associated tables.");
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================