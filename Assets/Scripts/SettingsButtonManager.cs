using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonManager : MonoBehaviour
{
    [SerializeField] GameObject muteBar;
    [SerializeField] GameObject vibrateBar;
    private AudioSource musicSource;

    private void Awake()
    {   
        musicSource = AudioManager.Instance.GetAudioSource();
        LoadingMute();
    }
    public void MuteMusic()
    {   
        musicSource.mute = !musicSource.mute;
        PlayerPrefs.SetString(PlayerPrefKey.isMute, musicSource.mute ? PlayerPrefKey.mute : PlayerPrefKey.unmute);
        LoadingMute();
    }

    private void LoadingMute()
    {
        muteBar.SetActive(musicSource.mute);
    }

    public void ToggleVibrate()
    {
        vibrateBar.SetActive(true);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        SceneController.Instance.LoadScene(SceneName.start);
    }
}
