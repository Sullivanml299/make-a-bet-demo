using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CoinDropController : MonoBehaviour
{
    [SerializeField] private VisualEffect coinDropVFX;

    public void TriggerCoinDrop()
    {
        coinDropVFX.SendEvent("OnDrop");
    }
}
