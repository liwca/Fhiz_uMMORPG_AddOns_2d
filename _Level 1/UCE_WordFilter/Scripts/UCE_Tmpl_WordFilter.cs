using UnityEngine;

// =======================================================================================
// DATABASE CLEANER
// =======================================================================================
[CreateAssetMenu(fileName = "UCE WordFilter", menuName = "UCE Templates/New UCE WordFilter", order = 999)]
public class UCE_Tmpl_WordFilter : ScriptableObject
{
    [Tooltip("[Required] Enter all bad words here. If a chatext or player name contains one of them, it will be denied.")]
    public string[] badwords;
}

// =======================================================================================