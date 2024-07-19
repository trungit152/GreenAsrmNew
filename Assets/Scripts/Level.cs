using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Level Configuration")]
    [SerializeField] private int level;
    [SerializeField] private Sprite completedSprite;

    private void Awake()
    {
        AnimateUnlockedLevel();
    }

    private void AnimateUnlockedLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt(PlayerPrefKey.unlockedLevel, 1);
        if (level == unlockedLevel)
        {
            GetComponent<Animator>().SetTrigger("ButtonAppear");
        }
    }

    public Sprite GetCompletedSprite()
    {
        return completedSprite;
    }
}
