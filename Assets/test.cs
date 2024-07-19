using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class test : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private const int LevelIndex = 4;
    private Vector2 offset, initialPosition;
    private Canvas canvas;
    private RectTransform rectTransform;
    private Collider2D brushCollider;

    [Header("Animation")]
    [SerializeField] private Animator levelCompletedAnimator;

    private void Awake()
    {
        brushCollider = GetComponent<Collider2D>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(rectTransform.anchoredPosition);
        rectTransform.SetAsLastSibling();
        initialPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        brushCollider.enabled = false;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Thực hiện raycast từ vị trí chuột
        RaycastHit2D hitInfo = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hitInfo.collider == null)
        {
            Debug.Log("null");
        }
        StartCoroutine(HandleMouseUp(hitInfo));
        brushCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

    }

    private IEnumerator HandleMouseUp(RaycastHit2D hitInfo)
    {
        if (hitInfo)
        {
            Debug.Log(hitInfo.transform.name);
            if (hitInfo.collider.CompareTag(Tag.brush))
            {
                RectTransform hitRect = hitInfo.transform.GetComponent<RectTransform>();
                Vector2 hitInitialPos = hitRect.anchoredPosition;
                yield return StartCoroutine(LerpPosition(hitRect, initialPosition, 0.15f));
                yield return StartCoroutine(LerpPosition(rectTransform, hitInitialPos, 0.04f));
            }
        }
        else
        {
            yield return StartCoroutine(LerpPosition(rectTransform, initialPosition, 0.15f));
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
        foreach (RectTransform childTransform in transform.parent)
        {
            childTransform.GetComponent<Image>().raycastTarget = false;
        }
    }

    private IEnumerator LerpPosition(RectTransform transform, Vector2 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.anchoredPosition;
        while (time < duration)
        {
            transform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }

    private Vector2 GetMouseWorldPosition(PointerEventData eventData)
    {
        return eventData.position / canvas.scaleFactor;
    }

    private bool CheckLevelCompletion()
    {
        RectTransform previousTransform = null;
        foreach (RectTransform currentTransform in transform.parent)
        {
            if (previousTransform != null)
            {
                if (previousTransform.anchoredPosition.y < currentTransform.anchoredPosition.y)
                {
                    return false;
                }
            }

            previousTransform = currentTransform;
        }

        return true;
    }
}
