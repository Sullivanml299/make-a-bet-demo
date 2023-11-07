using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DenominationController : MonoBehaviour, IGameStateObserver
{
    public TextMeshProUGUI displayText;
    public Button increaseButton, decreaseButton;
    private float[] denominations = new float[] { 0.25f, 0.50f, 1.00f, 5.00f };
    private int currentIndex = 0;

    void Start()
    {
        GameStateManager.Instance.RegisterObserver(this);
        UpdateText();
        UpdateButtons();
        GameplayController.Instance.SetBetAmount(denominations[currentIndex]);
    }

    public void ChangeDenomination(bool increase)
    {
        if (increase && currentIndex < denominations.Length - 1) currentIndex++;
        else if (!increase && currentIndex > 0) currentIndex--;
        UpdateText();
        GameplayController.Instance.SetBetAmount(denominations[currentIndex]);
    }

    public void OnGameStateChange(GameState newState)
    {
        if (newState != GameState.Setup)
        {
            increaseButton.interactable = false;
            decreaseButton.interactable = false;
        }
        else UpdateButtons();
    }

    private void UpdateText()
    {
        displayText.text = denominations[currentIndex].ToString("C2");
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        increaseButton.interactable = IsInteractable(true);
        decreaseButton.interactable = IsInteractable(false);
    }


    private bool IsInteractable(bool increase)
    {
        if (increase && currentIndex < denominations.Length - 1) return true;
        else if (!increase && currentIndex > 0) return true;
        return false;
    }
}
