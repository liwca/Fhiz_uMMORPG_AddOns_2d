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
// EQUIPMENT ITEM
// =======================================================================================
public partial class EquipmentItem
{
    [Header("[-=-=- UCE Mesh Switcher -=-=-]")]
    public int[] meshIndex;
    public Material meshMaterial;
    public SwitchableColor[] switchableColors;
}