// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using System;
using System.Text;
using UnityEngine;
using System.Collections.Generic;

// =======================================================================================
// SWITCHABLE MESH
// =======================================================================================
[System.Serializable]
public partial class SwitchableMesh
{
    public GameObject mesh;
    [HideInInspector] public Material defaultMaterial;
}

// =======================================================================================
// SWITCHABLE COLOR
// =======================================================================================
[System.Serializable]
public partial class SwitchableColor
{
    public string propertyName;
    public Color color;
}