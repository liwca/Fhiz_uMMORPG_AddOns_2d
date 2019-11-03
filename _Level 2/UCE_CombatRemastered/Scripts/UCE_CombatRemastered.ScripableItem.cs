using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EquipmentItem
{
    // Create our weapon types to match our skill types.
    public enum EquipType { None, Unarmed, Slash1Hand, Pierce1Hand, Bludgeon1Hand, Slash2Hand, Pierce2Hand, Bludgeon2Hand, RangedThrown, RangedBow, RangedGun, Shield, Dual, Cloth, Leather, Plate }

    // Create a selectable type for each weapon.
    [Header("UCE Combat Remastered - Required Type")]
    public EquipType equipType = EquipType.None;
}