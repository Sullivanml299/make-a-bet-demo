using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayController : MonoBehaviour, IGameStateObserver
{
    public static GameplayController Instance { get; private set; }
    private GameRoundData gameRoundData = new GameRoundData();
    private bool canSelectChest = false;
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

    public void SelectChest(TreasureChest chest)
    {
        if (!canSelectChest) return;
        canSelectChest = false;
        if (gameRoundData.WinAmounts.Count == 0)
        {
            chest.SetValue(0);
            endRound = true;
        }
        else chest.SetValue(gameRoundData.WinAmounts.Dequeue());
        chest.Open();
    }

    public void EndOpen()
    {
        canSelectChest = true;
        if (endRound) EndRound();
    }

    public void SetBetAmount(float amount)
    {
        gameRoundData.BetAmount = amount;
    }

    public void OnGameStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.Setup:
                break;

            case GameState.Playing:
                StartRound();
                break;

            case GameState.PostGame:
                break;
        }
    }

    private void StartRound()
    {
        endRound = false;
        canSelectChest = true;
        gameRoundData.RoundMultiplier = Multiplier.GetRandomMultplier();
        gameRoundData.TotalWinnings = gameRoundData.RoundMultiplier * gameRoundData.BetAmount;
        Winnings.SplitWinnings(gameRoundData);
        Debug.Log(gameRoundData);
    }

    private void EndRound()
    {
        GameStateManager.Instance.ChangeGameState(GameState.PostGame);
        //TODO: add animation to show winnings filling your bank (current balance)
        GameStateManager.Instance.ChangeGameState(GameState.Setup);
    }
}

//TODO: move to its own file
public class GameRoundData
{
    public static readonly int numberOfChests = 9;
    public float BetAmount { get; set; }
    public int RoundMultiplier { get; set; }
    public float TotalWinnings { get; set; }
    public Queue<float> WinAmounts { get; } = new Queue<float>();

    //TODO: add reset function to clear list of win amounts

    public override string ToString()
    {
        string output = "Bet: " + BetAmount + "\n";
        output += "Round Multiplier: " + RoundMultiplier + "\n";
        output += "Total Winnings: " + TotalWinnings + "\n";
        output += "Win Amounts: ";
        foreach (var winAmount in WinAmounts)
        {
            output += winAmount + ", ";
        }
        return output;
    }

}
