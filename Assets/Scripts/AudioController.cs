using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Slider seekBar;
    private AudioSource musicSource;
    private bool isDragging = false;

    private void Start()
    {
        seekBar = GetComponent<Slider>();
        seekBar.onValueChanged.AddListener(OnSliderValueChange);
        musicSource = AudioManager.Instance.GetAudioSource();
        if (musicSource.clip != null)
        {
            seekBar.maxValue = musicSource.clip.length;
        }
    }

    private void Update()
    {
        if (!isDragging)
        {
            seekBar.value = musicSource.time;
        }
    }

    void OnSliderValueChange(float value)
    {
        if (isDragging)
        {
            musicSource.time = value;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void Next()
    {
        AudioManager.Instance.NextAudio();
    }

    public void Previous()
    {
        AudioManager.Instance.PreviousAudio();
    }
}
