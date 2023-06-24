using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Weapon weapon;
    public List<AttackSO> attckCombo;
    private float lastClickedTime;
    private float lastComboEnd;
    private int comboCounter;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        ExitAttack();
    }

    void Attack()
    {
        if (Time.time - lastComboEnd > 0.5f && comboCounter < attckCombo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.3f)
            {
                animator.runtimeAnimatorController = attckCombo[comboCounter].animatorOV;
                animator.Play("Attack", 0, 0);
                weapon.damage = attckCombo[comboCounter].damage;
                comboCounter++;
                lastClickedTime = Time.time;

                if (comboCounter >= attckCombo.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

    void ExitAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f 
            && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 1);
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }
}
