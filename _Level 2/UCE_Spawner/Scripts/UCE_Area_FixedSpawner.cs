// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;

// =======================================================================================
// FIXED SPAWNER AREA
// =======================================================================================
[RequireComponent(typeof(Collider2D))]
public class UCE_Area_FixedSpawner : UCE_Area_Spawner
{
    [Header("-=-=-=- SPAWN LIST -=-=-=-")]
    public UCE_FixedSpawnableGameObject[] spawnableGameObjects;

    // -----------------------------------------------------------------------------------
    // Start
    // -----------------------------------------------------------------------------------
    public override void Start()
    {
        base.Start();
        UpdateMaxGameObjects();
    }

    // -----------------------------------------------------------------------------------
    // UpdateMaxGameObjects
    // -----------------------------------------------------------------------------------
    protected override void UpdateMaxGameObjects()
    {
        maxGameObjects = spawnableGameObjects.Length;
    }

    // -----------------------------------------------------------------------------------
    // OnSpawn
    // -----------------------------------------------------------------------------------
    protected override void OnSpawn()
    {
        foreach (UCE_FixedSpawnableGameObject spawnableEntity in spawnableGameObjects)
            spawnGameObject(spawnableEntity.gameobject, spawnableEntity.transform.position, spawnableEntity.transform.rotation);
    }

    // -----------------------------------------------------------------------------------
    // OnUnspawn
    // -----------------------------------------------------------------------------------
    protected override void OnUnspawn() { }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================