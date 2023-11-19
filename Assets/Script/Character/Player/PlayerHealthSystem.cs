using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour, IDamageable
{
    private int _maxHp;
    private int _hp;

    private void Start()
    {
        _hp = _maxHp;
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if(_hp<=0)
        {
            _hp = 0;
            //TODO DIE
            this.gameObject.SetActive(false);
            GameManager.GameState = GameState.GameOver;
        }
    }
}
