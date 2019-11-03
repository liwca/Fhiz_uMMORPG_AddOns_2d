// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;

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
    // Connect_UCE_Bindpoint
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_Bindpoint()
    {
#if _MYSQL
		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS character_bindpoint (
					 `character` VARCHAR(32) NOT NULL,
					 `name` VARCHAR(32) NOT NULL,
					x FLOAT NOT NULL,
            		y FLOAT NOT NULL,
            		z FLOAT NOT NULL,
            		sceneName VARCHAR(64) NOT NULL,
                    PRIMARY KEY(`character`)
                ) CHARACTER SET=utf8mb4");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS character_bindpoint (
					character TEXT NOT NULL,
					name TEXT NOT NULL,
					x REAL NOT NULL,
            		y REAL NOT NULL,
            		z REAL NOT NULL,
            		sceneName TEXT NOT NULL
                )");
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_Bindpoint
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    private void CharacterSave_UCE_Bindpoint(Player player)
    {
        if (!player.UCE_myBindpoint.Valid) return;

#if _MYSQL
        ExecuteNonQueryMySql("DELETE FROM character_bindpoint WHERE `character`=@character", new MySqlParameter("@character", player.name));
        ExecuteNonQueryMySql("INSERT INTO character_bindpoint VALUES (@character, @name, @x, @y, @z, @sceneName)",
				new MySqlParameter("@character", 	player.name),
				new MySqlParameter("@name", 		player.UCE_myBindpoint.name),
				new MySqlParameter("@x", 			player.UCE_myBindpoint.position.x),
				new MySqlParameter("@y", 			player.UCE_myBindpoint.position.y),
				new MySqlParameter("@z", 			player.UCE_myBindpoint.position.z),
				new MySqlParameter("@sceneName", 	player.UCE_myBindpoint.SceneName)
				);
#elif _SQLITE
        ExecuteNonQuery("DELETE FROM character_bindpoint WHERE `character`=@character", new SqliteParameter("@character", player.name));
        ExecuteNonQuery("INSERT INTO character_bindpoint VALUES (@character, @name, @x, @y, @z, @sceneName)",
                new SqliteParameter("@character", player.name),
                new SqliteParameter("@name", player.UCE_myBindpoint.name),
                new SqliteParameter("@x", player.UCE_myBindpoint.position.x),
                new SqliteParameter("@y", player.UCE_myBindpoint.position.y),
                new SqliteParameter("@z", player.UCE_myBindpoint.position.z),
                new SqliteParameter("@sceneName", player.UCE_myBindpoint.SceneName)
                );
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_UCE_Bindpoint
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    private void CharacterLoad_UCE_Bindpoint(Player player)
    {
        player.UCE_myBindpoint = new UCE_BindPoint();

#if _MYSQL
		var table = ExecuteReaderMySql("SELECT name, x, y, z, sceneName FROM character_bindpoint WHERE `character`=@name", new MySqlParameter("@name", player.name));
#elif _SQLITE
        var table = ExecuteReader("SELECT name, x, y, z, sceneName FROM character_bindpoint WHERE character=@name", new SqliteParameter("@name", player.name));
#endif
        if (table.Count == 1)
        {
            var row = table[0];

            Vector3 p = new Vector3((float)row[1], (float)row[2], (float)row[3]);
            string sceneName = (string)row[4];

            if (p != Vector3.zero && !string.IsNullOrEmpty(sceneName))
            {
                player.UCE_myBindpoint.name = (string)row[0];
                player.UCE_myBindpoint.position = p;
                player.UCE_myBindpoint.SceneName = sceneName;
            }
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================