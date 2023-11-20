using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponBase : MonoBehaviour,IWeapon
{
    [SerializeField] private WeaponData _weaponData;
    [Header("==== Object ====")]
    [SerializeField] private GameObject _weaponEffectPrefab;
    [SerializeField] private GameObject _weaponMuzzle;
    [SerializeField] GameObject _target;
    private float _t;
    //public GameObject _weaponEffectPrefab;
    //public GameObject _weaponMuzzle;
    private Animator _animator;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PerformAttack(Vector2 _target)
    {
        StartCoroutine(AttackCoroutine(_target));
    }

    public void StopAttack()
    {
        StopCoroutine(nameof(AttackCoroutine));
    }

    IEnumerator AttackCoroutine(Vector2 _target)
    {
        while (true)
        {
            //if (_usingGamepad)
            //{
            //    Vector2 rightStickInput = Gamepad.current.rightStick.ReadValue();
            //    if (rightStickInput.sqrMagnitude > 0.01f)
            //    {
            //        RotateWeapon(rightStickInput.normalized);
            //    }
            //}
            //else if (Mouse.current != null)
            //{
            //    Vector2 mouseInput = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - _attackPoint.transform.position;
            //    RotateWeapon(mouseInput.normalized);
            //}
            RotateWeapon(_target);
            
            yield return null;
        }
    }


    protected virtual void FireEffect(float angle)
    {
        //_animator.SetTrigger("Attack");
        GameObject bulletInstance = PoolManager.Release(_weaponEffectPrefab, _weaponMuzzle.transform.position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D bulletRigidbody = bulletInstance.GetComponent<Rigidbody2D>();

        Vector2 fireDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        bulletRigidbody.velocity = fireDirection.normalized * _weaponData._effectSpeed;

        bulletInstance.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void RotateWeapon(Vector2 input)
    {
        if (transform.localScale.x <= 0)
        {
            input *= new Vector2(-1, -1);
        }
        float rotZ = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        //if (transform.localScale.x < 0)
        //{
        //    rotZ += 180f;
        //}

        //if (_t<=_weaponData._attackInterval)
        //{
        //    _t += Time.deltaTime;
        //}
        //else
        //{
        //    FireEffect(rotZ);
        //    _t= 0;
        //}
    }

    private void Update()
    {
        RotateWeapon(_target.transform.position);
    }
}
