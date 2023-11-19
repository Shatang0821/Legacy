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

    private WaitForSeconds _waitForAttackInterval;

    [SerializeField]
    private MonoBehaviour _currentWeaponMonoBehaviour; // ��? MonoBehaviour ���p
    [SerializeField]
    private GameObject _attackPoint;
    private IWeapon _currentWeapon; // ??�g�p�I IWeapon �ڌ�

    private void Awake()
    {
        _currentWeapon = _currentWeaponMonoBehaviour as IWeapon;
        if (_currentWeapon == null)
        {
            Debug.LogError("The assigned weapon does not implement the IWeapon interface.");
        }
        _waitForAttackInterval = new WaitForSeconds(_attackInterval);
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

            yield return _waitForAttackInterval;
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
        _currentWeapon.Attack(rotZ);
    }
}
