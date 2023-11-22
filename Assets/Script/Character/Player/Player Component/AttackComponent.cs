using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class AttackComponent : MonoBehaviour
{
    [SerializeField]
    private float _attackInterval;
    private float _t;
    //private WaitForSeconds _waitForAttackInterval;

    private WeaponManager _weaponManager;

    private void Awake()
    {
        //_waitForAttackInterval = new WaitForSeconds(_attackInterval);
        _weaponManager = GetComponent<WeaponManager>();

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
        _attackInterval = _weaponManager.GetAttackInterval();
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
            _weaponManager.AttackWithCurrentWeapon();
            yield return new WaitForSeconds(_attackInterval);
        }
    }
}
