using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Level3 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private const int LevelIndex = 3;
    private float mouseStartPosY;
    private const float DragThreshold = 25f;
    private float dragLength;
    private int eyelidUpperIndex, eyelidLowerIndex;
    private int maxEyelidIndex, minEyelidIndex;
    private Canvas canvas;

    [SerializeField] private List<Sprite> eyelidSprites;

    [Header("Animation")]
    [SerializeField] private Animator levelCompletedAnimator;

    private void Awake()
    {
        maxEyelidIndex = eyelidSprites.Count - 1;
        minEyelidIndex = 0;

        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseStartPosY = GetMouseWorldPosition(eventData).y;
    }

    
    public void OnDrag(PointerEventData eventData)
    {
        if (GetMouseWorldPosition(eventData).y > mouseStartPosY)
        {
            PullEyelidUp(eventData, mouseStartPosY);
        }
        else if (GetMouseWorldPosition(eventData).y < mouseStartPosY)
        {
            PullEyelidDown(eventData, mouseStartPosY);
        }

        if (IsLevelCompleted())
            CompleteLevel();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    private void CompleteLevel()
    {
        gameObject.SetActive(false);
        levelCompletedAnimator.SetTrigger("IsCompleted");
        UnlockManager.Instance.UnlockLevel(LevelIndex);
    }

    private void PullEyelidUp(PointerEventData eventData, float mouseStartPosY)
    {
        dragLength = Mathf.Abs(GetMouseWorldPosition(eventData).y - mouseStartPosY);
        eyelidUpperIndex = Mathf.Min(eyelidLowerIndex + (int)(dragLength / DragThreshold), maxEyelidIndex);
        GetComponent<Image>().sprite = eyelidSprites[eyelidUpperIndex];
    }

    private void PullEyelidDown(PointerEventData eventData, float mouseStartPosY)
    {
        dragLength = Mathf.Abs(GetMouseWorldPosition(eventData).y - mouseStartPosY);
        eyelidLowerIndex = Mathf.Max(minEyelidIndex, eyelidUpperIndex - (int)(dragLength / DragThreshold));
        GetComponent<Image>().sprite = eyelidSprites[eyelidLowerIndex];
    }

    private bool IsLevelCompleted()
    {
        return eyelidUpperIndex == maxEyelidIndex;
    }

    private Vector2 GetMouseWorldPosition(PointerEventData eventData)
    {
        return eventData.position / canvas.scaleFactor;
    }
}
