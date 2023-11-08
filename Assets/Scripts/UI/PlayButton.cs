using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void SetInteractable(bool value)
    {
        button.interactable = value;
    }

    void OnClick()
    {
        GameStateManager.Instance.ChangeGameState(GameState.Playing);
    }
}
