using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class AttackComponent : MonoBehaviour
{
    private bool _usingGamepad = false;

    [SerializeField]
    private float _attackInterval;
    private float _t;
    //private WaitForSeconds _waitForAttackInterval;

    [SerializeField]
    private GameObject _attackPoint;

    private WeaponSwitcher _weaponSwitcher;

    private void Awake()
    {
        //_waitForAttackInterval = new WaitForSeconds(_attackInterval);
        _weaponSwitcher = GetComponent<WeaponSwitcher>();
    }

    public void OnAttackEvent()
    {
        Attack();
        //Vector2 attackDirection = GetAttackDirection();
        //_currentWeapon.Attack(attackDirection);
    }

    public void OnStopAttackEvent()
    {
        StopAttack();
    }

    private void Attack()
    {
        if (Gamepad.current != null && Gamepad.current.leftTrigger.isPressed)
        {
            _usingGamepad = true;
        }
        else
        {
            _usingGamepad = false;
        }
        _attackInterval = _weaponSwitcher.GetAttackInterval();
        StartCoroutine(nameof(AttackCoroutine));
        
    }

    private void StopAttack()
    {
        StopCoroutine(nameof(AttackCoroutine));
    }


    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            if (_usingGamepad)
            {
                Vector2 rightStickInput = Gamepad.current.rightStick.ReadValue();
                if (rightStickInput.sqrMagnitude > 0.01f)
                {
                    RotateWeapon(rightStickInput.normalized);
                }
            }
            else if (Mouse.current != null)
            {
                Vector2 mouseInput = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - _attackPoint.transform.position;
                RotateWeapon(mouseInput.normalized);
            }

            yield return null;
        }
    }

    private void RotateWeapon(Vector2 input)
    {
        if (transform.localScale.x <= 0)
        {
            input *= new Vector2(-1, -1);
        }
        float rotZ = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        _attackPoint.transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (transform.localScale.x < 0)
        {
            rotZ += 180f;
        }


        if (_t <= _attackInterval)
        {
            _t += Time.deltaTime;
        }
        else
        {
            _weaponSwitcher.AttackWithCurrentWeapon(rotZ);
            _t = 0f;
        }
    }
}
