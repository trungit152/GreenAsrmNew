using UnityEngine;


public class HomeButtonManager : MonoBehaviour
{
    public void OnChatButtonClick()
    {
        SceneController.Instance.LoadScene(SceneName.chat);
    }
    public void OnMusicButtonClick()
    {
        SceneController.Instance.LoadScene(SceneName.music);
    }

    public void OnPhotosButtonClick()
    {
        SceneController.Instance.LoadScene(SceneName.photo);
    }

    public void OnSettingButtonClick()
    {
        SceneController.Instance.LoadScene(SceneName.setting);
    }
    public void OnLevelButtonClick(int levelSceneIndex)
    {
        SceneController.Instance.LoadScene(levelSceneIndex);
    }
}
