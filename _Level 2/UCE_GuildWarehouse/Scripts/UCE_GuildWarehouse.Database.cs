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
    // Connect_UCE_GuildWareHouse
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_GuildWareHouse()
    {
#if _MYSQL

		ExecuteNonQueryMySql(@"
            CREATE TABLE IF NOT EXISTS UCE_guild_warehouse(
			        guild VARCHAR(32) NOT NULL,
					gold INTEGER(16) NOT NULL DEFAULT 0,
					level INTEGER(16) NOT NULL DEFAULT 0,
					locked INTEGER(1) NOT NULL DEFAULT 0,
					busy INTEGER(1) NOT NULL DEFAULT 0,
                    PRIMARY KEY(guild)
                  ) CHARACTER SET=utf8mb4");

        ExecuteNonQueryMySql(@"
           CREATE TABLE IF NOT EXISTS UCE_guild_warehouse_items(
                    guild VARCHAR(32) NOT NULL,
                    slot INTEGER(16) NOT NULL,
                    name TEXT(16) NOT NULL,
                    amount INTEGER(16) NOT NULL,
                    summonedHealth INTEGER NOT NULL,
                    summonedLevel INTEGER NOT NULL,
                    summonedExperience INTEGER NOT NULL
                  ) CHARACTER SET=utf8mb4");

#elif _SQLITE

        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS 'UCE_guild_warehouse' (
							'guild' TEXT NOT NULL PRIMARY KEY,
							'gold' INTEGER NOT NULL DEFAULT 0,
							'level' INTEGER NOT NULL DEFAULT 0,
							'locked' INTEGER NOT NULL DEFAULT 0,
							'busy' INTEGER NOT NULL DEFAULT 0)");

        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS 'UCE_guild_warehouse_items' (
                           'guild' TEXT NOT NULL,
                           'slot' INTEGER NOT NULL,
                           'name' TEXT NOT NULL,
                           'amount' INTEGER NOT NULL,
                           'summonedHealth' INTEGER NOT NULL,
                           'summonedLevel' INTEGER NOT NULL,
                           'summonedExperience' INTEGER NOT NULL)");
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_GuildWarehouse
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    private void CharacterSave_UCE_GuildWarehouse(Player player)
    {
        UCE_SaveGuildWarehouse(player);
    }

    // -----------------------------------------------------------------------------------
    // UCE_LoadGuildWarehouse
    // -----------------------------------------------------------------------------------
    public void UCE_LoadGuildWarehouse(Player player)
    {
        player.resetGuildWarehouse();
        if (!player.InGuild()) return;

#if _MYSQL

		var UCE_warehouseData = ExecuteReaderMySql("SELECT gold, level, locked FROM UCE_guild_warehouse WHERE guild=@guild", new MySqlParameter("@guild", player.guild.name));

        // -- exists already? load to player and set busy

        if (UCE_warehouseData.Count == 1)
        {
            player.guildWarehouseGold 	= Convert.ToInt32(UCE_warehouseData[0][0]);
            player.guildWarehouseLevel	= Convert.ToInt32(UCE_warehouseData[0][1]);
            player.guildWarehouseLock	= (Convert.ToInt32(UCE_warehouseData[0][2]) == 1) ? true : false;
            ExecuteNonQueryMySql("UPDATE UCE_guild_warehouse SET busy=1 WHERE guild=@guild", new MySqlParameter("@guild", 	player.guild.name));
        }
        else
        {
        	// -- does not exist? create new and set busy

            ExecuteNonQueryMySql("INSERT INTO UCE_guild_warehouse (guild, gold, level, locked, busy) VALUES(@guild, 0, 0, 0, 1)", new MySqlParameter("@guild", player.guild.name));
            player.guildWarehouseGold 	= 0;
            player.guildWarehouseLevel 	= 0;
            player.guildWarehouseLock	= false;
        }

        for (int i = 0; i < player.guildWarehouseStorageItems; ++i) {
			player.UCE_guildWarehouse.Add(new ItemSlot());
		}

		var table = ExecuteReaderMySql("SELECT name, slot, amount, summonedHealth, summonedLevel, summonedExperience FROM UCE_guild_warehouse_items WHERE guild=@guild", new MySqlParameter("@guild", player.guild.name));
        if (table.Count > 0)
        {
            foreach (var row in table)
            {
                string itemName 	= (string)row[0];
                int slot 			= Convert.ToInt32(row[1]);
                ScriptableItem template;
                if (slot < player.guildWarehouseStorageItems && ScriptableItem.dict.TryGetValue(itemName.GetStableHashCode(), out template))
                {
                    Item item 					= new Item(template);
                    int amount 					= Convert.ToInt32(row[2]);
                    item.summonedHealth 		= Convert.ToInt32(row[3]);
                    item.summonedLevel 			= Convert.ToInt32(row[4]);
                    item.summonedExperience 	= Convert.ToInt32(row[5]);
                    player.UCE_guildWarehouse[slot] = new ItemSlot(item, amount);
                }
            }
        }

#elif _SQLITE

        var UCE_warehouseData = ExecuteReader("SELECT gold, level, locked FROM UCE_guild_warehouse WHERE guild=@guild", new SqliteParameter("@guild", player.guild.name));

        // -- exists already? load to player and set busy

        if (UCE_warehouseData.Count == 1)
        {
            player.guildWarehouseGold = (long)UCE_warehouseData[0][0];
            player.guildWarehouseLevel = Convert.ToInt32((long)UCE_warehouseData[0][1]);
            player.guildWarehouseLock = (Convert.ToInt32((long)UCE_warehouseData[0][2]) == 1) ? true : false;
            ExecuteNonQuery("UPDATE UCE_guild_warehouse SET busy=1 WHERE guild=@guild", new SqliteParameter("@guild", player.guild.name));
        }
        else
        {
            // -- does not exist? create new and set busy

            ExecuteNonQuery("INSERT INTO UCE_guild_warehouse (guild, gold, level, locked, busy) VALUES(@guild, 0, 0, 0, 1)", new SqliteParameter("@guild", player.guild.name));
            player.guildWarehouseGold = 0;
            player.guildWarehouseLevel = 0;
            player.guildWarehouseLock = false;
        }

        for (int i = 0; i < player.guildWarehouseStorageItems; ++i)
        {
            player.UCE_guildWarehouse.Add(new ItemSlot());
        }

        var table = ExecuteReader("SELECT name, slot, amount, summonedHealth, summonedLevel, summonedExperience FROM UCE_guild_warehouse_items WHERE guild=@guild", new SqliteParameter("@guild", player.guild.name));

        if (table.Count > 0)
        {
            foreach (var row in table)
            {
                string itemName = (string)row[0];
                int slot = Convert.ToInt32((long)row[1]);
                ScriptableItem template;

                if (slot < player.guildWarehouseStorageItems && ScriptableItem.dict.TryGetValue(itemName.GetStableHashCode(), out template))
                {
                    Item item = new Item(template);
                    int amount = Convert.ToInt32((long)row[2]);
                    item.summonedHealth = Convert.ToInt32((long)row[3]);
                    item.summonedLevel = Convert.ToInt32((long)row[4]);
                    item.summonedExperience = (long)row[5];
                    player.UCE_guildWarehouse[slot] = new ItemSlot(item, amount);
                }
            }
        }

#endif

        player.guildWarehouseActionDone = false;
    }

    // -----------------------------------------------------------------------------------
    // UCE_SaveGuildWarehouse
    // -----------------------------------------------------------------------------------
    public void UCE_SaveGuildWarehouse(Player player)
    {
        if (!player.InGuild()) player.resetGuildWarehouse();
        if (!player.InGuild() || !player.guildWarehouseActionDone) return;

#if _MYSQL

		long EntryExistsOrBusy = Convert.ToInt32(ExecuteScalarMySql("SELECT busy FROM UCE_guild_warehouse WHERE guild=@guild", new MySqlParameter("@guild", player.guild.name)));

		// -- check if exists, only delete entries when it does and is not busy

		if (EntryExistsOrBusy != 1)
            ExecuteNonQueryMySql("DELETE FROM UCE_guild_warehouse_items WHERE guild=@guild", new MySqlParameter("@guild", player.guild.name));

		for (int i = 0; i < player.UCE_guildWarehouse.Count; ++i) {
			ItemSlot slot = player.UCE_guildWarehouse[i];

			if (slot.amount > 0) {
				ExecuteNonQueryMySql("INSERT INTO UCE_guild_warehouse_items VALUES (@guild, @slot, @name, @amount, @summonedHealth, @summonedLevel, @summonedExperience)",
								new MySqlParameter("@guild", 				player.guild.name),
								new MySqlParameter("@slot", 				i),
								new MySqlParameter("@name", 				slot.item.name),
								new MySqlParameter("@amount", 				slot.amount),
								new MySqlParameter("@summonedHealth", 		slot.item.summonedHealth),
								new MySqlParameter("@summonedLevel", 		slot.item.summonedLevel),
								new MySqlParameter("@summonedExperience", 	slot.item.summonedExperience));
			}
		}

		ExecuteNonQueryMySql("UPDATE UCE_guild_warehouse SET gold=@gold, level=@level, locked=@locked, busy=0 WHERE guild=@guild",
			new MySqlParameter("@gold", 	player.guildWarehouseGold),
			new MySqlParameter("@level", 	player.guildWarehouseLevel),
			new MySqlParameter("@locked", 	(player.guildWarehouseLock) ? 1 : 0),
			new MySqlParameter("@guild", 	player.guild.name));

#elif _SQLITE

        long EntryExistsOrBusy = (long)ExecuteScalar("SELECT busy FROM UCE_guild_warehouse WHERE guild=@guild", new SqliteParameter("@guild", player.guild.name));

        // -- check if exists, only delete entries when it does and is not busy

        if (EntryExistsOrBusy != 1)
            ExecuteNonQuery("DELETE FROM UCE_guild_warehouse_items WHERE guild=@guild", new SqliteParameter("@guild", player.guild.name));

        for (int i = 0; i < player.UCE_guildWarehouse.Count; ++i)
        {
            ItemSlot slot = player.UCE_guildWarehouse[i];

            if (slot.amount > 0)
            {
                ExecuteNonQuery("INSERT INTO UCE_guild_warehouse_items VALUES (@guild, @slot, @name, @amount, @summonedHealth, @summonedLevel, @summonedExperience)",
                                new SqliteParameter("@guild", player.guild.name),
                                new SqliteParameter("@slot", i),
                                new SqliteParameter("@name", slot.item.name),
                                new SqliteParameter("@amount", slot.amount),
                                new SqliteParameter("@summonedHealth", slot.item.summonedHealth),
                                new SqliteParameter("@summonedLevel", slot.item.summonedLevel),
                                new SqliteParameter("@summonedExperience", slot.item.summonedExperience));
            }
        }

        ExecuteNonQuery("UPDATE UCE_guild_warehouse SET gold=@gold, level=@level, locked=@locked, busy=0 WHERE guild=@guild",
            new SqliteParameter("@gold", player.guildWarehouseGold),
            new SqliteParameter("@level", player.guildWarehouseLevel),
            new SqliteParameter("@locked", (player.guildWarehouseLock) ? 1 : 0),
            new SqliteParameter("@guild", player.guild.name));

#endif
    }

    // -----------------------------------------------------------------------------------
    // UCE_SetGuildWarehouseBusy
    // -----------------------------------------------------------------------------------
    public void UCE_SetGuildWarehouseBusy(Player player, int isbusy = 1)
    {
#if _MYSQL
		ExecuteNonQueryMySql("UPDATE UCE_guild_warehouse SET busy=@busy WHERE guild=@guild", new MySqlParameter("@guild", player.guild.name), new MySqlParameter("@busy", 	isbusy));
#elif _SQLITE
        ExecuteNonQuery("UPDATE UCE_guild_warehouse SET busy=@busy WHERE guild=@guild", new SqliteParameter("@guild", player.guild.name), new SqliteParameter("@busy", isbusy));
#endif
    }

    // -----------------------------------------------------------------------------------
    // UCE_GetGuildWarehouseAccess
    // -----------------------------------------------------------------------------------
    public bool UCE_GetGuildWarehouseAccess(Player player)
    {
#if _MYSQL
		return Convert.ToInt32(ExecuteScalarMySql("SELECT busy FROM UCE_guild_warehouse WHERE guild=@guild", new MySqlParameter("@guild", player.guild.name))) != 1;
#elif _SQLITE
        return Convert.ToInt32(ExecuteScalar("SELECT busy FROM UCE_guild_warehouse WHERE guild=@guild", new SqliteParameter("@guild", player.guild.name))) != 1;
#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================