using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public static class Multiplier
{
    private static List<WeightedOption> multipliers = new List<WeightedOption>
    {
        new WeightedOption { MultiplierArray = new int[]{0}, Weight = 50 },
        new WeightedOption { MultiplierArray = new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, Weight = 30 },
        new WeightedOption { MultiplierArray =new int[] {12, 16, 24, 32, 48, 64}, Weight = 15 },
        new WeightedOption { MultiplierArray = new int[]{100, 200, 300, 400, 500}, Weight = 5 }
    };
    private static int totalWeight = multipliers.Sum(m => m.Weight);


    public static int GetRandomMultplier()
    {
        Assert.IsTrue(totalWeight == 100, "Total weight of multipliers must be 100"); //TODO: Only needs to check once
        int randomNumber = Random.Range(0, totalWeight);

        foreach (var multiplier in multipliers)
        {
            if (randomNumber <= multiplier.Weight)
            {
                return multiplier.MultiplierArray[Random.Range(0, multiplier.MultiplierArray.Length)];
            }
            randomNumber -= multiplier.Weight;
        }

        throw new System.InvalidOperationException("Unexpected error in GetRandomMultiplier");
    }
}

internal struct WeightedOption
{
    public int[] MultiplierArray { get; set; }
    public int Weight { get; set; }
}