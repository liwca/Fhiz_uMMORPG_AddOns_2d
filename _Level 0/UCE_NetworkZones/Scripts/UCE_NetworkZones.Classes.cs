// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Mirror;

// =======================================================================================
// SceneLocation
// =======================================================================================
[System.Serializable]
public partial class SceneLocation
{
    public UnityScene mapScene;
    public Vector3 position;

    public bool Valid
    {
        get
        {
            return mapScene.IsSet();
        }
    }
}

// =======================================================================================
// NetworkSpawn
// =======================================================================================
[System.Serializable]
public partial class NetworkSpawn
{
    public GameObject gameObject;
    public Transform spawnPosition;
}

// =======================================================================================