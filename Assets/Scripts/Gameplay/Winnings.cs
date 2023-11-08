using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public static class Winnings
{
    private static float minimumIncrement = 0.05f;

    //TODO: fine tune winning splits. Should be variable and emphasize a feeling of excitement.
    public static void SplitWinnings(GameRoundData gameRoundData)
    {
        int numberOfIncrements = Mathf.FloorToInt(gameRoundData.TotalWinnings / minimumIncrement);
        if (numberOfIncrements == 0) return;

        Assert.IsTrue(
            Mathf.Approximately(gameRoundData.TotalWinnings, numberOfIncrements * minimumIncrement),
            "Invalid Number of Increments: Inconsitent total winnings");

        //TODO: Update the code below vary the win amounts across the chests
        // EvenSplit(numberOfIncrements, gameRoundData);
        RandomSplit(numberOfIncrements, gameRoundData);
    }

    //TODO: Scales poorly. Just a placeholder for testing purposes.
    static void RandomSplit(int numberOfIncrements, GameRoundData gameRoundData)
    {
        int numberOfChests = Random.Range(1, GameRoundData.numberOfChests + 1);
        int incrementsRemaining = numberOfIncrements;
        int incrementsInChest;
        List<float> winAmounts = new List<float>();

        for (int i = 0; i < numberOfChests - 1; i++)
        {
            incrementsInChest = Random.Range(0, incrementsRemaining);
            incrementsRemaining -= incrementsInChest;
            winAmounts.Add(incrementsInChest * minimumIncrement);
        }
        winAmounts.Add(incrementsRemaining * minimumIncrement);
        winAmounts.Sort();

        foreach (var winAmount in winAmounts)
        {
            gameRoundData.WinAmounts.Enqueue(winAmount);
        }
    }

    static void EvenSplit(int numberOfIncrements, GameRoundData gameRoundData)
    {
        int largestDivisor = LargestIntegerDivisorInRange(numberOfIncrements, 1, GameRoundData.numberOfChests - 1);
        float winningsPerChest = numberOfIncrements / largestDivisor * minimumIncrement;
        Debug.Log("largest Divisor: " + largestDivisor);
        Debug.Log("Winning Increments per Chest: " + numberOfIncrements);
        Debug.Log("Total winnings: " + gameRoundData.TotalWinnings);
        Assert.IsTrue(
            Mathf.Approximately(gameRoundData.TotalWinnings, winningsPerChest * largestDivisor),
            "Invalid Winnings Split: Inconsistent total winnings");

        for (int i = 0; i < largestDivisor; i++)
        {
            gameRoundData.WinAmounts.Enqueue(winningsPerChest);
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

        throw new System.ArgumentException("No divisor found in the given range");
    }
}
