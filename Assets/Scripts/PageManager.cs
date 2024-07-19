using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{   
    private List<GameObject> pages;
    private List<GameObject> buttons;

    private void Awake()
    {
        LoadPage();
        LoadLevelButton();
    }

    private void LoadPage()
    {
        int unlockedPage = PlayerPrefs.GetInt(PlayerPrefKey.unlockedPage, 1);

        for (int i = 0; i < unlockedPage; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);  
        }
    }

    private void LoadLevelButton()
    {
        int unlockedLevel = PlayerPrefs.GetInt(PlayerPrefKey.unlockedLevel, 1);
        int completedLevel = unlockedLevel - 1;
        int count = 0;

        foreach (Transform page in transform)
        {
            foreach (Transform button in page.transform)
            {
                if (count == unlockedLevel) return;

                button.gameObject.SetActive(true);
                if (count < completedLevel)
                {
                    button.GetComponent<Image>().sprite = button.GetComponent<Level>()
                                                                    .GetCompletedSprite();
                }

                count++;
            }
        }
    }
}
