using UnityEngine;

public partial class EquipmentItem : UsableItem
{
    [Header("-=-=- UCE Usage Requirements -=-=-")]
    [Tooltip("While equipped, allows the usage of skills with the same ID (0 = disabled)")]
    public int usageEquipmentId;
}