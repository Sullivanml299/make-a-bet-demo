using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DenominationController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI displayText;
    [SerializeField]
    private Button increaseButton, decreaseButton;
    private float[] denominations = new float[] { 0.25f, 0.50f, 1.00f, 5.00f };
    private int currentIndex = 0;

    void Start()
    {
        UpdateText();
        UpdateButtons();
        GameplayController.Instance.SetBetAmount(denominations[currentIndex]);
    }

    public void SetInteractable(bool IsInteractable)
    {
        if (IsInteractable) UpdateButtons();
        else
        {
            increaseButton.interactable = false;
            decreaseButton.interactable = false;
        }
    }

    public void ChangeDenomination(bool increase)
    {
        if (increase && currentIndex < denominations.Length - 1) currentIndex++;
        else if (!increase && currentIndex > 0) currentIndex--;
        UpdateText();
        GameplayController.Instance.SetBetAmount(denominations[currentIndex]);
    }
    private void UpdateButtons()
    {
        increaseButton.interactable = IsInteractable(true);
        decreaseButton.interactable = IsInteractable(false);
    }

    private void UpdateText()
    {
        displayText.text = denominations[currentIndex].ToString("C2");
        UpdateButtons();
    }

    private bool IsInteractable(bool increase)
    {
        if (increase && currentIndex < denominations.Length - 1) return true;
        else if (!increase && currentIndex > 0) return true;
        return false;
    }
}
