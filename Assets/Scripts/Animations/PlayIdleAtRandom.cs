using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayIdleAtRandom : MonoBehaviour
{
    // References
    private Animator animator;

    private float timePassed = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed > 15f)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                animator.SetTrigger("IdleTrigger");
            timePassed = 0f;
        } 
    }
}
