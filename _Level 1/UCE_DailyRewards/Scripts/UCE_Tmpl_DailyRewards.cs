// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;

// =======================================================================================
// DAILY REWARDS - TEMPLATE
// =======================================================================================
[CreateAssetMenu(fileName = "UCE_Tmpl_DailyRewards", menuName = "UCE Templates/New UCE DailyRewards", order = 999)]
public class UCE_Tmpl_DailyRewards : ScriptableObject
{
    [Header("-=-=-=- Daily Rewards -=-=-=-")]
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

    [Tooltip("Hours between login to advance reward counter")]
    public int LoginIntervalHours = 23;

    [Tooltip("Define your rewards by adding and editing entries")]
    public UCE_DailyReward[] rewards;
}

// =======================================================================================