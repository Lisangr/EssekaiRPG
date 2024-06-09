using System;
using UnityEngine;

public class TraderGreatings : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        ObjectChecker.OnPressButtonF += AnimationGreatings;
        ForCMCameras.GoAwayFromNPC += AnimationBye;
    }

    private void AnimationBye()
    {
        animator.SetTrigger("Happy");
    }

    private void AnimationGreatings()
    {
        animator.SetTrigger("Greatings");
    }

    private void OnDisable()
    {
        ObjectChecker.OnPressButtonF -= AnimationGreatings;
        ForCMCameras.GoAwayFromNPC -= AnimationBye;
    }
}
