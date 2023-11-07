using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour, IGameStateObserver
{
    public float moveTime = 0.2f;
    public float animationDistanceFromCamera = 1.5f;
    public float scaleMultiplier = 2f;
    public bool modelZIsUp = true;
    public bool modelOriginIsBottom = true;
    private Animator animator;
    float verticalOffset;
    bool isOpen = false;

    void Start()
    {
        if (modelOriginIsBottom)
        {
            verticalOffset = modelZIsUp
                            ? -GetComponent<MeshCollider>().bounds.extents.z * transform.localScale.z
                            : -GetComponent<MeshCollider>().bounds.extents.y * transform.localScale.y;
        }
        animator = GetComponentInChildren<Animator>();
    }

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
        StartCoroutine(AnimationSetup());
    }

    //TODO: add animation event that tells the GameplayController a chest is done opening

    IEnumerator AnimationSetup()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = Camera.main.transform.position
                                + verticalOffset * Camera.main.transform.up
                                + Camera.main.transform.forward
                                * animationDistanceFromCamera;
        Vector3 currentScale = transform.localScale;
        Vector3 targetScale = currentScale * scaleMultiplier;

        float time = 0f;
        while (time < moveTime)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(currentPosition, targetPosition, time / moveTime);
            transform.localScale = Vector3.Lerp(currentScale, targetScale, time / moveTime);
            yield return null;
        }
        animator.SetTrigger("Open");
    }
}
