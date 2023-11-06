using System;
using UnityEngine;
using UnityEngine.Assertions;

public static class Winnings
{
    private static float winningIncrement = 0.05f;

    //TODO: fine tune winning splits. Should be variable and emphasize a feeling of excitement.
    public static void SplitWinnings(GameRoundData gameRoundData)
    {
        int numberOfIncrements = (int)(gameRoundData.TotalWinnings / winningIncrement);
        Assert.IsTrue(
            Mathf.Approximately(gameRoundData.TotalWinnings, numberOfIncrements * winningIncrement),
            "Inconsitent total winnings");

        //TODO: Update the code below vary the win amounts across the chests
        int largestDivisor = LargestIntegerDivisorInRange(numberOfIncrements, 1, GameRoundData.numberOfChests - 1);
        float winningsPerChest = numberOfIncrements / largestDivisor;

        Assert.IsTrue(
            Mathf.Approximately(gameRoundData.TotalWinnings, winningsPerChest * largestDivisor),
            "Inconsistent total winnings");

        for (int i = 0; i < largestDivisor; i++)
        {
            gameRoundData.WinAmounts.Add(winningsPerChest);
        }
    }

    static int LargestIntegerDivisorInRange(int numerator, int lowerLimit, int upperLimit)
    {
        for (int i = upperLimit; i >= lowerLimit; i--)
        {
            if (numerator % i == 0)
            {
                return i;
            }
        }

        throw new ArgumentException("No divisor found in the given range");
    }
}
