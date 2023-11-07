using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour, IGameStateObserver
{
    bool isOpen = false;

    public void OnGameStateChange(GameState newState)
    {
        if (newState == GameState.Playing)
        {
            isOpen = false;
            //TODO: trigger animation
        }
    }

    public void Open(GameObject contents)
    {
        if (isOpen) return;
        isOpen = true; //TODO: remove once animatin event is added
        Debug.Log("Chest Opened: " + name);
        //TODO: trigger animation that reveals the contents
    }

    //TODO: add animation event that tells the GameplayController a chest is done opening
}
