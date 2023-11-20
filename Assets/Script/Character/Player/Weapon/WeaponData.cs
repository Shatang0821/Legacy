using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("==== Object ====")]
    public GameObject _weapon;


    [Header("==== Attribute ====")]
    public float _effectSpeed;
    public float _damage;
    public float _attackInterval;
}
