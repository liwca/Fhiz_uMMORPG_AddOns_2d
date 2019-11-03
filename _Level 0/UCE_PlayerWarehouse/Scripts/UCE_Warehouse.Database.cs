// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;
using Mirror;
using System;
using System.IO;
using System.Linq;
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
    // Connect_UCE_Warehouse
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_Warehouse()
    {
#if _MYSQL

		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS uce_warehouse (
							`character` VARCHAR(32) NOT NULL PRIMARY KEY,
							gold INTEGER(16) NOT NULL DEFAULT 0,
							level INTEGER(16) NOT NULL DEFAULT 0
							) CHARACTER SET=utf8mb4");

		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS uce_warehouse_items (
                           `character` VARCHAR(32) NOT NULL,
                           slot INTEGER(16) NOT NULL,
                           `name` VARCHAR(32) NOT NULL,
                           amount INTEGER(16) NOT NULL,
                           summonedHealth INTEGER NOT NULL,
                           summonedLevel INTEGER NOT NULL,
                           summonedExperience INTEGER NOT NULL
                           ) CHARACTER SET=utf8mb4");

#elif _SQLITE

        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS 'uce_warehouse' (
							'character' TEXT NOT NULL PRIMARY KEY,
							'gold' INTEGER NOT NULL DEFAULT 0,
							'level' INTEGER NOT NULL DEFAULT 0
							)");

        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS 'uce_warehouse_items' (
                           'character' TEXT NOT NULL,
                           'slot' INTEGER NOT NULL,
                           'name' TEXT NOT NULL,
                           'amount' INTEGER NOT NULL,
                           'summonedHealth' INTEGER NOT NULL,
                           'summonedLevel' INTEGER NOT NULL,
                           'summonedExperience' INTEGER NOT NULL)");

#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_UCE_Warehouse
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    private void CharacterLoad_UCE_Warehouse(Player player)
    {
        player.warehouseActionDone = false;

#if _MYSQL

		var warehouseData = ExecuteReaderMySql("SELECT gold, level FROM uce_warehouse WHERE `character`=@character", new MySqlParameter("@character", player.name));

		if (warehouseData.Count == 1) {
			player.playerWarehouseGold 		= Convert.ToInt32(warehouseData[0][0]);
			player.playerWarehouseLevel 	= Convert.ToInt32(warehouseData[0][1]);
		} else {
			ExecuteNonQueryMySql("INSERT INTO uce_warehouse (`character`, gold, level) VALUES(@character, 0, 0)", new MySqlParameter("@character", player.name));
			player.playerWarehouseGold 		= 0;
			player.playerWarehouseLevel 	= 0;
		}

		for (int i = 0; i < player.playerWarehouseStorageItems; ++i) {
			player.UCE_playerWarehouse.Add(new ItemSlot());
		}

		List<List<object>> table = ExecuteReaderMySql("SELECT `name`, slot, amount, summonedHealth, summonedLevel, summonedExperience FROM uce_warehouse_items WHERE `character`=@character", new MySqlParameter("@character", player.name));
		if (table.Count > 0) {
			foreach (List<object> row in table) {
				string itemName 	= (string)row[0];
				int slot 			= Convert.ToInt32(row[1]);
				ScriptableItem template;

				if (slot < player.playerWarehouseStorageItems && ScriptableItem.dict.TryGetValue(itemName.GetStableHashCode(), out template)) {
					Item item 					= new Item(template);
					int amount 					= Convert.ToInt32(row[2]);
					item.summonedHealth 		= Convert.ToInt32(row[3]);
					item.summonedLevel 			= Convert.ToInt32(row[4]);
					item.summonedExperience 	= Convert.ToInt32(row[5]);
					player.UCE_playerWarehouse[slot] = new ItemSlot(item, amount);
				}
			}
		}

#elif _SQLITE

        var warehouseData = ExecuteReader("SELECT gold, level FROM uce_warehouse WHERE character=@character", new SqliteParameter("@character", player.name));

        if (warehouseData.Count == 1)
        {
            player.playerWarehouseGold = (long)warehouseData[0][0];
            player.playerWarehouseLevel = Convert.ToInt32(warehouseData[0][1]);
        }
        else
        {
            ExecuteNonQuery("INSERT INTO uce_warehouse (character, gold, level) VALUES(@character, 0, 0)", new SqliteParameter("@character", player.name));
            player.playerWarehouseGold = 0;
            player.playerWarehouseLevel = 0;
        }

        for (int i = 0; i < player.playerWarehouseStorageItems; ++i)
        {
            player.UCE_playerWarehouse.Add(new ItemSlot());
        }

        List<List<object>> table = ExecuteReader("SELECT name, slot, amount, summonedHealth, summonedLevel, summonedExperience FROM uce_warehouse_items WHERE character=@character", new SqliteParameter("@character", player.name));
        if (table.Count > 0)
        {
            foreach (List<object> row in table)
            {
                string itemName = (string)row[0];
                int slot = Convert.ToInt32((long)row[1]);
                ScriptableItem template;

                if (slot < player.playerWarehouseStorageItems && ScriptableItem.dict.TryGetValue(itemName.GetStableHashCode(), out template))
                {
                    Item item = new Item(template);
                    int amount = Convert.ToInt32((long)row[2]);
                    item.summonedHealth = Convert.ToInt32((long)row[3]);
                    item.summonedLevel = Convert.ToInt32((long)row[4]);
                    item.summonedExperience = (long)row[5];
                    player.UCE_playerWarehouse[slot] = new ItemSlot(item, amount);
                }
            }
        }

#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_Warehouse
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    public void CharacterSave_UCE_Warehouse(Player player)
    {
#if _MYSQL

		var warehouseGoldEntryExists = ExecuteReaderMySql("SELECT 1 FROM uce_warehouse WHERE `character`=@character", new MySqlParameter("@character", player.name));

		if (warehouseGoldEntryExists.Count == 1) {
			if (player.warehouseActionDone) {
				ExecuteNonQueryMySql("UPDATE uce_warehouse SET gold=@gold, level=@level WHERE `character`=@character",
					new MySqlParameter("@gold", 		player.playerWarehouseGold),
					new MySqlParameter("@level", 		player.playerWarehouseLevel),
					new MySqlParameter("@character", 	player.name));
			}
		}

		if (player.warehouseActionDone) {
			ExecuteNonQueryMySql("DELETE FROM uce_warehouse_items WHERE `character`=@character", new MySqlParameter("@character", player.name));

			for (int i = 0; i < player.UCE_playerWarehouse.Count; ++i) {
				ItemSlot slot = player.UCE_playerWarehouse[i];

				if (slot.amount > 0) {
					ExecuteNonQueryMySql("INSERT INTO uce_warehouse_items VALUES (@character, @slot, @name, @amount, @petHealth, @petLevel, @petExperience)",
									new MySqlParameter("@character", player.name),
									new MySqlParameter("@slot", i),
									new MySqlParameter("@name", slot.item.name),
									new MySqlParameter("@amount", slot.amount),
									new MySqlParameter("@petHealth", slot.item.summonedHealth),
									new MySqlParameter("@petLevel", slot.item.summonedLevel),
									new MySqlParameter("@petExperience", slot.item.summonedExperience));
				}
			}
		}

#elif _SQLITE

        var warehouseGoldEntryExists = ExecuteReader("SELECT 1 FROM uce_warehouse WHERE `character`=@character", new SqliteParameter("@character", player.name));
        if (warehouseGoldEntryExists.Count == 1)
        {
            if (player.warehouseActionDone)
            {
                ExecuteNonQuery("UPDATE uce_warehouse SET gold=@gold, level=@level WHERE `character`=@character",
                new SqliteParameter("@gold", player.playerWarehouseGold),
                new SqliteParameter("@level", player.playerWarehouseLevel),
                new SqliteParameter("@character", player.name));
            }
        }

        if (player.warehouseActionDone)
        {
            ExecuteNonQuery("DELETE FROM uce_warehouse_items WHERE `character`=@character", new SqliteParameter("@character", player.name));

            for (int i = 0; i < player.UCE_playerWarehouse.Count; ++i)
            {
                ItemSlot slot = player.UCE_playerWarehouse[i];

                if (slot.amount > 0)
                {
                    ExecuteNonQuery("INSERT INTO uce_warehouse_items VALUES (@character, @slot, @name, @amount, @petHealth, @petLevel, @petExperience)",
                                    new SqliteParameter("@character", player.name),
                                    new SqliteParameter("@slot", i),
                                    new SqliteParameter("@name", slot.item.name),
                                    new SqliteParameter("@amount", slot.amount),
                                    new SqliteParameter("@petHealth", slot.item.summonedHealth),
                                    new SqliteParameter("@petLevel", slot.item.summonedLevel),
                                    new SqliteParameter("@petExperience", slot.item.summonedExperience));
                }
            }
        }

#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================