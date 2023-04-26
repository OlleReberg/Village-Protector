using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float cdTime = 2f;
    private float nextFireTime = 0f;
    [SerializeField] private static int noOfClicks = 0;
    private float lastClickedTime = 0;
    private float maxComboDelay = 1;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f 
            && animator.GetCurrentAnimatorStateInfo(0).IsName("hit 1"))
        {
            animator.SetBool("hit 1", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f 
            && animator.GetCurrentAnimatorStateInfo(0).IsName("hit 2"))
        {
            animator.SetBool("hit 2", false);
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }
    }

    void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            animator.SetBool("hit 1", true);
        }

        Mathf.Clamp(noOfClicks, 0, 3);

        if (noOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7
                            && animator.GetCurrentAnimatorStateInfo(0).IsName("hit 1"))
        {
            animator.SetBool("hit 1", false);
            animator.SetBool("hit 2", true);
        }
    }   
}
