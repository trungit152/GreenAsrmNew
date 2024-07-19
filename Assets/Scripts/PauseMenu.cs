using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
    }

    public void Home()
    {
        SceneController.Instance.LoadScene("Home");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pauseMenu.SetActive(false);
    }
}
