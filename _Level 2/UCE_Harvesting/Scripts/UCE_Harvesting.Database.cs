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

#if _iMMOHARVESTING

// =======================================================================================
// DATABASE (SQLite / mySQL Hybrid)
// =======================================================================================
public partial class Database
{
    // -----------------------------------------------------------------------------------
    // Connect_UCE_Harvesting
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_Harvesting()
    {
#if _MYSQL
		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS character_professions (
                    `character` VARCHAR(32) NOT NULL,
                    profession VARCHAR(32) NOT NULL,
                    experience BIGINT,
                     PRIMARY KEY(`character`, profession)
                    ) CHARACTER SET=utf8mb4");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS character_professions ( character TEXT NOT NULL, profession TEXT NOT NULL, experience INTEGER)");
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_UCE_Harvesting
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    public void CharacterLoad_UCE_Harvesting(Player player)
    {
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT profession, experience FROM character_professions WHERE `character`=@character", new MySqlParameter("@character", player.name));
        foreach (var row in table)
        {
            UCE_HarvestingProfession profession = new UCE_HarvestingProfession((string)row[0]);
            profession.experience = (long)row[1];
            player.UCE_Professions.Add(profession);
        }
#elif _SQLITE
        var table = ExecuteReader("SELECT profession, experience FROM character_professions WHERE character=@character", new SqliteParameter("@character", player.name));
        foreach (var row in table)
        {
            UCE_HarvestingProfession profession = new UCE_HarvestingProfession((string)row[0]);
            profession.experience = (long)row[1];
            player.UCE_Professions.Add(profession);
        }
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_Harvesting
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    public void CharacterSave_UCE_Harvesting(Player player)
    {
#if _MYSQL
		var query2 = @"
            INSERT INTO character_professions
            SET
            `character`=@character,
            profession=@profession,
            experience = @experience

            ON DUPLICATE KEY UPDATE
            `character`=@character,
            profession=@profession,
            experience = @experience
            ";

        foreach (var profession in player.UCE_Professions)
            ExecuteNonQueryMySql(query2,
           new MySqlParameter("@character", player.name),
           new MySqlParameter("@profession", profession.templateName),
           new MySqlParameter("@experience", profession.experience));
#elif _SQLITE
        ExecuteNonQuery("DELETE FROM character_professions WHERE character=@character", new SqliteParameter("@character", player.name));
        foreach (var profession in player.UCE_Professions)
            ExecuteNonQuery("INSERT INTO character_professions VALUES (@character, @profession, @experience)",
                            new SqliteParameter("@character", player.name),
                            new SqliteParameter("@profession", profession.templateName),
                            new SqliteParameter("@experience", profession.experience));
#endif
    }

    // -----------------------------------------------------------------------------------
}

#endif

// =======================================================================================