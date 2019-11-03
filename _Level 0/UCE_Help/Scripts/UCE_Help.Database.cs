using UnityEngine;
using System.IO;

#if _MYSQL
using MySql.Data;								// From MySql.Data.dll in Plugins folder
using MySql.Data.MySqlClient;                   // From MySql.Data.dll in Plugins folder

#elif _SQLITE

using Mono.Data.Sqlite;                       // copied from Unity/Mono/lib/mono/2.0 to Plugins

#endif

// =======================================================================================
// DATABASE (SQLite / mySQL Hybrid)
// =======================================================================================

public partial class Database
{
    #region Functions

    // -----------------------------------------------------------------------------------
    // Connect_Reports
    // Sets up the database to accept reports.
    // Create the reports table if it wasn't there for any reason.
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_Reports()
    {
#if _MYSQL

		ExecuteNonQueryMySql(@"
							CREATE TABLE IF NOT EXISTS UCE_reports(
                           	senderAcc VARCHAR(32) NOT NULL,
                            senderCharacter VARCHAR(32) NOT NULL,
                           	readBefore INTEGER(16) NOT NULL,
                           	title VARCHAR(32) NOT NULL,
                           	message VARCHAR(512) NOT NULL,
                           	solved INTEGER(16) NOT NULL,
                           	time VARCHAR(128) NOT NULL,
                           	position VARCHAR(256) NOT NULL
                  			) CHARACTER SET=utf8mb4");

#elif _SQLITE

        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS 'UCE_reports' (
                           'senderAcc' TEXT NOT NULL,
                           'senderCharacter' TEXT NOT NULL,
                           'readBefore' INTEGER NOT NULL,
                           'title' TEXT NOT NULL,
                           'message' TEXT NOT NULL,
                           'solved' INTEGER NOT NULL,
                           'time' TEXT NOT NULL,
                           'position' TEXT NOT NULL)");

#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_Reports
    // Loads the reports when they're called. Currently not used by anything, here for future addon.
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    private void CharacterLoad_Reports(Player player)
    {
#if _MYSQL

		var table = ExecuteReaderMySql("SELECT senderCharacter, readBefore, title, message, solved, time, position FROM UCE_reports WHERE senderAcc=@senderAcc;", new MySqlParameter("@senderAcc", player.account));
        if (table.Count > 0)                                //If the table has anything then continue.
        {
            for (int i = 0; i < table.Count; i++)           //Loop through the table to gather the information.
            {
                var row = table[i];                         //Grab each row from the table.
                var report = new UCE_HelpMember();          //Make the report.
                report.senderAcc = player.account;          //Set the account information.
                report.senderCharacter = player.name;       //Set the character name of sender.
                report.readBefore = ((int)row[1]) != 0;     //Set the read option.
                report.title = (string)row[2];              //Set the title.
                report.message = (string)row[3];            //Set the details message.
                report.solved = ((int)row[4]) != 0;         //Set the solved or not option.
                report.time = (string)row[5];               //Set the time and date.
                report.position = (string)row[6];           //Set the position the player was on the map.
                player.reports.Add(report);                 //Add the report to a list to pull with other addon.
            }
        }

#elif _SQLITE

        var table = ExecuteReader("SELECT senderCharacter, readBefore, title, message, solved, time, position FROM 'UCE_reports' WHERE senderAcc=@senderAcc;", new SqliteParameter("@senderAcc", player.account));
        if (table.Count > 0)                                //If the table has anything then continue.
        {
            for (int i = 0; i < table.Count; i++)           //Loop through the table to gather the information.
            {
                var row = table[i];                         //Grab each row from the table.
                var report = new UCE_HelpMember();          //Make the report.
                report.senderAcc = player.account;          //Set the account information.
                report.senderCharacter = player.name;       //Set the character name of sender.
                report.readBefore = ((long)row[1]) != 0;    //Set the read option.
                report.title = (string)row[2];              //Set the title.
                report.message = (string)row[3];            //Set the details message.
                report.solved = ((long)row[4]) != 0;        //Set the solved or not option.
                report.time = (string)row[5];               //Set the time and date.
                report.position = (string)row[6];           //Set the position the player was on the map.
                player.reports.Add(report);                 //Add the report to a list to pull with other addon.
            }
        }

#endif
    }

    // -----------------------------------------------------------------------------------
    // SaveReports
    //Saves the information provided to the database.
    // -----------------------------------------------------------------------------------
    public void SaveReports(UCE_HelpMember report)
    {
#if _MYSQL

		ExecuteNonQueryMySql("INSERT INTO UCE_reports VALUES (@senderAcc, @senderCharacter, @readBefore, @title, @message, @solved, @time, @position)",
                        new MySqlParameter("@senderAcc", report.senderAcc),
                        new MySqlParameter("@senderCharacter", report.senderCharacter),
                        new MySqlParameter("@readBefore", report.readBefore ? 1 : 0),
                        new MySqlParameter("@title", report.title),
                        new MySqlParameter("@message", report.message),
                        new MySqlParameter("@solved", report.solved ? 1 : 0),
                        new MySqlParameter("@time", report.time),
                        new MySqlParameter("@position", report.position));

#elif _SQLITE

        ExecuteNonQuery("INSERT INTO 'UCE_reports' VALUES (@senderAcc, @senderCharacter, @readBefore, @title, @message, @solved, @time, @position)",
new SqliteParameter("@senderAcc", report.senderAcc),
new SqliteParameter("@senderCharacter", report.senderCharacter),
new SqliteParameter("@readBefore", report.readBefore ? 1 : 0),
new SqliteParameter("@title", report.title),
new SqliteParameter("@message", report.message),
new SqliteParameter("@solved", report.solved ? 1 : 0),
new SqliteParameter("@time", report.time),
new SqliteParameter("@position", report.position));

#endif
    }

    // -----------------------------------------------------------------------------------

    #endregion Functions
}