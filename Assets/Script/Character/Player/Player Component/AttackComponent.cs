using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    private GameObject _currentWeaponObject;
    private IWeapon _currentWeapon;
    //private WaitForSeconds _waitForAttackInterval;

    private void Awake()
    {

    }

    public void OnAttackEvent()
    {
        Attack();
    }

    public void OnStopAttackEvent()
    {
        StopAttack();
    }

    public void SetWeapon(GameObject weaponPrefab)
    {
        if(_currentWeapon!=null)
        {
            Destroy(_currentWeaponObject);
        }
        _currentWeaponObject = Instantiate(weaponPrefab, transform);
        _currentWeapon = _currentWeaponObject.GetComponent<IWeapon>();
    }

    private void Attack()
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.PerformAttack(_target.transform.position);
        }
    }

    private void StopAttack()
    {
        if(_currentWeapon != null)
        {
            _currentWeapon.StopAttack();
        }
    }
}
