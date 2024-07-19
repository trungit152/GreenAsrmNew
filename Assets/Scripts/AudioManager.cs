using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]   
    [SerializeField] AudioSource musicSource;

    [Header("Audio Clip")]
    private int clipIndex;
    [SerializeField] AudioClip[] listAudioClip;

    public static AudioManager Instance { get; set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        clipIndex = 0;
        LoadingMute();
    }

    private void LoadingMute()
    {
        musicSource.mute = PlayerPrefs.GetString(Mute.keyMute, Mute.unmute) != Mute.unmute;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneName.sceneWithMusic.Contains(scene.name))
        { 
            PlayMusic();
        }
        else
            StopMusic();
    }

    public void PlayMusic()
    {
        AudioClip clip = listAudioClip[Mathf.Abs(clipIndex) % listAudioClip.Length];
        if (clip == null || (clip == musicSource.clip && musicSource.isPlaying)) return;

        musicSource.clip = clip;
        musicSource.Play();
    }

    void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();   
        }
    }

    public AudioSource GetAudioSource()
    {
        return musicSource;
    }

    public void NextAudio()
    {
        clipIndex++;
        PlayMusic();
    }

    public void PreviousAudio()
    {
        clipIndex--;
        PlayMusic();
    }
}
