
using TMPro;
using UnityEngine;

public class PlayerBankController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;
    public float CurrentBalance { get; private set; } = 10; //TODO: consider moving this to it's own data class

    void Start()
    {
        UpdateBalance(0);
    }

    public void UpdateBalance(float amount)
    {
        CurrentBalance += amount;
        displayText.text = CurrentBalance.ToString("C2");
    }

}
