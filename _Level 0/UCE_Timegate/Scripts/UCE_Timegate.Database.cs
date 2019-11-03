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
    // Connect_UCE_SimpleTimegate
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_SimpleTimegate()
    {
#if _MYSQL
		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS character_timegates (
			`character` VARCHAR(32) NOT NULL,
			timegateName TEXT NOT NULL,
			timegateCount INTEGER NOT NULL,
			timegateHours TEXT NOT NULL
              ) CHARACTER SET=utf8mb4");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS character_timegates (
			character TEXT NOT NULL,
			timegateName TEXT NOT NULL,
			timegateCount INTEGER NOT NULL,
			timegateHours TEXT NOT NULL)");
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_UCE_SimpleTimegate
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    private void CharacterLoad_UCE_SimpleTimegate(Player player)
    {
        player.UCE_timegates.Clear();

#if _MYSQL
		var table = ExecuteReaderMySql("SELECT timegateName, timegateCount, timegateHours FROM character_timegates WHERE `character`=@name", new MySqlParameter("@name", player.name));
		foreach (var row in table) {
			UCE_Timegate timegate = new UCE_Timegate();
			timegate.name = (string)row[0];
			timegate.count = Convert.ToInt32((int)row[1]);
			timegate.hours = (string)row[2];
			timegate.valid = true;
			player.UCE_timegates.Add(timegate);
		}
#elif _SQLITE
        var table = ExecuteReader("SELECT timegateName, timegateCount, timegateHours FROM character_timegates WHERE `character`=@name", new SqliteParameter("@name", player.name));
        foreach (var row in table)
        {
            UCE_Timegate timegate = new UCE_Timegate();
            timegate.name = (string)row[0];
            timegate.count = Convert.ToInt32((long)row[1]);
            timegate.hours = (string)row[2];
            timegate.valid = true;
            player.UCE_timegates.Add(timegate);
        }
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_SimpleTimegate
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    private void CharacterSave_UCE_SimpleTimegate(Player player)
    {
#if _MYSQL
		ExecuteNonQueryMySql("DELETE FROM character_timegates WHERE `character`=@character", new MySqlParameter("@character", player.name));
		for (int i = 0; i < player.UCE_timegates.Count; ++i) {
            ExecuteNonQueryMySql("INSERT INTO character_timegates VALUES (@character, @timegateName, @timegateCount, @timegateHours)",
 				new MySqlParameter("@character", player.name),
 				new MySqlParameter("@timegateName", player.UCE_timegates[i].name),
 				new MySqlParameter("@timegateCount", player.UCE_timegates[i].count),
 				new MySqlParameter("@timegateHours", player.UCE_timegates[i].hours));
 		}
#elif _SQLITE
        ExecuteNonQuery("DELETE FROM character_timegates WHERE `character`=@character", new SqliteParameter("@character", player.name));
        for (int i = 0; i < player.UCE_timegates.Count; ++i)
        {
            ExecuteNonQuery("INSERT INTO character_timegates VALUES (@character, @timegateName, @timegateCount, @timegateHours)",
                 new SqliteParameter("@character", player.name),
                 new SqliteParameter("@timegateName", player.UCE_timegates[i].name),
                 new SqliteParameter("@timegateCount", player.UCE_timegates[i].count),
                 new SqliteParameter("@timegateHours", player.UCE_timegates[i].hours));
        }
#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================