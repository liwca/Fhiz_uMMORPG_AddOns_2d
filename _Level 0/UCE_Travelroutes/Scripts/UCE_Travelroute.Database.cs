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
    // Connect_UCE_Travelroutes
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_Travelroutes()
    {
#if _MYSQL
		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS character_travelroutes (
				`character` VARCHAR(32) NOT NULL,
				travelroute VARCHAR(32) NOT NULL
				) CHARACTER SET=utf8mb4 ");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS character_travelroutes (
				character TEXT NOT NULL,
				travelroute TEXT NOT NULL
				)");
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_UCE_Travelroutes
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    private void CharacterLoad_UCE_Travelroutes(Player player)
    {
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT travelroute FROM character_travelroutes WHERE `character`=@name", new MySqlParameter("@name", player.name));
		foreach (var row in table) {
			UCE_TravelrouteClass tRoute = new UCE_TravelrouteClass((string)row[0]);
			player.UCE_travelroutes.Add(tRoute);
		}
#elif _SQLITE
        var table = ExecuteReader("SELECT travelroute FROM character_travelroutes WHERE `character`=@name", new SqliteParameter("@name", player.name));
        foreach (var row in table)
        {
            UCE_TravelrouteClass tRoute = new UCE_TravelrouteClass((string)row[0]);
            player.UCE_travelroutes.Add(tRoute);
        }
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_Travelroutes
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    private void CharacterSave_UCE_Travelroutes(Player player)
    {
#if _MYSQL
		ExecuteNonQueryMySql("DELETE FROM character_travelroutes WHERE `character`=@character", new MySqlParameter("@character", player.name));
		for (int i = 0; i < player.UCE_travelroutes.Count; ++i) {
            ExecuteNonQueryMySql("INSERT INTO character_travelroutes VALUES (@character, @travelroute)",
 				new MySqlParameter("@character", player.name),
 				new MySqlParameter("@travelroute", player.UCE_travelroutes[i].name));
 		}
#elif _SQLITE
        ExecuteNonQuery("DELETE FROM character_travelroutes WHERE `character`=@character", new SqliteParameter("@character", player.name));
        for (int i = 0; i < player.UCE_travelroutes.Count; ++i)
        {
            ExecuteNonQuery("INSERT INTO character_travelroutes VALUES (@character, @travelroute)",
                 new SqliteParameter("@character", player.name),
                 new SqliteParameter("@travelroute", player.UCE_travelroutes[i].name));
        }
#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================