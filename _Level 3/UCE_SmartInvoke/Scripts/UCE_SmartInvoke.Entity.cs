// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using Mirror;

// =======================================================================================
// Entity
// =======================================================================================
public partial class Entity
{
    protected bool bRecovering = false;

    // -----------------------------------------------------------------------------------
    // Update
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Update")]
    private void Update_UCE_SmartInvoke()
    {
        if (!isServer) return;

        if (
            !bRecovering &&
            enabled &&
            isAlive &&
            !(this is Npc) &&
            (
            health < healthMax ||
            mana < manaMax
            )
            )
        {
            InvokeRepeating("UCE_Recover", 1, 1);
            bRecovering = true;
        }
    }

    // -----------------------------------------------------------------------------------
    // UCE_Recover
    // -----------------------------------------------------------------------------------
    [ServerCallback]
    public void UCE_Recover()
    {
        if (
            !enabled ||
            !isAlive ||
            (!healthRecovery && !manaRecovery) ||
            (health >= healthMax && mana >= manaMax) ||
            (healthRecoveryRate == 0 && manaRecoveryRate == 0)
            )
        {
            CancelInvoke("UCE_Recover");
            bRecovering = false;
            return;
        }

        if (healthRecovery) health += healthRecoveryRate;
        if (manaRecovery) mana += manaRecoveryRate;
    }

    // -----------------------------------------------------------------------------------
}