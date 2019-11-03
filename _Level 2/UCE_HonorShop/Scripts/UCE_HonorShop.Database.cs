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
    // Connect_UCE_HonorShop
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_HonorShop()
    {
#if _MYSQL
		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS character_currencies (
			`character` VARCHAR(32) NOT NULL,
			currency VARCHAR(32) NOT NULL,
			amount INTEGER(16) NOT NULL,
			total INTEGER(16) NOT NULL
		    )CHARACTER SET=utf8mb4");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS character_currencies (
			character TEXT NOT NULL,
			currency TEXT NOT NULL,
			amount INTEGER NOT NULL,
			total INTEGER NOT NULL
		)");
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_UCE_HonorShop
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    private void CharacterLoad_UCE_HonorShop(Player player)
    {
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT currency, amount, total FROM character_currencies WHERE `character`=@name", new MySqlParameter("@name", player.name));
        foreach (var row in table)
        {
            string tmplName = (string)row[0];
            UCE_Tmpl_HonorCurrency tmplCurrency;

            if (UCE_Tmpl_HonorCurrency.dict.TryGetValue(tmplName.GetStableHashCode(), out tmplCurrency))
            {
                UCE_HonorShopCurrency hsc = new UCE_HonorShopCurrency();
                hsc.honorCurrency = tmplCurrency;
                hsc.amount = (int)row[1];
                hsc.total = (int)row[2];
                player.UCE_currencies.Add(hsc);
            }
        }
#elif _SQLITE
        var table = ExecuteReader("SELECT currency, amount, total FROM character_currencies WHERE `character`=@name", new SqliteParameter("@name", player.name));
        foreach (var row in table)
        {
            string tmplName = (string)row[0];
            UCE_Tmpl_HonorCurrency tmplCurrency;

            if (UCE_Tmpl_HonorCurrency.dict.TryGetValue(tmplName.GetStableHashCode(), out tmplCurrency))
            {
                UCE_HonorShopCurrency hsc = new UCE_HonorShopCurrency();
                hsc.honorCurrency = tmplCurrency;
                hsc.amount = (long)row[1];
                hsc.total = (long)row[2];
                player.UCE_currencies.Add(hsc);
            }
        }
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_HonorShop
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    private void CharacterSave_UCE_HonorShop(Player player)
    {
#if _MYSQL
		ExecuteNonQueryMySql("DELETE FROM character_currencies WHERE `character`=@character", new MySqlParameter("@character", player.name));
        for (int i = 0; i < player.UCE_currencies.Count; ++i)
        {
            ExecuteNonQueryMySql("INSERT INTO character_currencies VALUES (@character, @currency, @amount, @total)",
                 new MySqlParameter("@character", player.name),
                 new MySqlParameter("@currency", player.UCE_currencies[i].honorCurrency.name),
                 new MySqlParameter("@amount", player.UCE_currencies[i].amount),
                 new MySqlParameter("@total", player.UCE_currencies[i].total)
                 );
        }
#elif _SQLITE
        ExecuteNonQuery("DELETE FROM character_currencies WHERE `character`=@character", new SqliteParameter("@character", player.name));
        for (int i = 0; i < player.UCE_currencies.Count; ++i)
        {
            ExecuteNonQuery("INSERT INTO character_currencies VALUES (@character, @currency, @amount, @total)",
                 new SqliteParameter("@character", player.name),
                 new SqliteParameter("@currency", player.UCE_currencies[i].honorCurrency.name),
                 new SqliteParameter("@amount", player.UCE_currencies[i].amount),
                 new SqliteParameter("@total", player.UCE_currencies[i].total)
                 );
        }
#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================