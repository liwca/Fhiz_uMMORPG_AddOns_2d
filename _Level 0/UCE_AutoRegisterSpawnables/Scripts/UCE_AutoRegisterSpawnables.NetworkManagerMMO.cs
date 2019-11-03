// =======================================================================================
// Created and maintained by iMMO
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;
using Mirror;
using System.Collections.Generic;
using UnityEditor;
using System;

// =======================================================================================
// NETWORK MANAGER
// =======================================================================================
public partial class NetworkManagerMMO
{
    // -----------------------------------------------------------------------------------
    // AutoRegisterSpawnables
    // @Editor
    // -----------------------------------------------------------------------------------
    public void AutoRegisterSpawnables()
    {
#if UNITY_EDITOR

        var guids = AssetDatabase.FindAssets("t:Prefab");
        List<GameObject> toSelect = new List<GameObject>();
        spawnPrefabs.Clear();

        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            UnityEngine.Object[] toCheck = AssetDatabase.LoadAllAssetsAtPath(path);
            foreach (UnityEngine.Object obj in toCheck)
            {
                var go = obj as GameObject;
                if (go == null)
                {
                    continue;
                }

                NetworkIdentity comp = go.GetComponent<NetworkIdentity>();
                if (comp != null && !comp.serverOnly)
                {
                    toSelect.Add(go);
                }
            }
        }

        spawnPrefabs.AddRange(toSelect.ToArray());

        Debug.Log("Added [" + toSelect.Count + "] network prefabs to spawnables list");
#endif
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================