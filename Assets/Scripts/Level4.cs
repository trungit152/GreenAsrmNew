using System.Collections;
using UnityEngine;

public class Level4 : MonoBehaviour
{
    private const int LevelIndex = 4;
    private Collider2D brushCollider;
    private Vector3 offset, initialPosition;

    private Transform previousTransform;

    [Header("Animation")]
    [SerializeField] private Animator levelCompletedAnimator;

    private void Awake()
    {
        brushCollider = GetComponent<Collider2D>();
    }

    private void OnMouseDown()
    {
        initialPosition = transform.position;
        offset = initialPosition - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + offset;
    }

    private void OnMouseUp()
    {
        brushCollider.enabled = false;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.position);
        StartCoroutine(HandleMouseUp(hitInfo));
        brushCollider.enabled = true;
    }

    private IEnumerator HandleMouseUp(RaycastHit2D hitInfo)
    {
        if (hitInfo)
        {
            if (hitInfo.transform.CompareTag(Tag.brush))
            {
                Vector3 targetPosition = hitInfo.transform.position;
                yield return StartCoroutine(LerpPosition(hitInfo.transform, initialPosition, 0.15f));
                yield return StartCoroutine(LerpPosition(transform, targetPosition, 0.04f));
            }
        }
        else
        {
            yield return StartCoroutine(LerpPosition(transform, initialPosition, 0.15f));
        }

        if (CheckLevelCompletion())
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        levelCompletedAnimator.SetTrigger("IsCompleted");
        UnlockManager.Instance.UnlockLevel(LevelIndex);
        foreach (Transform childTransform in transform.parent)
        {
            childTransform.GetComponent<Collider2D>().enabled = false;
        }
    }

    private IEnumerator LerpPosition(Transform targetTransform, Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = targetTransform.position;
        while (targetTransform.position != targetPosition)
        {
            targetTransform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        targetTransform.position = targetPosition;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    private bool CheckLevelCompletion()
    {
        previousTransform = null;
        foreach (Transform currentTransform in transform.parent)
        {
            if (previousTransform != null)
            {
                if (previousTransform.position.y < currentTransform.position.y)
                {
                    return false;
                }
            }

            previousTransform = currentTransform;
        }

        return true;
    }
}
