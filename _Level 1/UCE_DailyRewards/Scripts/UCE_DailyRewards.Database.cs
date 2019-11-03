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
    // Connect_UCE_DailyRewards
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_DailyRewards()
    {
#if _MYSQL

		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS character_dailyrewards (
					`character` VARCHAR(32) NOT NULL,
					counter INTEGER NOT NULL,
					resetTime INTEGER NOT NULL,
                        PRIMARY KEY (`character`)
			) CHARACTER SET=utf8mb4");

        ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS character_lastonline (
				    `character` VARCHAR(32) NOT NULL,
				    lastOnline VARCHAR(64) NOT NULL,
                        PRIMARY KEY(`character`)
              ) CHARACTER SET=utf8mb4");

#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS character_lastonline (
					character TEXT NOT NULL,
					lastOnline TEXT NOT NULL
			)");

        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS character_dailyrewards (
					character TEXT NOT NULL,
					counter INTEGER NOT NULL,
					resetTime FLOAT NOT NULL
			)");
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_UCE_DailyRewards
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    public void CharacterLoad_UCE_DailyRewards(Player player)
    {
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT counter, resetTime FROM character_dailyrewards WHERE `character`=@name", new MySqlParameter("@name", player.name));
		if (table.Count == 1) {
            var row = table[0];
			player.dailyRewardCounter 	= (int)row[0];
			player.dailyRewardReset		= (int)row[1];
		}
#elif _SQLITE
        var table = ExecuteReader("SELECT counter, resetTime FROM character_dailyrewards WHERE `character`=@name", new SqliteParameter("@name", player.name));
        if (table.Count == 1)
        {
            var row = table[0];
            player.dailyRewardCounter = Convert.ToInt32((long)row[0]);
            player.dailyRewardReset = (double)row[1];
        }
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_DailyRewards
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    private void CharacterSave_UCE_DailyRewards(Player player)
    {
        DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan timeSinceEpoch = DateTime.UtcNow - UnixEpoch;
#if _MYSQL
		var query2 = @"
            INSERT INTO character_dailyrewards
            SET
            `character`=@character,
            counter = @counter,
            resetTime = @resetTime

            ON DUPLICATE KEY UPDATE
            counter = @counter,
            resetTime = @resetTime
            ";
        ExecuteNonQueryMySql(query2,
                    new MySqlParameter("@character", player.name),
                    new MySqlParameter("@counter", player.dailyRewardCounter),
                    new MySqlParameter("@resetTime", timeSinceEpoch.TotalHours));

        var query = @"
            INSERT INTO character_lastonline
            SET
            `character`=@character,
            lastOnline=@lastOnline

            ON DUPLICATE KEY UPDATE
            lastOnline=@lastOnline
            ";
        ExecuteNonQueryMySql(query,
                    new MySqlParameter("@lastOnline", DateTime.UtcNow.ToString("s")),
                    new MySqlParameter("@character", player.name));
#elif _SQLITE
        ExecuteNonQuery("DELETE FROM character_lastonline WHERE `character`=@character", new SqliteParameter("@character", player.name));
        ExecuteNonQuery("INSERT INTO character_lastonline VALUES (@character, @lastOnline)",
                new SqliteParameter("@lastOnline", DateTime.UtcNow.ToString("s")),
                new SqliteParameter("@character", player.name));

        ExecuteNonQuery("DELETE FROM character_dailyrewards WHERE `character`=@character", new SqliteParameter("@character", player.name));
        ExecuteNonQuery("INSERT INTO character_dailyrewards VALUES (@character, @counter, @resetTime)",
                new SqliteParameter("@character", player.name),
                new SqliteParameter("@counter", player.dailyRewardCounter),
                new SqliteParameter("@resetTime", timeSinceEpoch.TotalHours));
#endif
    }

    // -----------------------------------------------------------------------------------
    // UCE_DailyRewards_HoursPassed
    // -----------------------------------------------------------------------------------
    public double UCE_DailyRewards_HoursPassed(Player player)
    {
#if _MYSQL
		var row = (string)ExecuteScalarMySql("SELECT lastOnline FROM character_lastonline WHERE  `character`=@name", new MySqlParameter("@name", player.name));
		if (!string.IsNullOrWhiteSpace(row)) {
			DateTime time 			= DateTime.Parse(row);
			return (DateTime.UtcNow - time).TotalSeconds/3600;
		}
		return 0;
#elif _SQLITE
        var row = (string)ExecuteScalar("SELECT lastOnline FROM character_lastonline WHERE `character`=@name", new SqliteParameter("@name", player.name));
        if (!string.IsNullOrWhiteSpace(row))
        {
            DateTime time = DateTime.Parse(row);
            return (DateTime.UtcNow - time).TotalSeconds / 3600;
        }
        return 0;
#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================