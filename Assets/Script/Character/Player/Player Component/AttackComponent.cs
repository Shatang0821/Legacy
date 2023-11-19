using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _weaponPoint;
    [SerializeField]
    private GameObject _weapon;

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
        StartCoroutine(nameof(AttackCoroutine));
    }

    private void StopAttack()
    {
        StopCoroutine(nameof(AttackCoroutine));
    }

    //IEnumerator AttackCoroutine()
    //{
    //    while(true)
    //    {
    //        Vector2 diiference = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - _weaponPoint.transform.position;
    //        float rotZ = Mathf.Atan2(diiference.y, diiference.x) * Mathf.Rad2Deg;
    //        _weaponPoint.transform.rotation = Quaternion.Euler(0, 0, rotZ);

    //        yield return null;
    //    }

    //}

    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            Vector2 rightStickInput = InputSystem.GetDevice<Gamepad>().rightStick.ReadValue();
            // 注意：右?杆的?范?通常在 -1 到 1 之?，?可能需要?整角度?算的代?来???个范?。

            float rotZ = Mathf.Atan2(rightStickInput.y, rightStickInput.x) * Mathf.Rad2Deg;
            _weaponPoint.transform.rotation = Quaternion.Euler(0, 0, rotZ);

            yield return null;
        }
    }
}
