// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

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
    // Connect_UCE_PVPZone
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    public void Connect_UCE_PVPZone()
    {
#if _MYSQL
		ExecuteReaderMySql(@"CREATE TABLE IF NOT EXISTS character_pvpzones (
			`character` VARCHAR(32) NOT NULL,
			realm VARCHAR(32) NOT NULL,
			alliedrealm VARCHAR(32) NOT NULL
		) CHARACTER SET=utf8mb4");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS character_pvpzones (
			character TEXT NOT NULL,
			realm TEXT NOT NULL,
			alliedrealm TEXT NOT NULL
		)");
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_UCE_PVPZone
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    public void CharacterLoad_UCE_PVPZone(Player player)
    {
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT realm, alliedrealm FROM character_pvpzones WHERE `character`=@name", new MySqlParameter("@name", player.name));

#elif _SQLITE
        var table = ExecuteReader("SELECT realm, alliedrealm FROM character_pvpzones WHERE `character`=@name", new SqliteParameter("@name", player.name));
#endif
        if (table.Count == 1)
        {
            var row = table[0];
            string realm = (string)row[0];
            string ally = (string)row[1];
            player.UCE_setRealm(realm.GetStableHashCode(), ally.GetStableHashCode());
        }
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_PVPZone
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    public void CharacterSave_UCE_PVPZone(Player player)
    {
#if _MYSQL
		ExecuteNonQueryMySql("DELETE FROM character_pvpzones WHERE `character`=@character", new MySqlParameter("@character", player.name));
        ExecuteNonQueryMySql("INSERT INTO character_pvpzones VALUES (@character, @realm, @alliedrealm)",
				new MySqlParameter("@character", 	player.name),
				new MySqlParameter("@realm", 		(player.Realm != null) ? player.Realm.name : ""),
				new MySqlParameter("@alliedrealm", 	(player.Ally != null) ? player.Ally.name : ""));
#elif _SQLITE
        ExecuteNonQuery("DELETE FROM character_pvpzones WHERE `character`=@character", new SqliteParameter("@character", player.name));
        ExecuteNonQuery("INSERT INTO character_pvpzones VALUES (@character, @realm, @alliedrealm)",
                new SqliteParameter("@character", player.name),
                new SqliteParameter("@realm", (player.Realm != null) ? player.Realm.name : ""),
                new SqliteParameter("@alliedrealm", (player.Ally != null) ? player.Ally.name : ""));
#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================