using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    IState currentState;//ステート読み込む

    protected Dictionary<System.Type, IState> stateTable;//ステートのタイプによってステートを取得

    void Update()
    {
        currentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        currentState.PhysicUpdate();    
    }

    protected void SwitchOn(IState newState)
    {
        currentState = newState;
        currentState.Enter();
    }

    public void SwitchState(IState newState)
    {
        currentState.Exit();
        SwitchOn(newState);
    }

    public void SwitchState(System.Type newStateType)
    {
        SwitchState(stateTable[newStateType]);
    }
}
