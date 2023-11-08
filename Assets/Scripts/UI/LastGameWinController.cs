using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LastGameWinController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI displayText;
    float currentWinnings = 0;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    public void UpdateWinnings(float winnings)
    {
        currentWinnings += winnings;
        displayText.text = currentWinnings.ToString("C2");
    }

    public void Reset()
    {
        currentWinnings = 0;
        displayText.text = currentWinnings.ToString("C2");
    }
}
