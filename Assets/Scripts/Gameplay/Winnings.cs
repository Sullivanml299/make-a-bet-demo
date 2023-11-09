using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public static class Winnings
{
    private static float minimumIncrement = 0.05f;

    public static void SplitWinnings(GameRoundData gameRoundData)
    {
        int numberOfIncrements = Mathf.FloorToInt(gameRoundData.TotalWinnings / minimumIncrement);
        if (numberOfIncrements == 0) return;

        Assert.IsTrue(
            Mathf.Approximately(gameRoundData.TotalWinnings, numberOfIncrements * minimumIncrement),
            "Invalid Number of Increments: Inconsitent total winnings");

        // EvenSplit(numberOfIncrements, GameRoundData.numberOfChests - 1, gameRoundData.WinningsQueue);
        RandomSplit(numberOfIncrements, GameRoundData.numberOfChests - 1, gameRoundData.WinningsQueue);

        for (var i = 0; i < 100; i++)
        {
            int numberOfChests = Random.Range(1, Mathf.Min(numberOfIncrements + 1, GameRoundData.numberOfChests));
            GetRandomPartition(Random.Range(numberOfChests, numberOfChests + 1), numberOfChests);
        }
    }

    static void RandomSplit(int numberOfIncrements, int chestsAvailable, Queue<float> winningsQueue)
    {
        int numberOfChests = Random.Range(1, Mathf.Min(numberOfIncrements + 1, chestsAvailable + 1));
        int[] splits = GetRandomPartition(numberOfIncrements, numberOfChests);
        float totalWinAmount = 0;

        foreach (var split in splits)
        {
            winningsQueue.Enqueue(split * minimumIncrement);
            totalWinAmount += split * minimumIncrement;
        }

        Assert.IsTrue(
            Mathf.Approximately(totalWinAmount, numberOfIncrements * minimumIncrement),
            "Invalid Number of Increments: Inconsitent total winnings");
    }

    static int[] GetRandomPartition(int T, int n)
    {
        if (n == 1) return new int[] { T };
        if (n > T)
        {
            n = T;
            Debug.LogWarning("Number of partitions is greater than total value. Setting number of partitions to total value.");
        }

        Debug.Log($"Random ascending partition of {T} into {n}");
        List<int> numbers = Enumerable.Range(1, T - 1).ToList();
        System.Random random = new System.Random();

        // Shuffle the first n - 1 numbers
        for (int i = 0; i < n - 1; i++)
        {
            int randomIndex = random.Next(i, T - 1);
            int temp = numbers[i];
            numbers[i] = numbers[randomIndex];
            numbers[randomIndex] = temp;
        }

        // Take the first n - 1 numbers as the split points
        List<int> partitionList = numbers.Take(n - 1).ToList();

        // Sort the split points in ascending order
        partitionList.Sort();

        // Calculate the differences between consecutive split points
        int[] result = new int[n];
        result[0] = partitionList[0];
        for (int i = 1; i < n - 1; i++)
        {
            result[i] = partitionList[i] - partitionList[i - 1];
        }
        result[n - 1] = T - partitionList[n - 2];

        System.Array.Sort(result);

        Debug.Log($"Random ascending partition of {T} into {n} values: [{string.Join(", ", result)}] partition: [{string.Join(", ", partitionList)}]");

        return result;
    }

    //TODO: Come back to this. Average time is far more efficient, but worst case could technically be infinite
    // static int[] GetRandomPartition(int T, int n)
    // {
    //     if (n == 1) return new int[] { T };
    //     if (n > T)
    //     {
    //         n = T;
    //         Debug.LogWarning("Number of partitions is greater than total value. Setting number of partitions to total value.");
    //     }

    //     Debug.Log($"Random ascending partition of {T} into {n}");
    //     HashSet<int> partitionSet = new HashSet<int>();
    //     List<int> partitionList;

    //     while (partitionSet.Count < n - 1)
    //     {
    //         partitionSet.Add(Random.Range(1, T));
    //     }

    //     // Take the first n - 1 numbers as the split points
    //     partitionList = partitionSet.ToList();

    //     // Sort the split points in ascending order
    //     partitionList.Sort();

    //     // Calculate the differences between consecutive split points
    //     int[] result = new int[n];
    //     result[0] = partitionList[0];
    //     for (int i = 1; i < n - 1; i++)
    //     {
    //         result[i] = partitionList[i] - partitionList[i - 1];
    //     }
    //     result[n - 1] = T - partitionList[n - 2];

    //     System.Array.Sort(result);

    //     Debug.Log($"Random ascending partition of {T} into {n} values: [{string.Join(", ", result)}] partition: [{string.Join(", ", partitionList)}]");

    //     return result;
    // }

    static void EvenSplit(int numberOfIncrements, int chestsAvailable, Queue<float> winningsQueue)
    {
        int largestDivisor = LargestIntegerDivisorInRange(numberOfIncrements, 1, chestsAvailable);
        float winningsPerChest = numberOfIncrements / largestDivisor * minimumIncrement;
        for (int i = 0; i < largestDivisor; i++)
        {
            winningsQueue.Enqueue(winningsPerChest);
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
