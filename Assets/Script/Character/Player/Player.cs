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
    private AttackComponent _attackComponent;
    private SwitchWeapon _switchWeapon;
    #endregion
    private void Awake()
    {
        _moveComponent = GetComponent<MoveComponent>();
        _attackComponent = GetComponent<AttackComponent>();
        _switchWeapon = GetComponentInChildren<SwitchWeapon>();
    }

    private void OnEnable()
    {
        EventCenter.Subscribe("PlayerMove", _moveComponent.OnMoveEvent);
        EventCenter.Subscribe("PlayerStopMove", _moveComponent.OnStopMoveEvent);

        EventCenter.Subscribe("PlayerAttack", _attackComponent.OnAttackEvent);
        EventCenter.Subscribe("PlayerStopAttack", _attackComponent.OnStopAttackEvent);

        EventCenter.Subscribe("PlayerSwitchWeapon", _switchWeapon.SelectWeapon);
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe("PlayerMove", _moveComponent.OnMoveEvent);
        EventCenter.Unsubscribe("PlayerStopMove", _moveComponent.OnStopMoveEvent);

        EventCenter.Unsubscribe("PlayerAttack", _attackComponent.OnAttackEvent);
        EventCenter.Unsubscribe("PlayerStopAttack", _attackComponent.OnStopAttackEvent);

        EventCenter.Unsubscribe("PlayerSwitchWeapon", _switchWeapon.SelectWeapon);
    }

    private void Start()
    {
        input.EnableGameplayInput();
    }
}
