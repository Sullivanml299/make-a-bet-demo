using System.Collections;
using System.Collections.Generic;
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

        //TODO: divide number of increments across some number of chests < total number of chests-1

        // for (int i = 0; i < GameRoundData.numberOfChests; i++)
        // {
        //     gameRoundData.WinAmounts.Add(winningsPerChest);
        // }
    }
}
