using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinderActivatorForCutScene1 : MonoBehaviour
{
    private Animator animator;
    public float summonTime;
    public float dieTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        Idle();
        Invoke("Summon", summonTime);
    }
    void Idle()
    {
        animator.SetTrigger("Idle");
    }
    void Summon()
    {
        animator.SetTrigger("Summon");
    }

    void Die()
    {
        animator.SetTrigger("Die");
    }
}
