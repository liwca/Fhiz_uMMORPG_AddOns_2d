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
    // Connect_UCE_Friendlist
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_Friendlist()
    {
#if _MYSQL
		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS character_friends (
                        `character` VARCHAR(32) NOT NULL,
                        friendName VARCHAR(32) NOT NULL,
                        lastGifted TEXT,
                        PRIMARY KEY(`character`, friendName)
                        ) CHARACTER SET=utf8mb4");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS 'character_friends' (
                        character TEXT NOT NULL,
                        friendName TEXT NOT NULL,
                        lastGifted TEXT
        )");
#endif
    }

    // -----------------------------------------------------------------------------------
    // ´CharacterLoad_UCE_Friendlist
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    private void CharacterLoad_UCE_Friendlist(Player player)
    {
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT `character`, friendName, lastGifted FROM character_friends WHERE `character`=@character", new MySqlParameter("character", player.name));
        if (table.Count > 0) {
            for (int i = 0; i < table.Count; i++) {
                var row = table[i];
                UCE_Friend frnd = new UCE_Friend((string)row[1], (string)row[2]);
                player.UCE_Friends.Add(frnd);
            }
        }
#elif _SQLITE
        var table = ExecuteReader("SELECT character, friendName, lastGifted FROM character_friends WHERE character=@character", new SqliteParameter("@character", player.name));
        if (table.Count > 0)
        {
            for (int i = 0; i < table.Count; i++)
            {
                var row = table[i];
                UCE_Friend frnd = new UCE_Friend((string)row[1], (string)row[2]);
                player.UCE_Friends.Add(frnd);
            }
        }
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_Friendlist
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    private void CharacterSave_UCE_Friendlist(Player player)
    {
#if _MYSQL
		var query2 = @"
            INSERT INTO character_friends
            SET
            `character`=@character,
            friendName = @friendName,
            lastGifted = @lastGifted

            ON DUPLICATE KEY UPDATE
            friendName = @friendName,
            lastGifted = @lastGifted
            ";

        for (int i = 0; i < player.UCE_Friends.Count; i++)
        {
            UCE_Friend frnd = player.UCE_Friends[i];
            ExecuteNonQueryMySql(query2,
                                 new MySqlParameter("@character", player.name),
                                 new MySqlParameter("@friendName", frnd.name),
                                 new MySqlParameter("@lastGifted", frnd.lastGifted)
            );
        }
#elif _SQLITE
        ExecuteNonQuery("DELETE FROM character_friends WHERE character=@character", new SqliteParameter("@character", player.name));

        for (int i = 0; i < player.UCE_Friends.Count; i++)
        {
            UCE_Friend frnd = player.UCE_Friends[i];
            ExecuteNonQuery("INSERT INTO character_friends VALUES (@character, @friendName, @lastGifted)",
                            new SqliteParameter("@character", player.name),
                            new SqliteParameter("@friendName", frnd.name),
                            new SqliteParameter("@lastGifted", frnd.lastGifted)
            );
        }
#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================