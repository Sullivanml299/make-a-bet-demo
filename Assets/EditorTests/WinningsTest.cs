using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class WinningsTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void WinningsTestSimplePasses()
    {
        // Use the Assert class to test conditions
        GameRoundData gameRoundData = new GameRoundData();
        float[] totalWinnings = new float[] { 0f, 0.5f, 3f, 10f, 32f, 2500f };
        int iterations = 1000;
        float sum;

        foreach (var winnings in totalWinnings)
        {
            gameRoundData.TotalWinnings = winnings;
            for (var i = 0; i < iterations; i++)
            {
                Winnings.SplitWinnings(gameRoundData);
                sum = gameRoundData.WinningsQueue.Sum();
                Assert.IsTrue(Mathf.Approximately(sum, winnings));
                gameRoundData.WinningsQueue.Clear();
            }
        }
    }

}
