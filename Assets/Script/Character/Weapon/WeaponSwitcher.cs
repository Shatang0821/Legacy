using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _weaponObjects;
    private WeaponBase[] _weapons;//コンポーネント
    private int currentWeaponIndex = 0;
    private void Awake()
    {
        InitializeWeapons();
    }

    private void InitializeWeapons()
    {
        _weapons = new WeaponBase[_weaponObjects.Length]; // 初始化 _weapons 数组

        // 在?始?，除了当前?中的武器外，禁用所有武器
        for (int i = 0; i < _weaponObjects.Length; i++)
        {
            _weaponObjects[i].SetActive(i == currentWeaponIndex);
            _weapons[i] = _weaponObjects[i].GetComponent<WeaponBase>();
        }
    }
    public void SwitchWeapon(object param)
    {
        if (param is bool next)
        {
            _weaponObjects[currentWeaponIndex].SetActive(false);

            if (next)
            {
                currentWeaponIndex = (currentWeaponIndex + 1) % _weaponObjects.Length;
            }
            else
            {
                currentWeaponIndex--;
                if (currentWeaponIndex < 0) currentWeaponIndex = _weaponObjects.Length - 1;
            }
            _weaponObjects[currentWeaponIndex].SetActive(true);
        }
            
    }

    public GameObject GetCurrentWeapon()
    {
        return _weaponObjects[currentWeaponIndex];
    }

    public void AttackWithCurrentWeapon(float angle)
    {
        if (_weapons[currentWeaponIndex] != null)
        {
            _weapons[currentWeaponIndex].Attack(angle);
        }
    }

    public float GetAttackInterval()
    {
        if (_weapons[currentWeaponIndex] != null)
        {
            return _weapons[currentWeaponIndex].GetWeaponAttackInterval();
        }
        else
        {
            return 1;
        }
    }
}

