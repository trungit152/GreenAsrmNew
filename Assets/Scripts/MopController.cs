using System.Collections;
using UnityEngine;

public class MopController : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 dragOffset;

    private float minX, maxX;
    [SerializeField] private Collider2D dirtyAreaCollider;

    private const float LerpDuration = 0.2f;
    private Coroutine currentLerpCoroutine;

    private void Start()
    {
        minX = dirtyAreaCollider.bounds.min.x;
        maxX = dirtyAreaCollider.bounds.max.x;
        transform.position = new Vector3(minX, dirtyAreaCollider.transform.position.y, 0);
    }

    public void OnMouseDown()
    {
        dragOffset = transform.position - GetMouseWorldPosition();
        isDragging = true;

        if (currentLerpCoroutine != null)
        {
            StopCoroutine(currentLerpCoroutine);
            currentLerpCoroutine = null;
        }
    }

    public void OnMouseDrag()
    {
        if (isDragging) HandleDragging();

        if (IsAtMaxXPosition())
        {
            SceneController.Instance.LoadScene(SceneName.home);
        }
    }

    public void OnMouseUp()
    {
        isDragging = false;

        if (!IsAtMaxXPosition()) StartLerpToMinX();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    private void HandleDragging()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 targetPosition = mousePosition + dragOffset;
        float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    private void StartLerpToMinX()
    {
        Vector3 targetPosition = new Vector3(minX, transform.position.y, transform.position.z);
        currentLerpCoroutine = StartCoroutine(LerpPosition(targetPosition, LerpDuration));
    }

    private IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float timer = 0;
        Vector3 startPosition = transform.position;

        while (transform.position != targetPosition)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        currentLerpCoroutine = null;
    }

    private bool IsAtMaxXPosition()
    {
        return Mathf.Approximately(transform.position.x, maxX);
    }
}
