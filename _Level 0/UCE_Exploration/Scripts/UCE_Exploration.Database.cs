// =======================================================================================
// Created and maintained by iMMO
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;
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
    // Connect_UCE_Exploration
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_Exploration()
    {
#if _MYSQL
		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS character_exploration (`character` VARCHAR(32) NOT NULL, exploredArea VARCHAR(32) NOT NULL) CHARACTER SET=utf8mb4");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS character_exploration (character TEXT NOT NULL, exploredArea TEXT NOT NULL)");
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_UCE_Exploration
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    private void CharacterLoad_UCE_Exploration(Player player)
    {
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT exploredArea FROM character_exploration WHERE `character`=@character",
						new MySqlParameter("@character", player.name)
						);
		foreach (var row in table) {
			player.UCE_exploredAreas.Add((string)row[0]);
		}
#elif _SQLITE
        var table = ExecuteReader("SELECT exploredArea FROM character_exploration WHERE `character`=@character",
                        new SqliteParameter("@character", player.name)
                        );
        foreach (var row in table)
        {
            player.UCE_exploredAreas.Add((string)row[0]);
        }
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_Exploration
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    private void CharacterSave_UCE_Exploration(Player player)
    {
#if _MYSQL
		ExecuteNonQueryMySql("DELETE FROM character_exploration WHERE `character`=@character", new MySqlParameter("@character", player.name));
        for (int i = 0; i < player.UCE_exploredAreas.Count; ++i)
        {
            ExecuteNonQueryMySql("INSERT INTO character_exploration VALUES (@character, @exploredArea)",
                 new MySqlParameter("@character", player.name),
                 new MySqlParameter("@exploredArea", player.UCE_exploredAreas[i])
                 );
        }
#elif _SQLITE
        ExecuteNonQuery("DELETE FROM character_exploration WHERE `character`=@character", new SqliteParameter("@character", player.name));
        for (int i = 0; i < player.UCE_exploredAreas.Count; i++)
        {
            ExecuteNonQuery("INSERT INTO character_exploration VALUES (@character, @exploredArea)",
                 new SqliteParameter("@character", player.name),
                 new SqliteParameter("@exploredArea", player.UCE_exploredAreas[i])
                 );
        }
#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================