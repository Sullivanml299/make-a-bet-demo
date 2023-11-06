using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRound : MonoBehaviour
{
    public float betAmount; //TODO: find a good way to populate this
    private GameRoundData gameRoundData;

    void Start()
    {
        gameRoundData = new GameRoundData();
    }

    public void StartRound()
    {
        gameRoundData.RoundMultiplier = Multiplier.GetRandomMultplier();
        gameRoundData.TotalWinnings = gameRoundData.RoundMultiplier * betAmount;
        Winnings.SplitWinnings(gameRoundData);
    }

}

//TODO: move to its own file
public class GameRoundData
{
    public static readonly int numberOfChests = 9;
    public int RoundMultiplier { get; set; }
    public float TotalWinnings { get; set; }
    public List<float> WinAmounts { get; } = new List<float>(numberOfChests);

    //TODO: add reset function
}
