using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour, IGameStateObserver
{
    public static GameplayController Instance { get; private set; }
    [SerializeField] private LastGameWinController lastGameWinController;
    [SerializeField] private PlayerBankController playerBankController;
    [SerializeField] private DenominationController denominationController;
    [SerializeField] private PlayButton playButton;
    private GameRoundData gameRoundData = new GameRoundData();
    private bool canSelectChest = false;
    private float currentWinAmount;
    private bool endRound = false;

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

    public void SelectChest(TreasureChest chest)
    {
        if (!canSelectChest) return;
        canSelectChest = false;
        SetCurrentWinAmount();
        chest.SetValue(currentWinAmount);
        chest.Open();
    }

    private void SetCurrentWinAmount()
    {
        if (gameRoundData.WinAmounts.Count == 0)
        {
            currentWinAmount = 0;
            endRound = true;
        }
        else currentWinAmount = gameRoundData.WinAmounts.Dequeue();
    }

    public void EndOpen()
    {
        lastGameWinController.UpdateWinnings(currentWinAmount);
        if (endRound) EndRound();
        else canSelectChest = true;
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
        denominationController.SetInteractable(true);
        lastGameWinController.Reset();

        playerBankController.UpdateBalance(-gameRoundData.BetAmount);
        gameRoundData.RoundMultiplier = Multiplier.GetRandomMultplier();
        gameRoundData.TotalWinnings = gameRoundData.RoundMultiplier * gameRoundData.BetAmount;
        Winnings.SplitWinnings(gameRoundData);
        Debug.Log(gameRoundData);
    }

    private void EndRound()
    {
        GameStateManager.Instance.ChangeGameState(GameState.Setup);
        playerBankController.UpdateBalance(gameRoundData.TotalWinnings);
        //TODO: add animation to show winnings filling your bank (current balance)
    }
}
