using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player Input")]
public class PlayerInput : ScriptableObject, InputActions.IGamePlayActions
{
    // �V����Input System�̃A�N�V�����ւ̎Q�ƁB
    private InputActions inputActions;

    // �L���ɂ��ꂽ���ɌĂ΂�郁�\�b�h�B
    void OnEnable()
    {
        inputActions = new InputActions();

        inputActions.GamePlay.SetCallbacks(this);

    }

    // �����ɂ��ꂽ���ɌĂ΂�郁�\�b�h�B
    void OnDisable()
    {
        DisableAllInputs();
    }

    #region INPUT MAP
    /// <summary>
    /// �L��actionmap��ς��
    /// </summary>
    /// <param name="actionMap">�ς�����actionMap</param>
    /// <param name="isUIInput">UI�̑I����</param>
    void SwitchActionMap(InputActionMap actionMap, bool isUIInput)
    {
        inputActions.Disable();
        actionMap.Enable();

        //if (isUIInput)
        //{
        //    Cursor.visible = true;                     // �}�E�X�J�[�\�������ɂ��܂��B
        //    Cursor.lockState = CursorLockMode.None;    // �}�E�X�J�[�\�������b�N���Ȃ��B
        //}
        //else
        //{
        //    Cursor.visible = false;                     // �}�E�X�J�[�\����s���ɂ��܂��B
        //    Cursor.lockState = CursorLockMode.Locked;   // �}�E�X�J�[�\�������b�N����B
        //}
    }

    /// <summary>
    /// ���͂𖳌�������
    /// </summary>
    public void DisableAllInputs() => inputActions.Disable();

    /// <summary>
    /// �Q�[�����ŃL�����N�^�[�𑀍삷�鎞�ɓ��͂�L�������郁�\�b�h�B
    /// </summary>
    public void EnableGameplayInput() => SwitchActionMap(inputActions.GamePlay, false);
    #endregion


    #region EVENT
    // �ړ��A�N�V�������g���K�[���ꂽ���ɌĂ΂�郁�\�b�h�B
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
