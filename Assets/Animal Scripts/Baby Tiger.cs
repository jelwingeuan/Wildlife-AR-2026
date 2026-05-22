using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Meshy_AI_Tiger_Cub_Portrait_0513081932_texture : MonoBehaviour
{
    private static readonly int IsAwake = Animator.StringToHash("IsAwake");

    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    public void PlaceTiger(ARTrackable trackableParent)
    {
        transform.SetParent(trackableParent?.transform);
        animator.SetBool(IsAwake, true);
    }
}