using System.Collections.Generic;

public class GameRoundData
{
    public static readonly int numberOfChests = 9;
    public float BetAmount { get; set; } = 0;
    public int RoundMultiplier { get; set; }
    public float TotalWinnings { get; set; }
    public Queue<float> WinAmounts { get; } = new Queue<float>();

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