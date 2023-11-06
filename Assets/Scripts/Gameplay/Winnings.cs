using System;
using UnityEngine;
using UnityEngine.Assertions;

public static class Winnings
{
    private static float minimumIncrement = 0.05f;

    //TODO: fine tune winning splits. Should be variable and emphasize a feeling of excitement.
    public static void SplitWinnings(GameRoundData gameRoundData)
    {
        int numberOfIncrements = Mathf.FloorToInt(gameRoundData.TotalWinnings / minimumIncrement);

        Assert.IsTrue(
            Mathf.Approximately(gameRoundData.TotalWinnings, numberOfIncrements * minimumIncrement),
            "Invalid Number of Increments: Inconsitent total winnings");

        //TODO: Update the code below vary the win amounts across the chests
        int largestDivisor = LargestIntegerDivisorInRange(numberOfIncrements, 1, GameRoundData.numberOfChests - 1);
        float winningsPerChest = numberOfIncrements / largestDivisor * minimumIncrement;
        Debug.Log("largest Divisor: " + largestDivisor);
        Debug.Log("Winnings per Chest: " + numberOfIncrements);
        Debug.Log("Total winnings: " + gameRoundData.TotalWinnings);
        Assert.IsTrue(
            Mathf.Approximately(gameRoundData.TotalWinnings, winningsPerChest * largestDivisor),
            "Invalid Winnings Split: Inconsistent total winnings");

        for (int i = 0; i < largestDivisor; i++)
        {
            gameRoundData.WinAmounts.Add(winningsPerChest);
        }
    }

    static int LargestIntegerDivisorInRange(int numerator, int lowerLimit, int upperLimit)
    {
        Assert.IsTrue(lowerLimit <= upperLimit, "Lower limit must be less than or equal to upper limit");
        Assert.IsTrue(lowerLimit > 0, "Lower limit must be greater than 0");

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
