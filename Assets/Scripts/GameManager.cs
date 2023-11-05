using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool debugPrint = false;
    private GameState gameState = GameState.Setup;
    private List<IGameStateObserver> observers = new List<IGameStateObserver>();

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public void RegisterObserver(IGameStateObserver observer)
    {
        if (debugPrint) Debug.Log("Registering observer: " + observer.ToString());
        observers.Add(observer);
    }

    public void UnregisterObserver(IGameStateObserver observer)
    {
        if (debugPrint) Debug.Log("Unregistering observer: " + observer.ToString());
        observers.Remove(observer);
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnGameStateChange(gameState);
        }
    }

    public void ChangeGameState(GameState newState)
    {
        if (debugPrint) Debug.Log("Changing game state to: " + newState.ToString());
        gameState = newState;
        NotifyObservers();
    }
}

public enum GameState
{
    Setup,
    Playing,
    PostGame
}