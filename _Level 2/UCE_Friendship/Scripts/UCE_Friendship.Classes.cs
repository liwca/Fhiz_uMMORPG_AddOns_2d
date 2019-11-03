// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using Mirror;

// =======================================================================================
// FRIEND
// =======================================================================================
public partial struct UCE_Friend
{
    public string name;
    public string lastGifted;

    public int level;
    public string _class;
    public string guild;
    public bool online;
    public bool inParty;

    // -----------------------------------------------------------------------------------
    // UCE_Friend (Constructor)
    // -----------------------------------------------------------------------------------
    public UCE_Friend(string _name, string _lastGifted)
    {
        name = _name;
        lastGifted = _lastGifted;

        level = 0;
        _class = "";
        guild = "";
        online = false;
        inParty = false;
    }

    // -----------------------------------------------------------------------------------
}

public class SyncListUCE_Friend : SyncList<UCE_Friend> { }

// =======================================================================================