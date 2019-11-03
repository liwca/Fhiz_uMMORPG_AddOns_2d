// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

// =======================================================================================
// UCE ELEMENT MODIFIER
// =======================================================================================
[System.Serializable]
public class UCE_ElementModifier
{
    public UCE_ElementTemplate template;
    public float value = 1f;
}

// =======================================================================================
// UCE ELEMENT CACHE
// =======================================================================================
[System.Serializable]
public class UCE_ElementCache
{
    public float timer = 0f;
    public float value = 0f;
}

// =======================================================================================