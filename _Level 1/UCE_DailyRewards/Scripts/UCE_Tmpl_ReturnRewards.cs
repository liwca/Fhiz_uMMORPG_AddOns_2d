// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;

// =======================================================================================
// RETURN REWARDS - TEMPLATE
// =======================================================================================
[CreateAssetMenu(fileName = "UCE_Tmpl_ReturnRewards", menuName = "UCE Templates/New UCE ReturnRewards", order = 999)]
public class UCE_Tmpl_ReturnRewards : ScriptableObject
{
    [Header("-=-=-=- Returning Player Reward -=-=-=-")]
    [Tooltip("One click deactivation")]
    public bool isActive = true;

    [Tooltip("Min player level to enable the reward")]
    public int MinLevel = 1;

    [Tooltip("This quest must be completed first")]
#if _iMMOQUESTS
    public UCE_ScriptableQuest requiredQuest;
#else
	public ScriptableQuest requiredQuest;
#endif

    [Tooltip("Days between login to activate reward counter")]
    public int LoginIntervalDays = 30;

    [Tooltip("Define your rewards by adding and editing entries")]
    public UCE_DailyReward reward;
}

// =======================================================================================