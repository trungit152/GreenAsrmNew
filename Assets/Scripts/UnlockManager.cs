using UnityEngine;

public class UnlockManager : MonoBehaviour
{
    [SerializeField] private int maxPages;
    [SerializeField] private int maxLevels;
    [SerializeField] private int levelsPerPage;
    public static UnlockManager Instance { get; private set; }

    private void Awake()
    {
        PlayerPrefs.SetInt("MaxLevel", maxLevels);
        PlayerPrefs.Save();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UnlockLevel(int level)
    {
        int unlockedLevel = PlayerPrefs.GetInt(PlayerPrefKey.unlockedLevel, 1);
        if (level < unlockedLevel || unlockedLevel > maxLevels) return;

        PlayerPrefs.SetInt(PlayerPrefKey.unlockedLevel, unlockedLevel + 1);
        PlayerPrefs.Save();

        if (unlockedLevel + 1 > levelsPerPage && (unlockedLevel + 1) % levelsPerPage == 1)
        {
            UnlockPage();
        }
    }

    private void UnlockPage()
    {
        int unlockedPage = PlayerPrefs.GetInt(PlayerPrefKey.unlockedPage, 1);
        if (unlockedPage < maxPages)
        {
            PlayerPrefs.SetInt(PlayerPrefKey.unlockedPage, unlockedPage + 1);
            PlayerPrefs.Save();
        }
    }
}
