using UnityEngine;

public class TreasureChestAnimationEvents : MonoBehaviour
{
    TreasureChest chest;

    void Start()
    {
        chest = GetComponentInParent<TreasureChest>();
    }

    public void OnEndOpen()
    {
        chest.EndOpen();
    }

}
