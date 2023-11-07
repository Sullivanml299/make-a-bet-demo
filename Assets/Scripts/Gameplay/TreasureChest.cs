using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour, IGameStateObserver
{
    public float moveTime = 0.2f;
    public float postAnimationWaitTime = 1f;
    public float preAnimationWaitTime = 0.5f;
    public float animationDistanceFromCamera = 1.5f;
    public float scaleMultiplier = 2f;
    public bool modelZIsUp = true;
    public bool modelOriginIsBottom = true;
    private Animator animator;
    private Vector3 startPosition, startScale;
    float verticalOffset;
    bool isOpen = false;

    void Start()
    {
        GameStateManager.Instance.RegisterObserver(this);
        startPosition = transform.position;
        startScale = transform.localScale;
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
        if (isOpen && newState == GameState.Playing)
        {
            isOpen = false;
            animator.SetTrigger("Close");
        }
    }

    public void Open(GameObject contents)
    {
        if (isOpen) return;
        isOpen = true;
        Debug.Log("Chest Opened: " + name);
        StartCoroutine(AnimationSetup());
    }

    public void EndOpen()
    {
        StartCoroutine(AnimationCleanup());
    }

    IEnumerator AnimationSetup()
    {
        Vector3 targetPosition = Camera.main.transform.position
                                + verticalOffset * Camera.main.transform.up
                                + Camera.main.transform.forward
                                * animationDistanceFromCamera;
        Vector3 targetScale = startScale * scaleMultiplier;

        float time = 0f;
        while (time < moveTime)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / moveTime);
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / moveTime);
            yield return null;
        }

        time = 0f;
        while (time < preAnimationWaitTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        animator.SetTrigger("Open");
    }

    IEnumerator AnimationCleanup()
    {
        float time = 0f;

        while (time < postAnimationWaitTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        time = 0f;
        Vector3 currentPosition = transform.position;
        Vector3 currentScale = transform.localScale;

        while (time < moveTime)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(currentPosition, startPosition, time / moveTime);
            transform.localScale = Vector3.Lerp(currentScale, startScale, time / moveTime);
            yield return null;
        }

        GameplayController.Instance.EndOpen();
    }
}
