// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;

// ===================================================================================
// SIMPLE DAILY REWARD
// ===================================================================================
[System.Serializable]
public class UCE_DailyReward
{
    [Header("-=-=- UCE Daily Reward -=-=-")]
    public long rewardGold;
    public long rewardCoins;
    public long rewardExperience;
    public long rewardSkillExperience;

    public UCE_ItemRequirement[] rewardItems;

#if _iMMOHONORSHOP
    public UCE_HonorShopCurrencyDrop[] honorCurrencies;
#endif
}

// ===================================================================================