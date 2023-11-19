using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class AttackComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _attackPoint;
    [SerializeField]
    private GameObject _weapon;
    [SerializeField]
    private GameObject _swordEnergyPrefab;
    [SerializeField]
    private GameObject _swordMuzzle;

    private bool _usingGamepad = false;

    [SerializeField]
    private float _attackInterval;
    [SerializeField]
    private float bulletSpeed;

    private WaitForSeconds _waitForAttackInterval;

    private void Awake()
    {
        _waitForAttackInterval = new WaitForSeconds(_attackInterval);
    }
    private void OnEnable()
    {
        _weapon.SetActive(false);
    }

    public void OnAttackEvent()
    {
        Attack();
    }

    public void OnStopAttackEvent()
    {
        StopAttack();
    }

    private void Attack()
    {
        if (Gamepad.current != null && Gamepad.current.leftTrigger.isPressed)
        {
            _usingGamepad = true;
        }
        else
        {
            _usingGamepad = false;
        }
        StartCoroutine(nameof(AttackCoroutine));
        

    }

    private void StopAttack()
    {
        StopCoroutine(nameof(AttackCoroutine));
        _weapon.SetActive(false);
    }


    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            _weapon.SetActive(true);
            if (_usingGamepad)
            {
                Vector2 rightStickInput = Gamepad.current.rightStick.ReadValue();
                // 如果右?杆有?入，?使用游?手柄控制
                if (rightStickInput.sqrMagnitude > 0.01f)
                {
                    RotateWeapon(rightStickInput.normalized);
                }
            }
            else if (Mouse.current != null)
            {
                Vector2 mouseInput = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - _attackPoint.transform.position;
                RotateWeapon(mouseInput.normalized);
            }
            yield return _waitForAttackInterval;
            _weapon.SetActive(false);
            //攻撃間隔を上げてからもう一度攻撃
            yield return null;
        }
    }

    private void RotateWeapon(Vector2 input)
    {
        if (transform.localScale.x <= 0)
        {
            input *= new Vector2(-1, -1);
        }
        float rotZ = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        _attackPoint.transform.rotation = Quaternion.Euler(0, 0, rotZ);

        FireBullet(rotZ);
    }

    private void FireBullet(float angle)
    {
        // ?例化子?
        GameObject bulletInstance = PoolManager.Release(_swordEnergyPrefab, _attackPoint.transform.position, Quaternion.Euler(0, 0, angle));
        // 考?到角色翻?的影?
        if (transform.localScale.x < 0)
        {
            angle += 180f;
        }
        // ?取Rigidbody?件
        Rigidbody2D bulletRigidbody = bulletInstance.GetComponent<Rigidbody2D>();

        // ?算?射方向
        Vector2 fireDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

        // ?定速度
        bulletRigidbody.velocity = fireDirection.normalized * bulletSpeed; // bulletSpeed 是?想要的子?速度
                                                                           
        bulletInstance.transform.rotation = Quaternion.Euler(0, 0, angle);// ?整子??像的方向以匹配?射方向
    }
}
