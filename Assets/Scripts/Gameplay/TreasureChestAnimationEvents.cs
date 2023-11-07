using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestAnimationEvents : MonoBehaviour
{
    public TreasureChest chest;

    void Start()
    {
        chest = GetComponentInParent<TreasureChest>();
    }

    public void OnEndOpen()
    {
        chest.EndOpen();
    }

}
