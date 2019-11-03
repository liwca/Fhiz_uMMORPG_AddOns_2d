// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;

#if _MYSQL
using MySql.Data;								// From MySql.Data.dll in Plugins folder
using MySql.Data.MySqlClient;                   // From MySql.Data.dll in Plugins folder
using SqlParameter = MySql.Data.MySqlClient.MySqlParameter;
#elif _SQLITE

using Mono.Data.Sqlite; 						// copied from Unity/Mono/lib/mono/2.0 to Plugins

#endif

// =======================================================================================
// DATABASE (SQLite / mySQL Hybrid)
// =======================================================================================
public partial class Database
{
    // -----------------------------------------------------------------------------------
    // Connect_UCE_NetworkZone
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_NetworkZone()
    {
#if _MYSQL

		ExecuteNonQueryMySql(@"
        CREATE TABLE IF NOT EXISTS character_scene (
            `character` VARCHAR(32) NOT NULL,
            scene VARCHAR(64) NOT NULL,
            PRIMARY KEY(`character`)
            ) CHARACTER SET=utf8mb4");

        ExecuteNonQueryMySql(@"
        CREATE TABLE IF NOT EXISTS zones_online (
            id INT NOT NULL AUTO_INCREMENT,
            PRIMARY KEY(id),
            online TIMESTAMP NOT NULL
        ) CHARACTER SET=utf8mb4");

#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS character_scene (
                            character TEXT NOT NULL PRIMARY KEY,
                            scene TEXT NOT NULL)");

        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS zones_online (
                            online TEXT NOT NULL)");
#endif
    }

    // -----------------------------------------------------------------------------------
    // IsCharacterOnlineAnywhere
    // a character is online on any of the servers if the online string is not
    // empty and if the time difference is less than the save interval * 2
    // (* 2 to have some tolerance)
    // -----------------------------------------------------------------------------------
    public bool IsCharacterOnlineAnywhere(string characterName)
    {
        float saveInterval = ((NetworkManagerMMO)NetworkManager.singleton).saveInterval;

#if _MYSQL

		var obj = ExecuteScalarMySql("SELECT online FROM characters WHERE name=@name", new SqlParameter("@name", characterName));
		if (obj != null)
        {
            var time = (DateTime)obj;
            double elapsedSeconds = (DateTime.UtcNow - time).TotalSeconds;
            return elapsedSeconds < saveInterval * 2;
        }

#elif _SQLITE

        object obj = ExecuteScalar("SELECT online FROM characters WHERE name=@name", new SqliteParameter("@name", characterName));
        if (obj != null)
        {
            string online = (string)obj;
            if (online != "")
            {
                DateTime time = DateTime.Parse(online);
                double elapsedSeconds = (DateTime.UtcNow - time).TotalSeconds;

                return elapsedSeconds < saveInterval * 2;
            }
        }

#endif
        return false;
    }

    // -----------------------------------------------------------------------------------
    // AnyAccountCharacterOnline
    // -----------------------------------------------------------------------------------
    public bool AnyAccountCharacterOnline(string account)
    {
        List<string> characters = CharactersForAccount(account);
        return characters.Any(IsCharacterOnlineAnywhere);
    }

    // -----------------------------------------------------------------------------------
    // GetCharacterScene
    // -----------------------------------------------------------------------------------
    public string GetCharacterScene(string characterName)
    {
#if _MYSQL
        object obj = ExecuteScalarMySql("SELECT scene FROM character_scene WHERE `character`=@character", new SqlParameter("@character", characterName));
#elif _SQLITE
        object obj = ExecuteScalar("SELECT scene FROM character_scene WHERE character=@character", new SqliteParameter("@character", characterName));
#endif
        return obj != null ? (string)obj : "";
    }

    // -----------------------------------------------------------------------------------
    // SaveCharacterScene
    // -----------------------------------------------------------------------------------
    public void SaveCharacterScene(string characterName, string sceneName)
    {
#if _MYSQL
		var query = @"
            INSERT INTO character_scene
            SET
                `character`=@character,
                scene=@scene
            ON DUPLICATE KEY UPDATE
                scene=@scene";

        ExecuteNonQueryMySql(query,
                             new SqlParameter("@character", characterName),
                             new SqlParameter("@scene", sceneName));
#elif _SQLITE

        ExecuteNonQuery("INSERT OR REPLACE INTO character_scene VALUES (@character, @scene)",
                        new SqliteParameter("@character", characterName),
                        new SqliteParameter("@scene", sceneName));
#endif
    }

    // -----------------------------------------------------------------------------------
    // TimeElapsedSinceMainZoneOnline
    // a zone is online if the online string is not empty and if the time
    // difference is less than the write interval * multiplier
    // (* multiplier to have some tolerance)
    // -----------------------------------------------------------------------------------
    public double TimeElapsedSinceMainZoneOnline()
    {
#if _MYSQL
		var obj = ExecuteScalarMySql("SELECT online FROM zones_online");
        if (obj != null)
        {
            var time = (DateTime)obj;
            return (DateTime.Now - time).TotalSeconds;
        }
#elif _SQLITE
        object obj = ExecuteScalar("SELECT online FROM zones_online");
        if (obj != null)
        {
            string online = (string)obj;
            if (online != "")
            {
                DateTime time = DateTime.Parse(online);
                return (DateTime.UtcNow - time).TotalSeconds;
            }
        }
#endif
        return Mathf.Infinity;
    }

    // -----------------------------------------------------------------------------------
    // SaveMainZoneOnlineTime
    // Note: should only be called by main zone
    // online status:
    //   '' if offline (if just logging out etc.)
    //   current time otherwise
    // -> it uses the ISO 8601 standard format
    // -----------------------------------------------------------------------------------
    public void SaveMainZoneOnlineTime()
    {
#if _MYSQL

		var query = @"
            INSERT INTO zones_online
            SET
                id=@id,
                online=@online
            ON DUPLICATE KEY UPDATE
                online=@online";

        ExecuteNonQueryMySql(query,
                             new SqlParameter("@id", 1),
                             new SqlParameter("@online", DateTime.Now));

#elif _SQLITE

        string onlineString = DateTime.UtcNow.ToString("s");
        ExecuteNonQuery("DELETE FROM zones_online");
        ExecuteNonQuery("INSERT OR REPLACE INTO zones_online VALUES (@online)",
                        new SqliteParameter("@online", onlineString));

#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================