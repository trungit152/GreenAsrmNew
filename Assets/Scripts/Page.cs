using UnityEngine;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    [SerializeField] private CanvasScaler canvasScaler;

    private void Awake()
    {
        AdjustPageWidth();
    }

    private void AdjustPageWidth()
    {
        if (canvasScaler == null || canvasScaler.uiScaleMode != CanvasScaler.ScaleMode.ScaleWithScreenSize)
            return;

        float scaleWidth = Screen.width / CalculateScale();
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scaleWidth);
    }

    public float CalculateScale()
    {   
        //For match = 1
        Vector2 referenceResolution = canvasScaler.referenceResolution;
        float scale = Screen.height / referenceResolution.y;
        return scale;
    }
}
