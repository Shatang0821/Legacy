using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateWeapon : MonoBehaviour
{
    [SerializeField]
    private Transform parentTransform; // eTransform

    private Transform _targetTransform;

    private float _rotateAngle;
    private float _fireAngle;
    private Vector2 _targetDirection;

    public GameEnums.OwnerType ownerType;

    private void OnEnable()
    {
        if(ownerType == GameEnums.OwnerType.WeaponEnemy)
        {
            _targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        if(ownerType == GameEnums.OwnerType.Player)
        {
            _targetDirection = GetInputDirection();
        }
        else
        {
            _targetDirection = GetPlayerDirection();
        }
        RotateTowards(_targetDirection.normalized);
    }

    private Vector2 GetInputDirection()
    {
        if (Gamepad.current != null && Gamepad.current.rightStick.ReadValue().sqrMagnitude > 0.01f)
        {
            return Gamepad.current.rightStick.ReadValue();
        }
        else if (Mouse.current != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            return mousePosition - (Vector2)transform.position;
        }

        return Vector2.zero; // àÒ?•ûŒü
    }

    private Vector2 GetPlayerDirection()
    {
        if(_targetTransform !=null)
        {
            return _targetTransform.position - transform.position;
        }
        return Vector2.zero;
    }

    private void RotateTowards(Vector2 direction)
    {
        if (parentTransform.localScale.x <= 0)
        {
            direction *= new Vector2(-1, -1);

        }

        _rotateAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, _rotateAngle);
    }

    public float GetAngle()
    {
        _fireAngle = _rotateAngle;
        if(parentTransform.localScale.x <=0)
        {
            _fireAngle += 180f;
        }
        return _fireAngle;
    }
}
