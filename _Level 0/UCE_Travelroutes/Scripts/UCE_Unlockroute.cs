using UnityEngine;

// =======================================================================================
// Unlockroute
// =======================================================================================
[System.Serializable]
public class UCE_Unlockroute
{
    [Header("[-=-=-=- UCE UNLOCKROUTE -=-=-=-]")]
    [Tooltip("[Required] Any on scene Transform or GameObject OR off scene coordinates (requires UCE Network Zones AddOn)")]
    public UCE_TeleportationTarget teleportationTarget;

    [Tooltip("[Optional] The amount of experience is granted only once, when the route is first discovered")]
    public int ExpGain;
}

// =======================================================================================