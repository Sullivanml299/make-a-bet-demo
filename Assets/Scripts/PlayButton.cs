using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour, IGameStateObserver
{
    public Button button;
    public void OnGameStateChange(GameState newState)
    {
        button.interactable = newState == GameState.Setup;
    }

    void Start()
    {
        GameManager.Instance.RegisterObserver(this);
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        GameManager.Instance.ChangeGameState(GameState.Playing);
    }
}
