using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    Sword,
    Staff
}

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject[] weapons; // 直接使用GameObject数?
    private int currentWeaponIndex = 0;
    private void Awake()
    {
        InitializeWeapons();
    }

    private void InitializeWeapons()
    {
        // 在?始?，除了当前?中的武器外，禁用所有武器
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeaponIndex);
        }
    }
    public void SwitchWeapon(object param)
    {
        if (param is bool next)
        {
            // 禁用当前武器
            weapons[currentWeaponIndex].SetActive(false);

            // 切?武器
            if (next)
            {
                currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
            }
            else
            {
                currentWeaponIndex--;
                if (currentWeaponIndex < 0) currentWeaponIndex = weapons.Length - 1;
            }

            // 激活新??的武器
            weapons[currentWeaponIndex].SetActive(true);
        }
            
    }

    public GameObject GetCurrentWeapon()
    {
        return weapons[currentWeaponIndex];
    }

    public void AttackWithCurrentWeapon(float angle)
    {
        IWeapon currentWeapon = weapons[currentWeaponIndex].GetComponent<IWeapon>();
        if (currentWeapon != null)
        {
            currentWeapon.Attack(angle);
        }
    }
}

