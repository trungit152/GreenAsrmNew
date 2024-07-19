using UnityEngine;
using UnityEngine.EventSystems;

public class GuideMenu : MonoBehaviour
{
    private Animator guideAnim;

    private void Awake()
    {
        guideAnim = GetComponent<Animator>();
    }

    public void Guide()
    {
        guideAnim.SetTrigger("OnGuideButtonClick");
    }
    
}
