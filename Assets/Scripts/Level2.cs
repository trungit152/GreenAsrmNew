using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Level2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private const int LevelIndex = 2;
    private const float DegreesPerSecond = 30f;
    private const float CompletionAngle = -19f;
    private float previousMousePositionX, currentMousePositionX;
    private float threshold = 10f;
    private Canvas canvas;
    private RectTransform rectTransform;

    [Header("Completion Elements")]
    [SerializeField] private GameObject happyPicture;

    [Header("Animation")]
    [SerializeField] private Animator levelCompletedAnimator;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        previousMousePositionX = GetMouseWorldPosition(eventData).x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentMousePositionX = GetMouseWorldPosition(eventData).x;

        if (previousMousePositionX - currentMousePositionX < -threshold)
        {
            RotatePicture(DegreesPerSecond * Time.deltaTime);
        }
        else if (previousMousePositionX - currentMousePositionX > threshold)
        {
            RotatePicture(-DegreesPerSecond * Time.deltaTime);
        }

        previousMousePositionX = currentMousePositionX;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsLevelCompleted())
        {
            CompleteLevel();
        }
    }

    private void RotatePicture(float rotationAmount)
    {
        rectTransform.Rotate(Vector3.forward, rotationAmount);
    }

    private void CompleteLevel()
    {
        gameObject.SetActive(false);
        happyPicture.SetActive(true);
        levelCompletedAnimator.SetTrigger("IsCompleted");
        UnlockManager.Instance.UnlockLevel(LevelIndex);
    }

    private float ConvertRadToDeg(float radRotation)
    {
        return Mathf.Asin(radRotation) * 360 / Mathf.PI;
    }

    private Vector2 GetMouseWorldPosition(PointerEventData eventData)
    {
        return eventData.position / canvas.scaleFactor;
    }

    private bool IsLevelCompleted()
    {
        float currentAngle = ConvertRadToDeg(rectTransform.rotation.z);
        return Mathf.Abs(currentAngle - CompletionAngle) < 2;
    }
}
