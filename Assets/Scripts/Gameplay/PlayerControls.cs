using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    TreasureChest chest;
    RaycastHit hit;
    Ray ray;

    public void OnSelect()
    {
        //TODO: don't bother raycasting if not in Playing state
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out chest))
            {
                GameplayController.Instance.OpenChest(chest);
            }
        }
    }
}
