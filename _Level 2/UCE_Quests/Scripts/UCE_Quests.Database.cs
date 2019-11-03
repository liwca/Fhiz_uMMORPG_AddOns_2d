// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using System;
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
    // Connect_UCE_Quest
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_Quest()
    {
#if _MYSQL
		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS character_UCE_quests (
                            `character` VARCHAR(32) NOT NULL,
                            name VARCHAR(111) NOT NULL,
                            killed VARCHAR(111) NOT NULL,
                            gathered VARCHAR(111) NOT NULL,
                            harvested VARCHAR(111) NOT NULL,
                            visited VARCHAR(111) NOT NULL,
                            crafted VARCHAR(111) NOT NULL,
                            looted VARCHAR(111) NOT NULL,
                            completed INTEGER(16) NOT NULL,
                            completedAgain INTEGER(16) NOT NULL,
                            lastCompleted VARCHAR(111) NOT NULL,
                            counter INTEGER(16) NOT NULL,
                                PRIMARY KEY(`character`, name)
                            ) CHARACTER SET=utf8mb4");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS character_UCE_quests (
                            character TEXT NOT NULL,
                            name TEXT NOT NULL,
                            pvped TEXT NOT NULL,
                            killed TEXT NOT NULL,
                            gathered TEXT NOT NULL,
                            harvested TEXT NOT NULL,
                            visited TEXT NOT NULL,
                            crafted TEXT NOT NULL,
                            looted TEXT NOT NULL,
                            completed INTEGER NOT NULL,
                            completedAgain INTEGER NOT NULL,
                            lastCompleted TEXT NOT NULL,
                            counter INTEGER NOT NULL,
                            PRIMARY KEY(character, name))");
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_UCE_Quest
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    private void CharacterLoad_UCE_Quest(Player player)
    {
#if _MYSQL
		List< List<object> > table = ExecuteReaderMySql("SELECT name, killed, gathered, harvested, visited, crafted, looted, completed, completedAgain, lastCompleted, counter FROM character_UCE_quests WHERE `character`=@character",
        					new MySqlParameter("@character", player.name));
        foreach (List<object> row in table)
        {
            string questName = (string)row[0];
            UCE_ScriptableQuest questData;
            if (UCE_ScriptableQuest.dict.TryGetValue(questName.GetStableHashCode(), out questData))
            {
                UCE_Quest quest 		= new UCE_Quest(questData);

                quest.killedTarget 		= UCE_Tools.IntStringToArray((string)row[1]);
                quest.gatheredTarget 	= UCE_Tools.IntStringToArray((string)row[2]);
                quest.harvestedTarget 	= UCE_Tools.IntStringToArray((string)row[3]);
                quest.visitedTarget 	= UCE_Tools.IntStringToArray((string)row[4]);
                quest.craftedTarget 	= UCE_Tools.IntStringToArray((string)row[5]);
                quest.lootedTarget 		= UCE_Tools.IntStringToArray((string)row[6]);

                foreach (int i in quest.visitedTarget)
                	if (i != 0) quest.visitedCount++;

                quest.completed 		= ((int)row[7]) != 0; // sqlite has no bool
                quest.completedAgain 	= ((int)row[8]) != 0; // sqlite has no bool
        		quest.lastCompleted 	= (string)row[9];
                quest.counter			= (int)row[10];
                player.UCE_quests.Add(quest);
            }
        }

#elif _SQLITE
        List<List<object>> table = ExecuteReader("SELECT name, pvped, killed, gathered, harvested, visited, crafted, looted, completed, completedAgain, lastCompleted, counter FROM character_UCE_quests WHERE character=@character",
                            new SqliteParameter("@character", player.name));
        foreach (List<object> row in table)
        {
            string questName = (string)row[0];
            UCE_ScriptableQuest questData;
            if (UCE_ScriptableQuest.dict.TryGetValue(questName.GetStableHashCode(), out questData))
            {
                UCE_Quest quest = new UCE_Quest(questData);

                quest.pvpedTarget = UCE_Tools.IntStringToArray((string)row[1]);
                quest.killedTarget = UCE_Tools.IntStringToArray((string)row[2]);
                quest.gatheredTarget = UCE_Tools.IntStringToArray((string)row[3]);
                quest.harvestedTarget = UCE_Tools.IntStringToArray((string)row[4]);
                quest.visitedTarget = UCE_Tools.IntStringToArray((string)row[5]);
                quest.craftedTarget = UCE_Tools.IntStringToArray((string)row[6]);
                quest.lootedTarget = UCE_Tools.IntStringToArray((string)row[7]);

                foreach (int i in quest.visitedTarget)
                    if (i != 0) quest.visitedCount++;

                quest.completed = ((long)row[8]) != 0; // sqlite has no bool
                quest.completedAgain = ((long)row[9]) != 0; // sqlite has no bool
                quest.lastCompleted = (string)row[10];
                quest.counter = Convert.ToInt32((long)row[11]);
                player.UCE_quests.Add(quest);
            }
        }
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_Quest
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    private void CharacterSave_UCE_Quest(Player player)
    {
#if _MYSQL
		var query2 = @"
            INSERT INTO character_UCE_quests
            SET
            `character`=@character,
            name=@name,
            killed=@killed,
            gathered=@gathered,
            harvested=@harvested,
            visited=@visited,
            crafted=@crafted,
            looted=@looted,
            completed=@completed,
            completedAgain=@completedAgain,
            lastCompleted=@lastCompleted,
            counter=@counter
            ON DUPLICATE KEY UPDATE
            name=@name,
            killed=@killed,
            gathered=@gathered,
            harvested=@harvested,
            visited=@visited,
            crafted=@crafted,
            looted=@looted,
            completed=@completed,
            completedAgain=@completedAgain,
            lastCompleted=@lastCompleted,
            counter=@counter
            ";

        foreach (UCE_Quest quest in player.UCE_quests)
            ExecuteNonQueryMySql(query2,
                            new MySqlParameter("@character", player.name),
                            new MySqlParameter("@name", quest.name),
                            new MySqlParameter("@killed", UCE_Tools.IntArrayToString(quest.killedTarget)),
                            new MySqlParameter("@gathered", UCE_Tools.IntArrayToString(quest.gatheredTarget)),
                            new MySqlParameter("@harvested", UCE_Tools.IntArrayToString(quest.harvestedTarget)),
                            new MySqlParameter("@visited", UCE_Tools.IntArrayToString(quest.visitedTarget)),
                            new MySqlParameter("@crafted", UCE_Tools.IntArrayToString(quest.craftedTarget)),
                            new MySqlParameter("@looted", UCE_Tools.IntArrayToString(quest.lootedTarget)),
                            new MySqlParameter("@completed", Convert.ToInt32(quest.completed)),
                            new MySqlParameter("@completedAgain", Convert.ToInt16(quest.completedAgain)),
                            new MySqlParameter("@lastCompleted", quest.lastCompleted),
                            new MySqlParameter("@counter", quest.counter)
                            );

#elif _SQLITE
        // quests: remove old entries first, then add all new ones
        ExecuteNonQuery("DELETE FROM character_UCE_quests WHERE character=@character",
                        new SqliteParameter("@character", player.name));
        foreach (UCE_Quest quest in player.UCE_quests)
            ExecuteNonQuery("INSERT INTO character_UCE_quests VALUES (@character, @name, @pvped, @killed, @gathered, @harvested, @visited, @crafted, @looted, @completed, @completedAgain, @lastCompleted, @counter)",
                            new SqliteParameter("@character", player.name),
                            new SqliteParameter("@name", quest.name),
                            new SqliteParameter("@pvped", UCE_Tools.IntArrayToString(quest.killedTarget)),
                            new SqliteParameter("@killed", UCE_Tools.IntArrayToString(quest.killedTarget)),
                            new SqliteParameter("@gathered", UCE_Tools.IntArrayToString(quest.gatheredTarget)),
                            new SqliteParameter("@harvested", UCE_Tools.IntArrayToString(quest.harvestedTarget)),
                            new SqliteParameter("@visited", UCE_Tools.IntArrayToString(quest.visitedTarget)),
                            new SqliteParameter("@crafted", UCE_Tools.IntArrayToString(quest.craftedTarget)),
                            new SqliteParameter("@looted", UCE_Tools.IntArrayToString(quest.lootedTarget)),
                            new SqliteParameter("@completed", Convert.ToInt32(quest.completed)),
                            new SqliteParameter("@completedAgain", Convert.ToInt32(quest.completedAgain)),
                            new SqliteParameter("@lastCompleted", quest.lastCompleted),
                            new SqliteParameter("@counter", quest.counter)
                            );
#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================