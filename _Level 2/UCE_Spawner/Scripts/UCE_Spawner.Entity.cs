// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;

// =======================================================================================
// ENTITY
// =======================================================================================
public partial class Entity
{
    [HideInInspector] public UCE_Area_WaveSpawner UCE_parentSpawnArea;
    [HideInInspector] public int UCE_parentWaveIndex;

    // -----------------------------------------------------------------------------------
    // OnDeath
    // -----------------------------------------------------------------------------------
    [DevExtMethods("OnDeath")]
    public void OnDeath_UCE_WaveSpawnerEntity()
    {
        if (UCE_parentSpawnArea == null) return;
        UCE_parentSpawnArea.updateMemberPopulation(name.GetStableHashCode(), UCE_parentWaveIndex);
        UCE_parentSpawnArea = null;
    }

    // -----------------------------------------------------------------------------------
    // Awake
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Awake")]
    public void Awake_UCE_WaveSpawnerEntity()
    {
        name = name.Replace("(Clone)", "").Trim();
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================