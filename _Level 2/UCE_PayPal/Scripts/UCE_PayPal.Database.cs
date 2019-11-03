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
    // Connect_UCE_PayPal
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    public void Connect_UCE_PayPal()
    {
#if _MYSQL
		singleton.ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS character_purchases (
			`character` VARCHAR(16) NOT NULL,
			product VARCHAR(32) NOT NULL,
			purchased VARCHAR(32) NOT NULL,
			counter INTEGER(4) NOT NULL
		)");
#elif _SQLITE
        singleton.ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS character_purchases (
			`character` TEXT NOT NULL,
			product TEXT NOT NULL,
			purchased TEXT NOT NULL,
			counter INTEGER NOT NULL
		)");
#endif
    }

    // -----------------------------------------------------------------------------------
    // UCE_loadCharacterPurchase
    // -----------------------------------------------------------------------------------
    public int UCE_loadCharacterPurchase(string name, string product)
    {
        int counter = 0;

        if (UCE_hasCharacterPurchased(name, product))
        {
#if _MYSQL
			counter = Convert.ToInt32((long)singleton.ExecuteScalarMySql("SELECT counter FROM character_purchases WHERE `character`=@name AND `product`=@product",
							new MySqlParameter("@name", name),
							new MySqlParameter("@product", product)));
#elif _SQLITE
            counter = Convert.ToInt32((long)singleton.ExecuteScalar("SELECT counter FROM character_purchases WHERE `character`=@name AND `product`=@product",
                            new SqliteParameter("@name", name),
                            new SqliteParameter("@product", product)));
#endif
        }

        return counter;
    }

    // -----------------------------------------------------------------------------------
    // UCE_loadCharacterPurchase
    // -----------------------------------------------------------------------------------
    public bool UCE_hasCharacterPurchased(string name, string product)
    {
#if _MYSQL
		return ((long)singleton.ExecuteScalarMySql("SELECT Count(*) FROM character_purchases WHERE `character`=@name AND `product`=@product",
							new MySqlParameter("@name", name),
							new MySqlParameter("@product", product)))
							==1;
#elif _SQLITE
        return ((long)singleton.ExecuteScalar("SELECT Count(*) FROM character_purchases WHERE `character`=@name AND `product`=@product",
                            new SqliteParameter("@name", name),
                            new SqliteParameter("@product", product)))
                            == 1;
#endif
    }

    // -----------------------------------------------------------------------------------
    // UCE_saveCharacterPurchase
    // -----------------------------------------------------------------------------------
    public void UCE_saveCharacterPurchase(string name, UCE_Tmpl_PayPalProduct product, string purchased)
    {
        int counter = UCE_loadCharacterPurchase(name, product.name);

        counter++;
#if _MYSQL
		singleton.ExecuteNonQueryMySql("DELETE FROM character_purchases WHERE `character`=@name AND `product`=@product",
        				new MySqlParameter("@name", name),
						new MySqlParameter("@product", product.name));

        singleton.ExecuteNonQueryMySql("INSERT INTO character_purchases VALUES (@character, @product, @purchased, @counter)",
                        new MySqlParameter("@character", name),
                        new MySqlParameter("@product", product.name),
                        new MySqlParameter("@purchased", purchased),
                        new MySqlParameter("@counter", counter)
                        );
#elif _SQLITE
        singleton.ExecuteNonQuery("DELETE FROM character_purchases WHERE `character`=@name AND `product`=@product",
                        new SqliteParameter("@name", name),
                        new SqliteParameter("@product", product.name));

        singleton.ExecuteNonQuery("INSERT INTO character_purchases VALUES (@character, @product, @purchased, @counter)",
                        new SqliteParameter("@character", name),
                        new SqliteParameter("@product", product.name),
                        new SqliteParameter("@purchased", purchased),
                        new SqliteParameter("@counter", counter)
                        );
#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================