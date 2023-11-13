using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public RewardType rewardType { get; private set; }

    [SerializeField] private float rotationTime = 10f;
    [SerializeField] private float pullTime = 0.3f;
    [SerializeField] private float initialPullWaitTime = 0.2f;
    [SerializeField] private float betweenPullWaitTime = 0.1f;
    [SerializeField] private GameObject Gold, Silver, Copper, Pooper;
    [SerializeField] private TextMeshPro rewardText;
    private GameObject currentReward;
    private float rewardValue;
    private bool visible = false;
    private float rotationSpeed => 360 / rotationTime;
    private Vector3 startPosition, startScale;

    void Awake()
    {
        SetVisible(false);
    }

    void Update()
    {
        if (visible && currentReward != Pooper)
        {
            currentReward.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
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
    }

    public void TriggerBlackHole(List<TreasureChest> openChests)
    {
        StartCoroutine(BlackHolePull(openChests));
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

    private IEnumerator BlackHolePull(List<TreasureChest> openChests)
    {
        float time = 0f;

        while (time < initialPullWaitTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        GameObject currentReward;
        foreach (TreasureChest chest in openChests)
        {
            currentReward = chest.reward.currentReward;
            startPosition = currentReward.transform.position;
            startScale = currentReward.transform.localScale;


            while (time < betweenPullWaitTime)
            {
                time += Time.deltaTime;
                yield return null;
            }

            time = 0f;

            while (time < pullTime)
            {
                time += Time.deltaTime;
                currentReward.transform.position = Vector3.Lerp(startPosition, Pooper.transform.position, time / pullTime);
                currentReward.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, time / pullTime);
                yield return null;
            }

            chest.reward.SetVisible(false);
            currentReward.transform.position = startPosition;
            currentReward.transform.localScale = startScale;
            GameplayController.Instance.UpdateBalance(chest.reward.rewardValue);
        }

        GameStateManager.Instance.ChangeGameState(GameState.Setup);
    }
}

public enum RewardType
{
    Gold,
    Silver,
    Copper,
    Pooper
}