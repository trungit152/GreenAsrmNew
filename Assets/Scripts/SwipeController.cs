using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    private int currentPage, unlockedPage;
    private Vector3 targetPosition, pageStep;
    private float dragThreshold;

    [Header("Scroll Page")]
    [SerializeField] private RectTransform pageManagerRect;
    [SerializeField] private GameObject page;
    [SerializeField] private float tweenTime;
    [SerializeField] private LeanTweenType tweenType;

    [Header("Pagination Bar")]
    [SerializeField] private Sprite dotClosed, dotOpen;
    [SerializeField] private GameObject paginationBar;
    [SerializeField] private GameObject paginationDot;

    private void Awake()
    {
        currentPage = 1;
        dragThreshold = Screen.width / 20;

        unlockedPage = PlayerPrefs.GetInt(PlayerPrefKey.unlockedPage, 1);
        LoadUnlockedPage(unlockedPage);
        UpdateLevelBar();
    }

    public void NextPage()
    {
        if (currentPage < unlockedPage)
        {
            currentPage++;
            targetPosition += pageStep;
            MoveToPage();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPosition -= pageStep;
            MoveToPage();
        }
    }

    private void MoveToPage()
    {
        pageManagerRect.LeanMoveLocal(targetPosition, tweenTime).setEase(tweenType);
        UpdateLevelBar();
    }

    private void LoadUnlockedPage(int unlockedPage)
    {
        float widthScale = page.GetComponent<Page>().CalculateScale();
        pageStep = new Vector3(-Screen.width / widthScale, 0, 0);
        pageManagerRect.localPosition = new Vector3(pageManagerRect.localPosition.x / widthScale,
                                                    pageManagerRect.localPosition.y, pageManagerRect.localPosition.z);
        targetPosition = pageManagerRect.localPosition + pageStep * (unlockedPage - 1);
        pageManagerRect.LeanMoveLocal(targetPosition, Time.deltaTime);
        currentPage = unlockedPage;
        UpdateLevelBar();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshold)
        {
            if (eventData.position.x > eventData.pressPosition.x)
                PreviousPage();
            else
                NextPage();
        }
        else
        {
            MoveToPage();
        }
    }

    private void UpdateLevelBar()
    {
        int childCount = paginationBar.transform.childCount;
        if (unlockedPage > childCount)
        {
            CreateLevelDots();
            childCount = unlockedPage;
        }

        for (int i = 0; i < childCount; i++)
        {
            Transform levelDot = paginationBar.transform.GetChild(i);
            if (i == currentPage - 1)
            {
                levelDot.GetComponent<Image>().sprite = dotOpen;
                levelDot.GetComponentInChildren<Text>().text = (currentPage).ToString();
            }
            else
            {
                levelDot.GetComponent<Image>().sprite = dotClosed;
                levelDot.GetComponentInChildren<Text>().text = "";
            }
        }
    }

    private void CreateLevelDots()
    {
        for (int i = 0; i < unlockedPage; i++)
        {
            GameObject newLevelDot = Instantiate(paginationDot);
            newLevelDot.name = (unlockedPage).ToString();
            newLevelDot.transform.SetParent(paginationBar.transform, false);
        }
    }
}
