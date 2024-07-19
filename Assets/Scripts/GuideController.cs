using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideController : MonoBehaviour
{
    [SerializeField] private Animator guideAnim;
    void Start()
    {
        Invoke("Guide", 5f);
    }
    private void Guide()
    {
        guideAnim.SetTrigger("AppearGuide");
    }
}
