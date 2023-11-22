using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player Input")]
public class PlayerInput : ScriptableObject, InputActions.IGamePlayActions
{
    // 新しいInput Systemのアクションへの参照。
    private InputActions inputActions;

    // 有効にされた時に呼ばれるメソッド。
    void OnEnable()
    {
        inputActions = new InputActions();

        inputActions.GamePlay.SetCallbacks(this);

    }

    // 無効にされた時に呼ばれるメソッド。
    void OnDisable()
    {
        DisableAllInputs();
    }

    #region INPUT MAP
    /// <summary>
    /// 有効actionmapを変わり
    /// </summary>
    /// <param name="actionMap">変えたいactionMap</param>
    /// <param name="isUIInput">UIの選択か</param>
    void SwitchActionMap(InputActionMap actionMap, bool isUIInput)
    {
        inputActions.Disable();
        actionMap.Enable();

        //if (isUIInput)
        //{
        //    Cursor.visible = true;                     // マウスカーソルを可視にします。
        //    Cursor.lockState = CursorLockMode.None;    // マウスカーソルをロックしない。
        //}
        //else
        //{
        //    Cursor.visible = false;                     // マウスカーソルを不可視にします。
        //    Cursor.lockState = CursorLockMode.Locked;   // マウスカーソルをロックする。
        //}
    }

    /// <summary>
    /// 入力を無効化する
    /// </summary>
    public void DisableAllInputs() => inputActions.Disable();

    /// <summary>
    /// ゲーム内でキャラクターを操作する時に入力を有効化するメソッド。
    /// </summary>
    public void EnableGameplayInput() => SwitchActionMap(inputActions.GamePlay, false);
    #endregion


    #region EVENT
    // 移動アクションがトリガーされた時に呼ばれるメソッド。
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            EventCenter.TriggerEvent("PlayerMove", moveInput);
        }

        if (context.canceled)
        {
            EventCenter.TriggerEvent("PlayerStopMove");
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            EventCenter.TriggerEvent("PlayerAttack");
        }
        if(context.canceled)
        {
            EventCenter.TriggerEvent("PlayerStopAttack");
        }
    }

    public void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            EventCenter.TriggerEvent("PlayerSwitchWeapon");
        }
    }


    #endregion
}
