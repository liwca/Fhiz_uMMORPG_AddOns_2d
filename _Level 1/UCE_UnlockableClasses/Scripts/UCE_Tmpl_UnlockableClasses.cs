using UnityEngine;

// =======================================================================================
// DEFAULT UNLOCKED CLASSES
// =======================================================================================
[CreateAssetMenu(fileName = "UCE Default Unlocked Classes", menuName = "UCE Templates/New UCE Default Unlocked Classes", order = 999)]
public class UCE_Tmpl_UnlockableClasses : ScriptableObject
{
    [Tooltip("[Required] Default classes available to all players")]
    public Player[] defaultClasses;
}

// =======================================================================================