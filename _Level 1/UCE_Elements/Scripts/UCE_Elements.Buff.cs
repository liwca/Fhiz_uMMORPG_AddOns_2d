// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using System.Linq;

// =======================================================================================
// BUFF
// =======================================================================================
public partial struct Buff
{
    public LevelBasedElement[] elementalResistances { get { return data.elementalResistances; } }

    public float GetResistance(UCE_ElementTemplate element)
    {
        return elementalResistances.FirstOrDefault(x => x.template == element).Get(level);
    }
}

// =======================================================================================