// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;

// =======================================================================================
// UCE_PlayerCountAreaLimitation
// =======================================================================================
[System.Serializable]
public class UCE_PlayerGroupLocations
{
    public Transform[] teleportPosition;
}

// =======================================================================================
// UCE_SharedInstanceEntry
// =======================================================================================
[System.Serializable]
public class UCE_SharedInstanceEntry
{
    public Sprite image;
    public string title;
    public string description;

    public int instanceCategory;
    public UCE_SharedInstanceArea targetArea;

    public UCE_HonorShopCurrencyCost entranceCost;

    // -----------------------------------------------------------------------------------
    // getPlayerCount
    // Returns the amount of players currently in the target area
    // -----------------------------------------------------------------------------------
    public int getPlayerCount
    {
        get { return targetArea.getPlayerCount; }
    }

    // -----------------------------------------------------------------------------------
    // getMaxPlayerCount
    // Returns the maximum amount of players (per group) that can enter the area
    // -----------------------------------------------------------------------------------
    public int getMaxPlayerCount
    {
        get { return targetArea.maxPlayersPerGroup; }
    }

    // -----------------------------------------------------------------------------------
    // getGroupCount
    // Returns the maximum amount of groups that can enter the target area
    // -----------------------------------------------------------------------------------
    public int getGroupCount
    {
        get { return targetArea.getGroupCount; }
    }

    // -----------------------------------------------------------------------------------
    // getGroupCount
    // Returns the maximum amount of groups that can enter the target area
    // -----------------------------------------------------------------------------------
    public int getMaxGroupCount
    {
        get { return targetArea.getMaxGroupCount; }
    }

    // -----------------------------------------------------------------------------------
    // canPlayerSee
    // Checks if a player is allowed to see the target area in the list
    // -----------------------------------------------------------------------------------
    public bool canPlayerSee(Player player)
    {
        if (!targetArea || !player) return false;
        return targetArea.canPlayerSee(player);
    }

    // -----------------------------------------------------------------------------------
    // canPlayerEnter
    // Checks if a player is allowed to enter the target area
    // -----------------------------------------------------------------------------------
    public bool canPlayerEnter(Player player)
    {
        if (!targetArea || !player) return false;
        return checkEntranceCost(player) && targetArea.canPlayerEnter(player);
    }

    // ================================= COST FUNCTIONS ====================================

    // -----------------------------------------------------------------------------------
    // checkEntranceCost
    //
    // -----------------------------------------------------------------------------------
    protected bool checkEntranceCost(Player player)
    {
        return player.UCE_GetHonorCurrency(entranceCost.honorCurrency) >= entranceCost.amount;
    }

    // -----------------------------------------------------------------------------------
    // payEntranceCost
    //
    // -----------------------------------------------------------------------------------
    protected void payEntranceCost(Player player)
    {
        if (entranceCost.amount > 0)
            player.UCE_AddHonorCurrency(entranceCost.honorCurrency, entranceCost.amount * -1);
    }

    // -----------------------------------------------------------------------------------
    // teleportPlayerToInstance
    // -----------------------------------------------------------------------------------
    public void teleportPlayerToInstance(Player player)
    {
        if (!canPlayerEnter(player)) return;

        payEntranceCost(player);

        int index = targetArea.getGroupIndex(player);

        if (index == -1)
            index = 0;

        UCE_PlayerGroupLocations locations = targetArea.playerGroupLocation[index];

        if (locations.teleportPosition.Length == 0) return;

        index = UnityEngine.Random.Range(0, locations.teleportPosition.Length - 1);

        player.Cmd_UCE_Warp(locations.teleportPosition[index].position);
    }

    // ================================= UI FUNCTIONS ====================================

    // -----------------------------------------------------------------------------------
    // getGroupType
    //
    // -----------------------------------------------------------------------------------
    public string getGroupType()
    {
        if (targetArea.playerGroupType == GroupType.Party)
        {
            return "Party";
        }
        else if (targetArea.playerGroupType == GroupType.Guild)
        {
            return "Guild";
        }
        else if (targetArea.playerGroupType == GroupType.Realm)
        {
            return "Realm";
        }

        return "Open";
    }

    // -----------------------------------------------------------------------------------
    // getPlayerCountText
    //
    // -----------------------------------------------------------------------------------
    public string getPlayerCountText()
    {
        if (targetArea.getMaxPlayerCount > 0)
            return getPlayerCount + "/" + getMaxPlayerCount;

        return "1+";
    }

    // -----------------------------------------------------------------------------------
    // getGroupCountText
    // Returns a formatted string to show the number of groups currently in the area
    // -----------------------------------------------------------------------------------
    public string getGroupCountText()
    {
        if (targetArea.playerGroupType == GroupType.None)
            return "";

        string text = getGroupCount.ToString() + "/" + getMaxGroupCount.ToString();

        if (targetArea.playerGroupType == GroupType.Party)
        {
            text += " Parties";
        }
        else if (targetArea.playerGroupType == GroupType.Guild)
        {
            text += " Guilds";
        }
        else if (targetArea.playerGroupType == GroupType.Realm)
        {
            text += " Realms";
        }

        if (getGroupCount > 0)
        {
            text += " [ ";
            foreach (string name in targetArea.groupNames)
                text += name + " ";
            text += "]";
        }

        return text;
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================