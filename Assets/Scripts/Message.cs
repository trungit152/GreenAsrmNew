using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Message : MonoBehaviour
{
    public TMP_Text message;
    private CanvasScaler canvasScaler;
    float avatarX = 500f;

    void Start()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag(Tag.backgroundCanvas);
        if (canvas == null) return;
        
        canvasScaler = canvas.GetComponent<CanvasScaler>();
        MessageWidthScale();
        GetComponent<RectTransform>().SetAsLastSibling();
    }
    private void MessageWidthScale()
    {
        if (canvasScaler == null && canvasScaler.uiScaleMode != CanvasScaler.ScaleMode.ScaleWithScreenSize)
            return;

        float messageScale = Screen.width / GetScale() - avatarX;
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, messageScale);
    }

    public float GetScale()
    {
        // For match = 1
        Vector2 referenceResolution = canvasScaler.referenceResolution;
        float scale = Screen.height / referenceResolution.y;
        return scale;
    }
}
