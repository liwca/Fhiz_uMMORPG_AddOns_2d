// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// =======================================================================================
// RTS BUILD SYSTEM NETWORK MANAGER MMO
// =======================================================================================
public partial class NetworkManagerMMO
{
    // -----------------------------------------------------------------------------------
    // OnStartServer_UCE_RTSBuildSystem
    // -----------------------------------------------------------------------------------
    [DevExtMethods("OnStartServer")]
    public void OnStartServer_UCE_RTSBuildSystem()
    {
        Invoke("StartSpawnRTSStructures", 1);
    }

    // -----------------------------------------------------------------------------------
    // StartSpawnRTSStructures
    // Started with a small delay to prevent a sync issue because server is not ready yet
    // -----------------------------------------------------------------------------------
    protected void StartSpawnRTSStructures()
    {
        if (NetworkServer.active)
            StartCoroutine("SpawnRTSStructures");
    }

    // -----------------------------------------------------------------------------------
    // SpawnRTSStructures
    // -----------------------------------------------------------------------------------
    protected IEnumerator SpawnRTSStructures()
    {
        List<List<object>> table = Database.singleton.UCE_LoadPlaceableObjects();

        foreach (List<object> row in table)
        {
            string itemName = (string)row[9];

            if (((UCE_Item_PlaceableObject)ScriptableItem.dict[itemName.GetStableHashCode()]).placementObject)
            {
                Vector3 v = new Vector3(
                                (float)row[2],
                                (float)row[3],
                                (float)row[4]);

                Quaternion rotation = Quaternion.Euler((float)row[5], (float)row[6], (float)row[7]);

                GameObject go = (GameObject)Instantiate(((UCE_Item_PlaceableObject)ScriptableItem.dict[itemName.GetStableHashCode()]).placementObject, v, rotation);

                UCE_PlaceableObject po = go.GetComponent<UCE_PlaceableObject>();

                if (po)
                {
                    po.permanent = true;
                    po.ownerCharacter = (string)row[0];
                    po.ownerGuild = (string)row[1];
                    po.itemName = itemName; // row 9

#if _SQLITE
                    po.id = Convert.ToInt32((long)row[10]);
#elif _MYSQL
						po.id				= (int)row[10];
#endif

                    Entity e = po.GetComponent<Entity>();

                    if (e)
                    {
#if _SQLITE
                        e.level = Convert.ToInt32((long)row[8]);
#elif _MYSQL
						e.level = (int)row[8];
#endif
                    }
                }

                NetworkServer.Spawn(go);
            }
        }

        yield return new WaitForEndOfFrame();
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================