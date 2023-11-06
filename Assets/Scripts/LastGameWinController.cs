using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LastGameWinController : MonoBehaviour, IGameStateObserver
{
    public TextMeshProUGUI displayText;
    float currentWinnings = 0;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    public void OnGameStateChange(GameState newState)
    {
        if (newState == GameState.Playing) Reset();
    }

    public void UpdateWinnings(float winnings)
    {
        currentWinnings += winnings;
        displayText.text = currentWinnings.ToString("C2");
    }

    void Reset()
    {
        currentWinnings = 0;
        displayText.text = currentWinnings.ToString("C2");
    }
}
