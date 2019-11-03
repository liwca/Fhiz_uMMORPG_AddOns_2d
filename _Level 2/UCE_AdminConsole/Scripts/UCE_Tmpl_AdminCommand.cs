// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// =======================================================================================
// ADMIN COMMAND - TEMPLATE
// =======================================================================================
[CreateAssetMenu(fileName = "UCE AdminCommand", menuName = "UCE Templates/New UCE AdminCommand", order = 999)]
public class UCE_Tmpl_AdminCommand : ScriptableObject
{
    [Header("-=-=-=- UCE Admin Command -=-=-=-")]
    public string commandName;
    public string functionName;
    [Range(1, 100)] public int commandLevel;
    public UCE_AdminCommandArgument[] arguments;
    public string helpText;

    // -----------------------------------------------------------------------------------
    // getFormat
    // -----------------------------------------------------------------------------------
    public string getFormat()
    {
        string format = "";

        format += commandName + " ";

        foreach (UCE_AdminCommandArgument arg in arguments)
        {
            format += "<" + arg.argumentType.ToString() + "> ";
        }

        return format;
    }

    // -----------------------------------------------------------------------------------
    // Cache
    // -----------------------------------------------------------------------------------
    private Dictionary<int, UCE_Tmpl_AdminCommand> cache;

    public Dictionary<int, UCE_Tmpl_AdminCommand> dict
    {
        get
        {
            // load if not loaded yet
            return cache ?? (cache = Resources.LoadAll<UCE_Tmpl_AdminCommand>("").ToDictionary(
                item => item.name.GetStableHashCode(), item => item)
            );
        }
    }
}

// =======================================================================================