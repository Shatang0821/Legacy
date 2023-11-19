using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour,IWeapon
{

    [SerializeField] private GameObject _swordEnergyPrefab;
    [SerializeField] private float _slashSpeed;
    [SerializeField] private GameObject _swordMuzzle;
    public void Attack(float angle)
    {
        FireBullet(angle);
    }

    private void FireBullet(float angle)
    {
        GameObject bulletInstance = PoolManager.Release(_swordEnergyPrefab, _swordMuzzle.transform.position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D bulletRigidbody = bulletInstance.GetComponent<Rigidbody2D>();

        Vector2 fireDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        bulletRigidbody.velocity = fireDirection.normalized * _slashSpeed;

        bulletInstance.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
