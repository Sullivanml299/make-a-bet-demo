using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public bool debugPrint = false;
    private GameState gameState = GameState.Setup;
    private HashSet<IGameStateObserver> observers = new HashSet<IGameStateObserver>();

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public void RegisterObserver(IGameStateObserver observer)
    {
        observers.Add(observer);
    }

    public void UnregisterObserver(IGameStateObserver observer)
    {
        observers.Remove(observer);
    }

    public void ChangeGameState(GameState newState)
    {
        if (debugPrint) Debug.Log("Changing game state to: " + newState.ToString());
        gameState = newState;
        NotifyObservers();
    }
    private void NotifyObservers()
    {
        foreach (IGameStateObserver observer in observers) observer.OnGameStateChange(gameState);
    }


}

public enum GameState
{
    Setup,
    Playing,
}