using UnityEngine;
using UnityEngine.UI;

public class DenominationButton : MonoBehaviour
{
    public Button button;
    public DenominationController controller;
    private enum ButtonType { Increase, Decrease }
    [SerializeField] private ButtonType buttonType;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        controller = GetComponentInParent<DenominationController>();
    }

    void OnClick()
    {
        controller.ChangeDenomination(buttonType == ButtonType.Increase);
    }

}
