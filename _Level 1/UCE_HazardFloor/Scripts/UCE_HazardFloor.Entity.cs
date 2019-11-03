// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using Mirror;
using System.Linq;
using UnityEngine;

// =======================================================================================
// ENTITY
// =======================================================================================
public partial class Entity
{
    // -------------------------------------------------------------------------------
    // UCE_HazardFloorEnter
    // -------------------------------------------------------------------------------
    [ServerCallback]
    public void UCE_HazardFloorEnter(UCE_HazardBuff[] onEnterBuff)
    {
        foreach (UCE_HazardBuff buff in onEnterBuff)
        {
            if (this is Player &&
                buff.protectiveRequirements.hasRequirements() &&
                buff.protectiveRequirements.checkRequirements((Player)this))
            {
                ((Player)this).UCE_TargetAddMessage(buff.protectiveMessage);
                break;
            }

            int level = UnityEngine.Random.Range(buff.minBuffLevel, buff.maxBuffLevel);

            UCE_ApplyBuff(buff.buff, level, buff.chance);
        }
    }

    // -------------------------------------------------------------------------------
    // UCE_HazardFloorLeave
    // -------------------------------------------------------------------------------
    [ServerCallback]
    public void UCE_HazardFloorLeave(TargetBuffSkill[] onExitBuff)
    {
        if (onExitBuff.Length > 0)
        {
            for (int j = 0; j < onExitBuff.Length; ++j)
            {
                if (buffs.Any(x => x.data == onExitBuff[j]))
                {
                    buffs.RemoveAt(j);
                }
            }
        }
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================