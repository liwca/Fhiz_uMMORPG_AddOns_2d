// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using System.Collections.Generic;
using System;

#if _MYSQL
using MySql.Data;								// From MySql.Data.dll in Plugins folder
using MySql.Data.MySqlClient;                   // From MySql.Data.dll in Plugins folder
#elif _SQLITE

using Mono.Data.Sqlite; 						// copied from Unity/Mono/lib/mono/2.0 to Plugins

#endif

// =======================================================================================
// DATABASE
// =======================================================================================
public partial class Database
{
    // -----------------------------------------------------------------------------------
    //
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    public void Connect_Mail()
    {
#if _MYSQL
		ExecuteNonQueryMySql(@"
                            CREATE TABLE IF NOT EXISTS mail(
							id INTEGER(16) NOT NULL AUTO_INCREMENT,
							messageFrom VARCHAR(32) NOT NULL,
							messageTo VARCHAR(32) NOT NULL,
							subject VARCHAR(32) NOT NULL,
							body VARCHAR(512) NOT NULL,
							sent BIGINT(16) NOT NULL,
							expires BIGINT(16) NOT NULL,
							`read` BIGINT(16) NOT NULL,
							`deleted` BIGINT(16) NOT NULL,
							`item` VARCHAR(32) NOT NULL,
                            PRIMARY KEY(id)
                            ) CHARACTER SET=utf8mb4");
#elif _SQLITE
        ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS mail (
							id INTEGER PRIMARY KEY,
							messageFrom TEXT NOT NULL,
							messageTo TEXT NOT NULL,
							subject TEXT NOT NULL,
							body TEXT NOT NULL,
							sent INTEGER NOT NULL,
							expires INTEGER NOT NULL,
							read INTEGER NOT NULL,
							deleted INTEGER NOT NULL,
							item TEXT NOT NULL
							)");
#endif
    }

    // -----------------------------------------------------------------------------------
    //
    // -----------------------------------------------------------------------------------
    public MailMessage Mail_BuildMessageFromDBRow(List<object> row)
    {
        MailMessage message = new MailMessage();

        int colNum = 0;

#if _MYSQL
		message.id		= (int)row[colNum++];
		message.from	= (string)row[colNum++];
		message.to		= (string)row[colNum++];
		message.subject = (string)row[colNum++];
		message.body	= (string)row[colNum++];
		message.sent	= (long)row[colNum++];
		message.expires = (long)row[colNum++];
		message.read	= (long)row[colNum++];
		message.deleted = (long)row[colNum++];

		string name = (string)row[colNum++];
		if (ScriptableItem.dict.TryGetValue(name.GetStableHashCode(), out ScriptableItem itemData))
        	message.item = itemData;

#elif _SQLITE
        message.id = (long)row[colNum++];
        message.from = (string)row[colNum++];
        message.to = (string)row[colNum++];
        message.subject = (string)row[colNum++];
        message.body = (string)row[colNum++];
        message.sent = (long)row[colNum++];
        message.expires = (long)row[colNum++];
        message.read = (long)row[colNum++];
        message.deleted = (long)row[colNum++];

        string name = (string)row[colNum++];
        if (ScriptableItem.dict.TryGetValue(name.GetStableHashCode(), out ScriptableItem itemData))
            message.item = itemData;

#endif

        return message;
    }

    // -----------------------------------------------------------------------------------
    //
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    public void CharacterLoad_Mail(Player player)
    {
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT * FROM mail WHERE messageTo=@character AND deleted=0 AND expires > @expires ORDER BY sent", new MySqlParameter("@character", player.name), new MySqlParameter("@expires", Epoch.Current()));
#elif _SQLITE
        var table = ExecuteReader("SELECT * FROM mail WHERE messageTo=@character AND deleted=0 AND expires > @expires ORDER BY sent", new SqliteParameter("@character", player.name), new SqliteParameter("@expires", Epoch.Current()));
#endif
        foreach (var row in table)
        {
            MailMessage message = Mail_BuildMessageFromDBRow(row);
            player.mailMessages.Add(message);
        }
    }

    // -----------------------------------------------------------------------------------
    //
    // -----------------------------------------------------------------------------------
    public List<MailSearch> Mail_SearchForCharacter(string name, string selfPlayer)
    {
        List<MailSearch> result = new List<MailSearch>();

#if _MYSQL
		var table = ExecuteReaderMySql(@"SELECT `name` , level FROM `characters` WHERE name=@search", new MySqlParameter("@search", name));

		foreach (var row in table) {
			MailSearch res = new MailSearch();
			res.name = (string)row[0];
			res.level = Convert.ToInt32((int)row[1]);
			res.guild = "";

			result.Add(res);
		}

#elif _SQLITE
        /**
		 * Order by here is setup in such a way that:
		 *		exact matches appear first
		 *		followed by names where the search string is closer to the front of the name
		 */
        var table = ExecuteReader(@"SELECT `name`, level FROM characters
										LEFT JOIN character_guild
											ON character=name
									WHERE name LIKE '%' || @search || '%'
										AND name <> @self
									ORDER BY
										CASE
											WHEN name=@search THEN 0
											ELSE INSTR(LOWER(name), LOWER(@search))
										END, name
									LIMIT 30", new SqliteParameter("@search", name), new SqliteParameter("@self", selfPlayer));

        foreach (var row in table)
        {
            MailSearch res = new MailSearch();
            res.name = (string)row[0];
            res.level = Convert.ToInt32((long)row[1]);
            res.guild = "";

            result.Add(res);
        }

#endif

        return result;
    }

    // -----------------------------------------------------------------------------------
    //
    // -----------------------------------------------------------------------------------
    public void Mail_CreateMessage(string from, string to, string subject, string body, string itemName, long expiration = 0)
    {
        long sent = Epoch.Current();
        long expires = 0;
        if (expiration > 0)
        {
            expires = sent + expiration;
        }

        if (itemName == null) itemName = "";

#if _MYSQL
		ExecuteNonQueryMySql(@"INSERT INTO mail (
							messageFrom, messageTo, subject, body, sent, `expires`, `read`, `deleted`, `item`
						) VALUES (
							@from, @to, @subject, @body, @sent, @expires, 0, 0, @item
						)",
						new MySqlParameter("@from", from),
						new MySqlParameter("@to", to),
						new MySqlParameter("@subject", subject),
						new MySqlParameter("@body", body),
						new MySqlParameter("@sent", sent),
						new MySqlParameter("@expires", expires),
						new MySqlParameter("@item", itemName )
						);
#elif _SQLITE
        ExecuteNonQuery(@"INSERT INTO mail (
							messageFrom, messageTo, subject, body, sent, expires, read, deleted, item
						) VALUES (
							@from, @to, @subject, @body, @sent, @expires, 0, 0, @item
						)",
                        new SqliteParameter("@from", from),
                        new SqliteParameter("@to", to),
                        new SqliteParameter("@subject", subject),
                        new SqliteParameter("@body", body),
                        new SqliteParameter("@sent", sent),
                        new SqliteParameter("@expires", expires),
                        new SqliteParameter("@item", itemName)
                        );
#endif
    }

