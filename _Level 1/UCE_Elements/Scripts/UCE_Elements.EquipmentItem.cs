using System.Linq;
using UnityEngine;

// =======================================================================================
// EQUIPMENT ITEM
// =======================================================================================
public partial class EquipmentItem
{
    [Header("-=-=- UCE ELEMENTAL ATTACK -=-=-")]
    public UCE_ElementTemplate elementalAttack;

    [Header("-=-=- UCE ELEMENTAL RESISTANCES -=-=-")]
    public UCE_ElementModifier[] elementalResistances;

    public float GetResistance(UCE_ElementTemplate element)
    {
        if (elementalResistances.Any(x => x.template == element))
            return elementalResistances.FirstOrDefault(x => x.template == element).value;
        else
            return 0;
    }

    // -----------------------------------------------------------------------------------
}