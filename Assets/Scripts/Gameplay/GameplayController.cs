using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour, IGameStateObserver
{
    public static GameplayController Instance { get; private set; }
    [SerializeField] private LastGameWinController lastGameWinController;
    [SerializeField] private PlayerBankController playerBankController;
    [SerializeField] private DenominationController denominationController;
    [SerializeField] private PlayButton playButton;
    [SerializeField] private CoinDropController coinDropController;
    private GameRoundData gameRoundData = new GameRoundData();
    private bool canSelectChest = false;
    private float currentWinAmount;
    private bool endRound = false;
    private List<TreasureChest> openChests = new List<TreasureChest>();
    private TreasureChest blackHoleChest;

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        GameStateManager.Instance.RegisterObserver(this);
    }

    public void OnGameStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.Setup:
                SetupRound();
                break;

            case GameState.Playing:
                StartRound();
                break;
        }
    }

    public void SetBetAmount(float amount)
    {
        gameRoundData.BetAmount = amount;
        playButton.SetInteractable(amount <= playerBankController.CurrentBalance);
    }

    public void UpdateBalance(float value)
    {
        if (value > 0) coinDropController.TriggerCoinDrop();
        playerBankController.UpdateBalance(value);
    }

    public void OpenChest(TreasureChest chest)
    {
        if (!canSelectChest || chest.isOpen) return;
        canSelectChest = false;
        SetCurrentWinAmount();
        chest.SetValue(currentWinAmount);
        chest.Open();
        if (endRound) blackHoleChest = chest;
        else openChests.Add(chest);
    }

    public void EndOpen()
    {
        lastGameWinController.UpdateWinnings(currentWinAmount);
        if (endRound) EndRound();
        else canSelectChest = true;
    }

    private void SetCurrentWinAmount()
    {
        if (gameRoundData.WinningsQueue.Count == 0)
        {
            currentWinAmount = 0;
            endRound = true;
        }
        else currentWinAmount = gameRoundData.WinningsQueue.Dequeue();
    }

    private void SetupRound()
    {
        canSelectChest = false;
        denominationController.SetInteractable(true);
        playButton.SetInteractable(gameRoundData.BetAmount <= playerBankController.CurrentBalance);
    }

    private void StartRound()
    {
        endRound = false;
        canSelectChest = true;
        playButton.SetInteractable(false);
        denominationController.SetInteractable(false);
        lastGameWinController.Reset();
        openChests.Clear();

        UpdateBalance(-gameRoundData.BetAmount);
        gameRoundData.RoundMultiplier = Multiplier.GetRandomMultplier();
        gameRoundData.TotalWinnings = gameRoundData.RoundMultiplier * gameRoundData.BetAmount;
        Winnings.SplitWinnings(gameRoundData);
        Debug.Log(gameRoundData);
    }

    private void EndRound()
    {
        blackHoleChest.TriggerBlackHole(openChests);
    }

}
