using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Reward : MonoBehaviour
{
    //FIXME: rework how I set the reward type
    public GameObject Gold, Silver, Copper, Pooper;
    public RewardType rewardType { get; private set; }

    private GameObject currentReward;

    public void SetReward(float value)
    {
        if (value >= 100) SetRewardType(RewardType.Gold);
        else if (value >= 10) SetRewardType(RewardType.Silver);
        else if (value > 0) SetRewardType(RewardType.Copper);
        else SetRewardType(RewardType.Pooper);
    }

    public void SetVisible(bool visible)
    {
        if (currentReward != null) currentReward.SetActive(visible);
    }

    private void SetRewardType(RewardType type)
    {
        if (currentReward != null) currentReward.SetActive(false);
        rewardType = type;
        switch (type)
        {
            case RewardType.Gold:
                currentReward = Gold;
                break;
            case RewardType.Silver:
                currentReward = Silver;
                break;
            case RewardType.Copper:
                currentReward = Copper;
                break;
            case RewardType.Pooper:
                currentReward = Pooper;
                break;
        }
    }
}

public enum RewardType
{
    Gold,
    Silver,
    Copper,
    Pooper
}