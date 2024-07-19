using UnityEngine;

public class LevelButtonManager : MonoBehaviour
{

    public void OnHomeButtonClick()
    {
        SceneController.Instance.LoadScene(SceneName.home);
    }

    public void OnBackButtonClick()
    {
        SceneController.Instance.LoadPreviousScene();
    }

    public void OnReloadButtonClick()
    {
        SceneController.Instance.ReloadCurrentScene();
    }

    public void OnNextLevelButtonClick()
    {
        int currSceneIndex = SceneController.Instance.GetCurrentSceneIndex();
        int maxLevel = PlayerPrefs.GetInt(PlayerPrefKey.maxLevel, 1);
        if(currSceneIndex == maxLevel)
            SceneController.Instance.LoadScene(SceneName.home);
        else
            SceneController.Instance.LoadScene(currSceneIndex + 1);
    }
}
