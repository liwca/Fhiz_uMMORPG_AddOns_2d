// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;
using System.Collections.Generic;

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
    // Connect_UCE_PlaceableObject
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_PlaceableObject()
    {
#if _MYSQL
		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS placeable_objects (
				`character` VARCHAR(32) NOT NULL,
				guild VARCHAR(32) NOT NULL,
				x FLOAT NOT NULL,
            	y FLOAT NOT NULL,
            	z FLOAT NOT NULL,
            	xRot FLOAT NOT NULL,
            	yRot FLOAT NOT NULL,
            	zRot FLOAT NOT NULL,
            	level INT NOT NULL,
                item VARCHAR(64) NOT NULL,
                id INT NOT NULL
                )");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS placeable_objects (
				`character` TEXT NOT NULL,
				guild TEXT NOT NULL,
				x REAL NOT NULL,
            	y REAL NOT NULL,
            	z REAL NOT NULL,
            	xRot REAL NOT NULL,
            	yRot REAL NOT NULL,
            	zRot REAL NOT NULL,
            	level INTEGER NOT NULL,
                item TEXT NOT NULL,
                id INTEGER NOT NULL
                )");
#endif
    }

    // -----------------------------------------------------------------------------------
    // UCE_SavePlaceableObject
    // -----------------------------------------------------------------------------------
    public void UCE_SavePlaceableObject(string character, string guild, GameObject placeableObject, int level, string itemName, int id)
    {
        Vector3 position = placeableObject.transform.position;
        Vector3 rotation = placeableObject.transform.rotation.eulerAngles;
#if _MYSQL
		ExecuteNonQueryMySql("INSERT INTO placeable_objects VALUES (@character, @guild, @x, @y, @z, @xRot, @yRot, @zRot, @level, @item, @id)",
				new MySqlParameter("@character", 	character),
				new MySqlParameter("@guild", 		guild == null ? "" : guild),
				new MySqlParameter("@x", 			position.x),
				new MySqlParameter("@y", 			position.y),
				new MySqlParameter("@z", 			position.z),
				new MySqlParameter("@xRot", 		rotation.x),
				new MySqlParameter("@yRot", 		rotation.y),
				new MySqlParameter("@zRot", 		rotation.z),
				new MySqlParameter("@level", 		level),
				new MySqlParameter("@item", 		itemName),
				new MySqlParameter("@id", 			id)
				);
#elif _SQLITE
        ExecuteNonQuery("INSERT INTO placeable_objects VALUES (@character, @guild, @x, @y, @z, @xRot, @yRot, @zRot, @level, @item, @id)",
                new SqliteParameter("@character", character),
                new SqliteParameter("@guild", guild == null ? "" : guild),
                new SqliteParameter("@x", position.x),
                new SqliteParameter("@y", position.y),
                new SqliteParameter("@z", position.z),
                new SqliteParameter("@xRot", rotation.x),
                new SqliteParameter("@yRot", rotation.y),
                new SqliteParameter("@zRot", rotation.z),
                new SqliteParameter("@level", level),
                new SqliteParameter("@item", itemName),
                new SqliteParameter("@id", id)
                );
#endif
    }

    // -----------------------------------------------------------------------------------
    // UCE_DeletePlaceableObject
    // -----------------------------------------------------------------------------------
    public void UCE_DeletePlaceableObject(string character, string guild, int level, string itemName, int id)
    {
#if _MYSQL
		ExecuteNonQueryMySql("DELETE FROM placeable_objects WHERE (`character`=@character AND guild=@guild AND level=@level AND item=@item AND id=@id)",
				new MySqlParameter("@character", 	character),
				new MySqlParameter("@guild", 		guild == null ? "" : guild),
				new MySqlParameter("@level", 		level),
				new MySqlParameter("@item", 		itemName),
				new MySqlParameter("@id", 			id)
				);
#elif _SQLITE
        ExecuteNonQuery("DELETE FROM placeable_objects WHERE (`character`=@character AND guild=@guild AND level=@level AND item=@item AND id=@id)",
                new SqliteParameter("@character", character),
                new SqliteParameter("@guild", guild == null ? "" : guild),
                new SqliteParameter("@level", level),
                new SqliteParameter("@item", itemName),
                new SqliteParameter("@id", id)
                );
#endif
    }

    // -----------------------------------------------------------------------------------
    // UCE_LoadPlaceableObjects
    // -----------------------------------------------------------------------------------
    public List<List<object>> UCE_LoadPlaceableObjects()
    {
        List<List<object>> objects = new List<List<object>>();
#if _MYSQL
		if (0 < (long)Database.singleton.ExecuteScalarMySql("SELECT count(*) FROM placeable_objects"))
			objects = Database.singleton.ExecuteReaderMySql("SELECT `character`, guild, x, y, z, xRot, yRot, zRot, level, item, id FROM placeable_objects");
#elif _SQLITE
        if (0 < (long)Database.singleton.ExecuteScalar("SELECT count(*) FROM placeable_objects"))
            objects = Database.singleton.ExecuteReader("SELECT character, guild, x, y, z, xRot, yRot, zRot, level, item, id FROM placeable_objects");
#endif
        return objects;
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================