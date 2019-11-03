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
    // Connect_UCE_WorldEvents
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_WorldEvents()
    {
#if _MYSQL
		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS uce_worldevents (`name` VARCHAR(64) NOT NULL, `count` INTEGER NOT NULL) CHARACTER SET=utf8mb4");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS uce_worldevents (`name` TEXT NOT NULL, `count` INTEGER NOT NULL)");
#endif
    }

    // -----------------------------------------------------------------------------------
    // UCE_Load_WorldEvents
    // -----------------------------------------------------------------------------------
    public void UCE_Load_WorldEvents()
    {
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT `name`, `count` FROM uce_worldevents");
		foreach (var row in table) {
			string name = (string)row[0];
			int count 	= (int)row[1];

			if (!string.IsNullOrWhiteSpace(name) && count != 0)
			{
				NetworkManagerMMO.UCE_SetWorldEventCount(name, count);
			}
		}
#elif _SQLITE
        var table = ExecuteReader("SELECT `name`, `count` FROM uce_worldevents");
        foreach (var row in table)
        {
            string name = (string)row[0];
            int count = Convert.ToInt32((long)row[1]);

            if (!string.IsNullOrWhiteSpace(name) && count != 0)
            {
                NetworkManagerMMO.UCE_SetWorldEventCount(name, count);
            }
        }
#endif
    }

    // -----------------------------------------------------------------------------------
    // UCE_Save_WorldEvents
    // -----------------------------------------------------------------------------------
    public void UCE_Save_WorldEvents()
    {
#if _MYSQL
		ExecuteNonQueryMySql("DELETE FROM uce_worldevents");
        foreach (UCE_WorldEvent ev in NetworkManagerMMO.UCE_WorldEvents)
        {
            ExecuteNonQueryMySql("INSERT INTO uce_worldevents VALUES (@name, @count)",
                 new MySqlParameter("@name", ev.name),
                 new MySqlParameter("@count", ev.count)
                 );
        }
#elif _SQLITE
        ExecuteNonQuery("DELETE FROM uce_worldevents");
        foreach (UCE_WorldEvent ev in NetworkManagerMMO.UCE_WorldEvents)
        {
            ExecuteNonQuery("INSERT INTO uce_worldevents VALUES (@name, @count)",
                 new SqliteParameter("@name", ev.name),
                 new SqliteParameter("@count", ev.count)
                 );
        }
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_UCE_WorldEvents
    // refresh the world event list once a character is loaded to populate it with data
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    private void CharacterLoad_UCE_WorldEvents(Player player)
    {
        player.UCE_WorldEvents.Clear();
        foreach (UCE_WorldEvent ev in NetworkManagerMMO.UCE_WorldEvents)
        {
            UCE_WorldEvent e = new UCE_WorldEvent();
            e.name = ev.name;
            e.count = ev.count;
            player.UCE_WorldEvents.Add(e);
        }
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_WorldEvents
    // refresh the world event list every time a character is saved to keep it in sync
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    private void CharacterSave_UCE_WorldEvents(Player player)
    {
        foreach (UCE_WorldEvent ev in NetworkManagerMMO.UCE_WorldEvents)
        {
            int id = player.UCE_WorldEvents.FindIndex(x => x.template == ev.template);

            if (id != -1)
            {
                UCE_WorldEvent e = player.UCE_WorldEvents[id];
                e.count = ev.count;
                player.UCE_WorldEvents[id] = e;
            }
        }

        // -- we save the world events as well here, but only if they changed and only once (not for every player)
        NetworkManagerMMO.UCE_SaveWorldEvents();
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================