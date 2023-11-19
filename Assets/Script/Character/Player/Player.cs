using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    [Header("---- INPUT ----")]
    [SerializeField] PlayerInput input;

    #region COMPONENT

    private MoveComponent _moveComponent;
    private AttackComponent _AttackComponent;
    #endregion
    private void Awake()
    {
        _moveComponent = GetComponent<MoveComponent>();
        _AttackComponent = GetComponent<AttackComponent>();
    }

    private void OnEnable()
    {
        EventCenter.Subscribe("PlayerMove", _moveComponent.OnMoveEvent);
        EventCenter.Subscribe("PlayerStopMove", _moveComponent.OnStopMoveEvent);

        EventCenter.Subscribe("PlayerAttack", _AttackComponent.OnAttackEvent);
        EventCenter.Subscribe("PlayerStopAttack", _AttackComponent.OnStopAttackEvent);
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe("PlayerMove", _moveComponent.OnMoveEvent);
        EventCenter.Unsubscribe("PlayerStopMove", _moveComponent.OnStopMoveEvent);

        EventCenter.Unsubscribe("PlayerAttack", _AttackComponent.OnAttackEvent);
        EventCenter.Unsubscribe("PlayerStopAttack", _AttackComponent.OnStopAttackEvent);
    }

    private void Start()
    {
        input.EnableGameplayInput();
    }
}
