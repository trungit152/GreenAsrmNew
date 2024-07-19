using System.Collections;
using UnityEngine;

public class Level5 : MonoBehaviour
{
    private const int LevelNumber = 5;

    [Header("Animation")]
    [SerializeField] private float animationSpeed = 1.0f;
    [SerializeField] private Animator levelCompletedAnimator;
    [SerializeField] private Animator unlockAnimator;
    [SerializeField] private AnimationClip fingerprintAnimation;

    private Animator animator;
    private bool isMouseHeld = false;
    private float animationProgress = 0f;
    private const float MinAnimationProgress = 0f;
    private const float MaxAnimationProgress = 0.99f;
    private Coroutine coroutine;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0;
    }

    private void Update()
    {
        if (IsLevelCompleted())
        {   
            coroutine ??= StartCoroutine(CompleteLevel());
            return;
        }

        UpdateAnimationProgress();
        animator.Play(fingerprintAnimation.name, 0, animationProgress);
    }

    private void OnMouseDown()
    {
        isMouseHeld = true;
    }

    private void OnMouseUp()
    {
        isMouseHeld = false;
    }

    private void UpdateAnimationProgress()
    {
        if (isMouseHeld)
        {
            animationProgress += Time.deltaTime * animationSpeed;
        }
        else
        {
            animationProgress -= Time.deltaTime * animationSpeed;
        }

        animationProgress = Mathf.Clamp(animationProgress, MinAnimationProgress, MaxAnimationProgress);
    }

    IEnumerator CompleteLevel()
    {
        unlockAnimator.SetTrigger("Unlock");
        yield return new WaitForSeconds(40 * Time.deltaTime);
        levelCompletedAnimator.SetTrigger("IsCompleted");
        UnlockManager.Instance.UnlockLevel(LevelNumber);
    }

    private bool IsLevelCompleted()
    {
        return Mathf.Approximately(animationProgress, MaxAnimationProgress);
    }
}
