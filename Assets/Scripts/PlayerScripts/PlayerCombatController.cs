using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

public class PlayerCombatController : MonoBehaviour
{
    private Animator animator;
    private Weapon weapon;
    public List<AttackSO> attackCombo;
    private float lastClickedTime;
    private float lastComboEnd;
    private int comboCounter;
    public float delay = 1f;
    public VisualEffect lightning;
    private PlayerController playerController;

    public bool IsAttacking { get; private set; }
    public event Action OnAttackStart;
    public event Action OnAttackEnd;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        lightning.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(AbilitySequence());
        }

        ExitAttack();
    }

    void Attack()
    {
        if (Time.time - lastComboEnd > 0.5f && comboCounter < attackCombo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.3f)
            {
                IsAttacking = true;
                OnAttackStart?.Invoke();

                animator.runtimeAnimatorController = attackCombo[comboCounter].animatorOV;
                animator.Play("Attack", 0, 0);
                comboCounter++;
                lastClickedTime = Time.time;

                if (comboCounter >= attackCombo.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

    private IEnumerator AbilitySequence()
    {
        yield return new WaitForSeconds(0.25f);
        lightning.gameObject.SetActive(true);
        lightning.Play();
    }

    void ExitAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 1);
        }
        //playerController.HandleAttackEnd();
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
        IsAttacking = false;
        OnAttackEnd?.Invoke();
    }
}

