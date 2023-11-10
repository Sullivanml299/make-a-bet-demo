using System.Collections;
using TMPro;
using UnityEngine;

public class Reward : MonoBehaviour
{
    //FIXME: rework how I set the reward type
    public float rotationTime = 1f;
    public GameObject Gold, Silver, Copper, Pooper;
    public RewardType rewardType { get; private set; }
    [SerializeField]
    private TextMeshPro rewardText;
    private GameObject currentReward;
    private float rewardValue;
    private bool visible = false;
    private float rotationSpeed => 360 / rotationTime;


    void Update()
    {
        if (visible)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

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
        this.visible = visible;
        if (currentReward != null)
        {
            currentReward.SetActive(visible);
            rewardText.gameObject.SetActive(visible);
        }
        if (!visible) transform.rotation = Quaternion.identity;
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