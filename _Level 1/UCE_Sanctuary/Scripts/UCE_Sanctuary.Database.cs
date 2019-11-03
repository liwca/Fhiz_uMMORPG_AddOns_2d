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
    // -----------------------------------------------------------------------------------
    // Connect_UCE_Sanctuary
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_Sanctuary()
    {
#if _MYSQL
		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS character_lastonline (
				`character` VARCHAR(32) NOT NULL,
				lastOnline VARCHAR(64) NOT NULL,
                    PRIMARY KEY(`character`)
                ) CHARACTER SET=utf8mb4");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS character_lastonline (
				character TEXT NOT NULL,
				lastOnline TEXT NOT NULL)");
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_UCE_Sanctuary
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    private void CharacterLoad_UCE_Sanctuary(Player player)
    {
#if _MYSQL
		var row = (string)ExecuteScalarMySql("SELECT lastOnline FROM character_lastonline WHERE `character`=@name", new MySqlParameter("@name", player.name));
		if (!string.IsNullOrWhiteSpace(row)) {
			DateTime time 				= DateTime.Parse(row);
			player.UCE_SecondsPassed 	= (DateTime.UtcNow - time).TotalSeconds;
		} else {
			player.UCE_SecondsPassed 	= 0;
		}
#elif _SQLITE
        var row = (string)ExecuteScalar("SELECT lastOnline FROM character_lastonline WHERE `character`=@name", new SqliteParameter("@name", player.name));
        if (!string.IsNullOrWhiteSpace(row))
        {
            DateTime time = DateTime.Parse(row);
            player.UCE_SecondsPassed = (DateTime.UtcNow - time).TotalSeconds;
        }
        else
        {
            player.UCE_SecondsPassed = 0;
        }
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_Sanctuary
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    private void CharacterSave_UCE_Sanctuary(Player player)
    {
#if _MYSQL
		ExecuteNonQueryMySql("DELETE FROM character_lastonline WHERE `character`=@character", new MySqlParameter("@character", player.name));
        ExecuteNonQueryMySql("INSERT INTO character_lastonline VALUES (@character, @lastOnline)",
				new MySqlParameter("@lastOnline", DateTime.UtcNow.ToString("s")),
				new MySqlParameter("@character", player.name));
#elif _SQLITE
        ExecuteNonQuery("DELETE FROM character_lastonline WHERE `character`=@character", new SqliteParameter("@character", player.name));
        ExecuteNonQuery("INSERT INTO character_lastonline VALUES (@character, @lastOnline)",
                new SqliteParameter("@lastOnline", DateTime.UtcNow.ToString("s")),
                new SqliteParameter("@character", player.name));
#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================