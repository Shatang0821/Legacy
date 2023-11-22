using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAttack : MonoBehaviour
{
    float _t;
    private AttackComponent _attackComponent;
    // Start is called before the first frame update
    void Start()
    {
        _attackComponent = GetComponent<AttackComponent>();
    }

    // Update is called once per frame
    void Update()
    {

        if (_t <= 1)
        {
            _t += Time.deltaTime;
            _attackComponent.OnStopAttackEvent();
        }
        else
        {
            _attackComponent.OnAttackEvent();
            _t = 0;
        }

    }
}
