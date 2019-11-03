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
    // Connect_UCE_Factions
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_Factions()
    {
#if _MYSQL
		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS character_factions (
        				`character` VARCHAR(32) NOT NULL,
        				faction VARCHAR(32)  NOT NULL,
        				rating INTEGER(15)
        				) CHARACTER SET=utf8mb4 ");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS character_factions (
        				character TEXT NOT NULL,
        				faction TEXT NOT NULL,
        				rating INTEGER
        				)");
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_UCE_Factions
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    public void CharacterLoad_UCE_Factions(Player player)
    {
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT faction, rating FROM character_factions WHERE `character`=@character", new MySqlParameter("@character", player.name));

        foreach (var row in table) {
            UCE_Faction faction 	= new UCE_Faction();
            faction.name 			= (string)row[0];
            faction.rating 			= Convert.ToInt32(row[1]);
            player.UCE_Factions.Add(faction);
        }
#elif _SQLITE
        var table = ExecuteReader("SELECT faction, rating FROM character_factions WHERE character=@character", new SqliteParameter("@character", player.name));

        foreach (var row in table)
        {
            UCE_Faction faction = new UCE_Faction();
            faction.name = (string)row[0];
            faction.rating = Convert.ToInt32((long)row[1]);
            player.UCE_Factions.Add(faction);
        }
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_Factions
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    public void CharacterSave_UCE_Factions(Player player)
    {
#if _MYSQL
		ExecuteNonQueryMySql("DELETE FROM character_factions WHERE `character`=@character", new MySqlParameter("@character", player.name));
        foreach (UCE_Faction faction in player.UCE_Factions)
            ExecuteNonQueryMySql("INSERT INTO character_factions VALUES (@character, @faction, @rating)",
                            new MySqlParameter("@character", player.name),
                            new MySqlParameter("@faction", faction.name),
                            new MySqlParameter("@rating", faction.rating));
#elif _SQLITE
        ExecuteNonQuery("DELETE FROM character_factions WHERE character=@character", new SqliteParameter("@character", player.name));
        foreach (UCE_Faction faction in player.UCE_Factions)
            ExecuteNonQuery("INSERT INTO character_factions VALUES (@character, @faction, @rating)",
                            new SqliteParameter("@character", player.name),
                            new SqliteParameter("@faction", faction.name),
                            new SqliteParameter("@rating", faction.rating));
#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================