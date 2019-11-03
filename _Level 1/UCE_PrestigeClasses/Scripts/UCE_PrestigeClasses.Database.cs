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
    // Connect_UCE_PrestigeClasses
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    public void Connect_UCE_PrestigeClasses()
    {
#if _MYSQL
		ExecuteReaderMySql(@"CREATE TABLE IF NOT EXISTS character_prestigeclasses (
			`character` VARCHAR(32) NOT NULL,
			class1 VARCHAR(32) NOT NULL,
			class2 VARCHAR(32) NOT NULL
		) CHARACTER SET=utf8mb4");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS character_prestigeclasses (
			character TEXT NOT NULL,
			class1 TEXT NOT NULL,
			class2 TEXT NOT NULL
		)");
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_UCE_PrestigeClasses
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    public void CharacterLoad_UCE_PrestigeClasses(Player player)
    {
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT class1, class2 FROM character_prestigeclasses WHERE `character`=@name", new MySqlParameter("@name", player.name));

#elif _SQLITE
        var table = ExecuteReader("SELECT class1, class2 FROM character_prestigeclasses WHERE `character`=@name", new SqliteParameter("@name", player.name));
#endif
        if (table.Count == 1)
        {
            var row = table[0];
            string class1 = (string)row[0];
            string class2 = (string)row[1];

            UCE_PrestigeClassTemplate prestigeClass1 = null;
            if (UCE_PrestigeClassTemplate.dict.TryGetValue(class1.GetStableHashCode(), out prestigeClass1))
                player.UCE_prestigeClass = prestigeClass1;
        }
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_PrestigeClasses
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    public void CharacterSave_UCE_PrestigeClasses(Player player)
    {
#if _MYSQL
		ExecuteNonQueryMySql("DELETE FROM character_prestigeclasses WHERE `character`=@character", new MySqlParameter("@character", player.name));
        ExecuteNonQueryMySql("INSERT INTO character_prestigeclasses VALUES (@character, @class1, @class2)",
				new MySqlParameter("@character", 	player.name),
				new MySqlParameter("@class1", 		(player.UCE_prestigeClass != null) ? player.UCE_prestigeClass.name : ""),
				new MySqlParameter("@class2", 		""));
#elif _SQLITE
        ExecuteNonQuery("DELETE FROM character_prestigeclasses WHERE `character`=@character", new SqliteParameter("@character", player.name));
        ExecuteNonQuery("INSERT INTO character_prestigeclasses VALUES (@character, @class1, @class2)",
                new SqliteParameter("@character", player.name),
                new SqliteParameter("@class1", (player.UCE_prestigeClass != null) ? player.UCE_prestigeClass.name : ""),
                new SqliteParameter("@class2", ""));
#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================