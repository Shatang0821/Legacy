using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponBase _currentWeapon;
    private RotateWeapon _rotateWeapon;

    private void Awake()
    {
        _rotateWeapon = GetComponentInChildren<RotateWeapon>();
        _currentWeapon = GetCurrentWeapon();
        if(_rotateWeapon == null)
        {
            Debug.Log("YES");
        }
    }

    private void OnEnable()
    {
        if(_rotateWeapon.ownerType == GameEnums.OwnerType.Player)
        {
            EventCenter.Subscribe("ResetCurrentWeapon", ResetCurrentWeapon);
        }
        
    }

    private void OnDisable()
    {
        if (_rotateWeapon.ownerType == GameEnums.OwnerType.Player)
        {
            EventCenter.Unsubscribe("ResetCurrentWeapon", ResetCurrentWeapon);
        }
    }

    private WeaponBase GetCurrentWeapon()
    {
        if (_rotateWeapon.ownerType==GameEnums.OwnerType.Player)
        {
            return GetComponentInChildren<SwitchWeapon>().GetCurrentWeapon();
        }
        else
        {
            return _currentWeapon;
        }
    }

    void ResetCurrentWeapon()
    {
        _currentWeapon = GetComponentInChildren<SwitchWeapon>().GetCurrentWeapon();
    }

    public void AttackWithCurrentWeapon()
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.Attack(_rotateWeapon.GetAngle());
        }
    }



    public float GetAttackInterval()
    {
        if (_currentWeapon != null)
        {
            return _currentWeapon.GetWeaponAttackInterval();
        }
        else
        {
            return 1;
        }
    }
}

