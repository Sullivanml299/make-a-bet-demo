using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Reward : MonoBehaviour
{
    public GameObject Gold, Silver, Bronze, Pooper;
    public RewardType rewardType { get; private set; }

    private GameObject currentReward;

    public void SetRewardType(RewardType type)
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
                currentReward = Bronze;
                break;
            case RewardType.Pooper:
                currentReward = Pooper;
                break;
        }
        currentReward.SetActive(true);
    }
}

public enum RewardType
{
    Gold,
    Silver,
    Copper,
    Pooper
}