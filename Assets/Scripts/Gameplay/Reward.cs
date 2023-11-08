using System.Collections;
using TMPro;
using UnityEngine;

public class Reward : MonoBehaviour
{
    //FIXME: rework how I set the reward type
    public GameObject Gold, Silver, Copper, Pooper;
    public RewardType rewardType { get; private set; }
    [SerializeField]
    private TextMeshPro rewardText;

    private GameObject currentReward;
    private float rewardValue;

    public void SetReward(float value)
    {
        rewardValue = value;
        rewardText.text = value.ToString("C2");
        if (value >= 100) SetRewardType(RewardType.Gold);
        else if (value >= 10) SetRewardType(RewardType.Silver);
        else if (value > 0) SetRewardType(RewardType.Copper);
        else SetRewardType(RewardType.Pooper);
    }

    public void SetVisible(bool visible)
    {
        if (currentReward != null)
        {
            currentReward.SetActive(visible);
            rewardText.gameObject.SetActive(visible);
        }
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