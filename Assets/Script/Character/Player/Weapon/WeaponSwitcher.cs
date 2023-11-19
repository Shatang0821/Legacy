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
    private GameObject[] weapons; // ΌΪgpGameObject?
    private int currentWeaponIndex = 0;
    private void Awake()
    {
        InitializeWeapons();
    }

    private void InitializeWeapons()
    {
        // έ?n?CΉO?IνOCΦpLν
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeaponIndex);
        }
    }
    public void SwitchWeapon(object param)
    {
        if (param is bool next)
        {
            // ΦpOν
            weapons[currentWeaponIndex].SetActive(false);

            // Ψ?ν
            if (next)
            {
                currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
            }
            else
            {
                currentWeaponIndex--;
                if (currentWeaponIndex < 0) currentWeaponIndex = weapons.Length - 1;
            }

            // V??Iν
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

