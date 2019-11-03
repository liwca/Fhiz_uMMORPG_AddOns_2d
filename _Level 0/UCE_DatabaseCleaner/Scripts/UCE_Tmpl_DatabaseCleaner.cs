using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// =======================================================================================
// DATABASE CLEANER
// =======================================================================================
[CreateAssetMenu(fileName = "UCE DatabaseCleaner", menuName = "UCE Templates/New UCE DatabaseCleaner", order = 999)]
public class UCE_Tmpl_DatabaseCleaner : ScriptableObject
{
    [Tooltip("One click deactivation")]
    public bool isActive = true;

    [Tooltip("Delete inactive accounts after x days on server start (set 0 to disable)")]
    public int PruneInactiveAfterDays = 1;

    [Tooltip("Delete banned accounts after x days on server start (set 0 to disable)")]
    public int PruneBannedAfterDays = 1;

    [Tooltip("Delete empty accounts (= 0 chars) after x days on server start (set 0 to disable)")]
    public bool PruneEmptyAccounts = true;
    public string[] characterTables;
    public string[] accountTables;
}

// =======================================================================================