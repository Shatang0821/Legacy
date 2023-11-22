using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
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

        for (int i = 0; i < _weaponObjects.Length; i++)
        {
            _weaponObjects[i].SetActive(i == currentWeaponIndex);
            _weapons[i] = _weaponObjects[i].GetComponent<WeaponBase>();
        }
    }

    public void SelectWeapon()
    {

        _weaponObjects[currentWeaponIndex].SetActive(false);
        currentWeaponIndex++;
        if(currentWeaponIndex == _weaponObjects.Length) 
        {
            currentWeaponIndex = 0;
        }
        _weaponObjects[currentWeaponIndex].SetActive(true);
        EventCenter.TriggerEvent("ResetCurrentWeapon");
    }

    public WeaponBase GetCurrentWeapon()
    {
        Debug.Log("YES");
        return _weapons[currentWeaponIndex];
    }


}
