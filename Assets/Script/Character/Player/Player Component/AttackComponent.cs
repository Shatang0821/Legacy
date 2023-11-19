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
                // �@�ʉE?�W�L?���C?�g�p��?�蕿�T��
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
            //�U���Ԋu���グ�Ă��������x�U��
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
        // ?�ቻ�q?
        GameObject bulletInstance = PoolManager.Release(_swordEnergyPrefab, _attackPoint.transform.position, Quaternion.Euler(0, 0, angle));
        // �l?���p�F�|?�I�e?
        if (transform.localScale.x < 0)
        {
            angle += 180f;
        }
        // ?��Rigidbody?��
        Rigidbody2D bulletRigidbody = bulletInstance.GetComponent<Rigidbody2D>();

        // ?�Z?�˕���
        Vector2 fireDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

        // ?�葬�x
        bulletRigidbody.velocity = fireDirection.normalized * bulletSpeed; // bulletSpeed ��?�z�v�I�q?���x
                                                                           
        bulletInstance.transform.rotation = Quaternion.Euler(0, 0, angle);// ?���q??���I�����ȕC�z?�˕���
    }
}
