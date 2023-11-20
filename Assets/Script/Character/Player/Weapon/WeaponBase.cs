using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour,IWeapon
{
    [Header("==== Object ====")]
    [SerializeField] private GameObject _weaponEffectPrefab;
    [SerializeField] private GameObject _weaponMuzzle;

    [Header("==== Attribute ====")]
    [SerializeField] private float _effectSpeed;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackInterval;

    private Animator _animator;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void Attack(float angle)
    {
        FireEffect(angle);
    }

    protected virtual void FireEffect(float angle)
    {
        _animator.SetTrigger("Attack");
        GameObject bulletInstance = PoolManager.Release(_weaponEffectPrefab, _weaponMuzzle.transform.position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D bulletRigidbody = bulletInstance.GetComponent<Rigidbody2D>();

        Vector2 fireDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        bulletRigidbody.velocity = fireDirection.normalized * _effectSpeed;

        bulletInstance.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public float GetWeaponAttackInterval()
    {
        return _attackInterval;
    }

}
