using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// エネミー基底クラス
/// </summary>
public class Enemy : MonoBehaviour,IDamageable
{
    private GameObject _target;

    [SerializeField] private float _speed;

    private int _maxHp;
    private int _hp;

    protected virtual void OnEnable()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        _hp = _maxHp;
        if (_target == null )
        {
            Debug.Log("CAN NOT FIND PLAYER");
        }
        else
        {
            StartCoroutine(nameof(TrackPlayerCoroutine));
        }
    }

    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator TrackPlayerCoroutine()
    {
        while(_target.activeSelf)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
            yield return null;
        }
       
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            _hp = 0;
            //TODO DIE
            this.gameObject.SetActive(false);
        }
    }
}
