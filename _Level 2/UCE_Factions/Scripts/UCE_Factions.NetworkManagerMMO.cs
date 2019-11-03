// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

// =======================================================================================
// NETWORK MANAGER MMO
// =======================================================================================
public partial class NetworkManagerMMO
{
    // -----------------------------------------------------------------------------------
    // OnServerCharacterCreate_UCE_Factions
    // -----------------------------------------------------------------------------------
    [DevExtMethods("OnServerCharacterCreate")]
    private void OnServerCharacterCreate_UCE_Factions(CharacterCreateMsg message, Player player)
    {
        foreach (UCE_FactionRating faction in player.startingFactions)
            player.UCE_AddFactionRating(faction.faction, faction.startRating);
    }

    // -----------------------------------------------------------------------------------
}