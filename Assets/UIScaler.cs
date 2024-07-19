using UnityEngine;
using UnityEngine.UI;

public class UIScaler : MonoBehaviour
{   
    public float resolutionX, resolutionY;
    private CanvasScaler canvasScaler;
    void Start()
    {
        canvasScaler = GetComponent<CanvasScaler>();
        SetInfo();
    }

    void SetInfo()
    {
        resolutionX = Screen.currentResolution.width;
        resolutionY = Screen.currentResolution.height;

        canvasScaler.referenceResolution = new Vector2 (resolutionX, resolutionY);
    }
}
