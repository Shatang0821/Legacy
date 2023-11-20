using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    float _t;
    private AttackComponent _attackComponent;
    private void OnEnable()
    {
        EventCenter.Subscribe("EnemyAttack", _attackComponent.OnAttackEvent);
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe("EnemyAttack", _attackComponent.OnAttackEvent);
    }
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
        }
        else
        {
            EventCenter.TriggerEvent("EnemyAttack");
        }

    }
}