    // -----------------------------------------------------------------------------------
    //
    // -----------------------------------------------------------------------------------
    public void Mail_UpdateMessage(MailMessage message)
    {
        string itemName = "";
        if (message.item != null)
            itemName = message.item.name;

#if _MYSQL
		ExecuteNonQueryMySql(@"UPDATE mail SET
							`read`=@read,
							deleted=@deleted,
							item=@item
						WHERE id=@id",
						new MySqlParameter("@read", message.read),
						new MySqlParameter("@deleted", message.deleted),
						new MySqlParameter("@item", itemName),
						new MySqlParameter("@id", message.id));

#elif _SQLITE
        ExecuteNonQuery(@"UPDATE mail SET
							read=@read,
							deleted=@deleted,
							item=@item
						WHERE id=@id",
                        new SqliteParameter("@read", message.read),
                        new SqliteParameter("@deleted", message.deleted),
                        new SqliteParameter("@item", itemName),
                        new SqliteParameter("@id", message.id));
#endif
    }

    // -----------------------------------------------------------------------------------
    //
    // -----------------------------------------------------------------------------------
    public MailMessage Mail_MessageById(long id)
    {
        MailMessage message = new MailMessage();
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT * FROM mail WHERE id=@id", new MySqlParameter("@id", id));
#elif _SQLITE
        var table = ExecuteReader("SELECT * FROM mail WHERE id=@id", new SqliteParameter("@id", id));
#endif
        if (table.Count == 1)
        {
            message = Mail_BuildMessageFromDBRow(table[0]);
        }

        return message;
    }

    // -----------------------------------------------------------------------------------
    //
    // -----------------------------------------------------------------------------------
    public List<MailMessage> Mail_CheckForNewMessages(long maxID)
    {
        List<MailMessage> result = new List<MailMessage>();
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT * FROM mail WHERE id > @maxid AND deleted=0 AND expires > @expires ORDER BY sent", new MySqlParameter("@maxid", maxID), new MySqlParameter("@expires", Epoch.Current()));
#elif _SQLITE
        var table = ExecuteReader("SELECT * FROM mail WHERE id > @maxid AND deleted=0 AND expires > @expires ORDER BY sent", new SqliteParameter("@maxid", maxID), new SqliteParameter("@expires", Epoch.Current()));
#endif
        foreach (var row in table)
        {
            MailMessage message = Mail_BuildMessageFromDBRow(row);
            result.Add(message);
        }

        return result;
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================