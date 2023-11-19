using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _staffMuzzle;
    public void Attack(float angle)
    {
        FireBullet(angle);
    }

    private void FireBullet(float angle)
    {
        GameObject bulletInstance = PoolManager.Release(_bulletPrefab, _staffMuzzle.transform.position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D bulletRigidbody = bulletInstance.GetComponent<Rigidbody2D>();

        Vector2 fireDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        bulletRigidbody.velocity = fireDirection.normalized * _bulletSpeed;

        bulletInstance.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
