using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayController : MonoBehaviour, IGameStateObserver
{
    public static GameplayController Instance { get; private set; }
    private GameRoundData gameRoundData = new GameRoundData();

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
        //TODO: add logic to disable selection while a chest is opening
        // Debug.Log("Chest Selected: " + chest.name);
        chest.Open(null);
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

    public void StartRound()
    {
        gameRoundData.RoundMultiplier = Multiplier.GetRandomMultplier();
        gameRoundData.TotalWinnings = gameRoundData.RoundMultiplier * gameRoundData.BetAmount;
        Winnings.SplitWinnings(gameRoundData);
        Debug.Log(gameRoundData);
    }


}

//TODO: move to its own file
public class GameRoundData
{
    public static readonly int numberOfChests = 9;
    public float BetAmount { get; set; }
    public int RoundMultiplier { get; set; }
    public float TotalWinnings { get; set; }
    public List<float> WinAmounts { get; } = new List<float>(numberOfChests);

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