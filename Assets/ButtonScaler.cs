using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScaler : MonoBehaviour
{
    [SerializeField] private CanvasScaler canvasScaler;

    private void Awake()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            ScaleWithScreen(button);
            Debug.Log(button.name);
        }
    }

    private void ScaleWithScreen(Button button)
    {
        if (canvasScaler == null || canvasScaler.uiScaleMode != CanvasScaler.ScaleMode.ScaleWithScreenSize)
            return;

        Vector3 currenScale = button.GetComponent<RectTransform>().localScale;
        button.GetComponent<RectTransform>().localScale = new Vector3(currenScale.x / CalculateScale(), currenScale.y / CalculateScale(), currenScale.z);
        Debug.Log(currenScale.x / CalculateScale());
    }

    public float CalculateScale()
    {
        //For match = 1
        Vector2 referenceResolution = canvasScaler.referenceResolution;
        float scaleWidth = Screen.width / referenceResolution.x;
        float scaleHeight = Screen.height / referenceResolution.y;
        return scaleHeight / scaleWidth;
    }
}
