
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScaleController : MonoBehaviour
{
    private CanvasScaler canvasScaler;

    private void Awake()
    {
        SceneManager.sceneLoaded += Scale;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= Scale;
    }

    private void Scale(Scene scene, LoadSceneMode mode)
    {
        GameObject canvas = GameObject.FindGameObjectWithTag(Tag.backgroundCanvas);
        if (canvas == null || Screen.width > Screen.height) return;

        canvasScaler = canvas.GetComponent<CanvasScaler>();

        GameObject[] scaleObjects = GameObject.FindGameObjectsWithTag(Tag.scaleObject);
        GridLayoutGroup gridLayoutGroup;
        foreach (GameObject scaleObject in scaleObjects)
        {
            gridLayoutGroup = scaleObject?.GetComponent<GridLayoutGroup>();
            if (gridLayoutGroup != null)
            {
                ScaleGridLayout(gridLayoutGroup);
            }
            else
            {
                ScaleObject(scaleObject.transform);
            }
        }  
    }

    private void ScaleGridLayout(GridLayoutGroup gridLayoutGroup)
    { 
        gridLayoutGroup.cellSize = new Vector2(gridLayoutGroup.cellSize.x / GetScale(), gridLayoutGroup.cellSize.y / GetScale());
        gridLayoutGroup.spacing = new Vector2(gridLayoutGroup.spacing.x / GetScaleWithHeight(), gridLayoutGroup.spacing.y);
    }

    private void ScaleObject(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (canvasScaler == null || canvasScaler.uiScaleMode != CanvasScaler.ScaleMode.ScaleWithScreenSize)
                return;

            Vector3 currenScale = child.localScale;
            child.localScale = new Vector3(currenScale.x / GetScale(), currenScale.y / GetScale(), currenScale.z);
        }
    }

    public float GetScale()
    {
        //For match = 1
        Vector2 referenceResolution = canvasScaler.referenceResolution;
        float scaleWidth = Screen.width / referenceResolution.x;
        float scaleHeight = Screen.height / referenceResolution.y;
        return scaleHeight / scaleWidth;
    }

    public float GetScaleWithHeight()
    {
        //For match = 1
        Vector2 referenceResolution = canvasScaler.referenceResolution;
        float scale = Screen.height / referenceResolution.y;
        return scale;
    }
}
