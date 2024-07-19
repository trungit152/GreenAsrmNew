using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Level1 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private const int LevelIndex = 1;
    private float left, right, top, bottom;
    private Vector2 targetPosition;
    private bool isDragging = false, isLevelCompleted = false;
    private Canvas canvas;
    private Coroutine currentLerpCoroutine;
    private RectTransform rectTransform;

    [SerializeField] private float duration = 0.2f;
    [SerializeField] private Image background;
    [SerializeField] private Sprite completionBackground;
    [SerializeField] private RectTransform rectangle;

    [Header("Animation")]
    [SerializeField] private Animator levelCompletedAnimator;



    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        targetPosition = rectTransform.anchoredPosition;

        CalculateEdges();
    }

    private void CalculateEdges()
    {
        left = rectangle.anchoredPosition.x - rectangle.sizeDelta.x * rectangle.pivot.x;
        right = rectangle.anchoredPosition.x + rectangle.sizeDelta.x * (1 - rectangle.pivot.x);
        top = rectangle.anchoredPosition.y + rectangle.sizeDelta.y * (1 - rectangle.pivot.y);
        bottom = rectangle.anchoredPosition.y - rectangle.sizeDelta.y * rectangle.pivot.y;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;

        if (currentLerpCoroutine != null)
        {
            StopCoroutine(currentLerpCoroutine);
            currentLerpCoroutine = null;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        if (isLevelCompleted)
        {
            UnlockManager.Instance.UnlockLevel(LevelIndex);
        }
        else
        {
            currentLerpCoroutine = StartCoroutine(ReturnPosition(targetPosition, duration));
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            HandleDragging(eventData);
        }

        if (IsLevelCompletion())
        {
            CompleteLevel();
        }
    }

    private void HandleDragging(PointerEventData eventData)
    {
        Vector2 newPosition = rectTransform.anchoredPosition + eventData.delta / canvas.scaleFactor;

        newPosition.x = Mathf.Clamp(newPosition.x, left, right);
        newPosition.y = Mathf.Clamp(newPosition.y, bottom, top);

        rectTransform.anchoredPosition = newPosition;
    }


    private IEnumerator ReturnPosition(Vector2 targetPosition, float duration)
    {
        float time = 0;
        Vector2 startPosition = rectTransform.anchoredPosition;

        while (rectTransform.anchoredPosition != targetPosition)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        currentLerpCoroutine = null;
    }

    private void CompleteLevel()
    {
        isLevelCompleted = true;
        background.sprite = completionBackground;
        GetComponent<Image>().raycastTarget = false;
        StartCoroutine(ReturnPosition(targetPosition, duration));
        levelCompletedAnimator.SetTrigger("IsCompleted");
    }

    private bool IsLevelCompletion()
    {
        return Mathf.Approximately(rectTransform.anchoredPosition.y, bottom);
    }
}
