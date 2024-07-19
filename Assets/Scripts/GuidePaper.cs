using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuidePaper : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Animator guideAnim;
    private CanvasScaler canvasScaler;

    private void Awake()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag(Tag.backgroundCanvas);
        Debug.Log(Screen.width + " " + Screen.height);
        if (canvas == null || Screen.width > Screen.height) return;

        canvasScaler = canvas.GetComponent<CanvasScaler>();
        ScaleObject();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        guideAnim.SetTrigger("VisiblePaper");
    }

    private void ScaleObject()
    {
        if (canvasScaler == null || canvasScaler.uiScaleMode != CanvasScaler.ScaleMode.ScaleWithScreenSize)
            return;

        Vector3 currenScale = transform.localScale;
        transform.localScale = new Vector3(currenScale.x / GetScale(), currenScale.y / GetScale(), currenScale.z);
    }
    public float GetScale()
    {
        //For match = 1
        Vector2 referenceResolution = canvasScaler.referenceResolution;
        float scaleWidth = Screen.width / referenceResolution.x;
        float scaleHeight = Screen.height / referenceResolution.y;
        return scaleHeight / scaleWidth;
    }
}
