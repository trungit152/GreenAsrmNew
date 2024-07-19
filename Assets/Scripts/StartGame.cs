using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartGame : MonoBehaviour, IPointerUpHandler, IDragHandler
{
    public Slider slider;
    public Animator sliderAnim;

    [SerializeField] AnimationClip cleanDirty;

    private void Start()
    {
        if (slider != null)
        {
            //slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        sliderAnim.speed = 0;

        slider.maxValue /= 1.5f;
    }

    private void OnDestroy()
    {
        if (slider != null)
        {
            //slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        sliderAnim.Play(cleanDirty.name, 0, value);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StartCoroutine(ResetSlider(slider.value));
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (slider.value == slider.maxValue)
        {
            SceneController.Instance.LoadScene(SceneName.home);
        }
    }


    private IEnumerator ResetSlider(float value)
    {
        float step = 0.05f;
        while (value > 0)
        {
            value -= step;
            slider.value = value;
            yield return null;
        }
    }
}
