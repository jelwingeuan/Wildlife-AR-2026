using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    [Header("Tiger Animator")]
    [SerializeField] private Animator tigerAnimator;

    [Header("Animation State Names")]
    [SerializeField] private string idleAnimationName = "rig|idle";
    [SerializeField] private string jumpAnimationName = "rig|jump";

    private void Start()
    {
        PlayIdle();
    }

    public void PlayIdle()
    {
        if (tigerAnimator == null)
        {
            Debug.LogWarning("Tiger Animator is missing!");
            return;
        }

        tigerAnimator.Play(idleAnimationName);
    }

    public void PlayJump()
    {
        if (tigerAnimator == null)
        {
            Debug.LogWarning("Tiger Animator is missing!");
            return;
        }

        tigerAnimator.Play(jumpAnimationName);
        Invoke(nameof(PlayIdle), 2f);
    }
}
