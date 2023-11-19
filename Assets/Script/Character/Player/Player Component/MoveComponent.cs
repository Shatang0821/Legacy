using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveComponent : MonoBehaviour
{
    [SerializeField]
    private float _accelerationTime;
    [SerializeField]
    private float _decelerationTime;
    [SerializeField]
    private float _moveSpeed;
    private float _t;

    private Vector2 _previousVelocity;
    private Vector2 _moveDirection;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    /*------------------------------------------------------------*/

    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();   //used for MoveCoroutine
    private Coroutine _moveCoroutine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public void OnMoveEvent(object param)
    {
        if (param is Vector2 direction)
        {
            Move(direction);
        }
    }

    public void OnStopMoveEvent()
    {
        StopMove();
    }

    #region MOVE
    private void Move(Vector2 direction)
    {
        _animator.SetBool("IsMoving", direction != Vector2.zero);
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }
        _moveDirection = direction.normalized;

        // scale.x�t�]����
        if (_moveDirection.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(_moveDirection.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        _moveCoroutine = StartCoroutine(MoveCoroutine(_accelerationTime, _moveDirection * _moveSpeed));
    }   

    private void StopMove()
    {
        _animator.SetBool("IsMoving", false);
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }
        _moveDirection = Vector2.zero;
        _moveCoroutine = StartCoroutine(MoveCoroutine(_decelerationTime, Vector2.zero));
    }

    /// <summary>
    ///  ���x�ύX������
    /// </summary>
    /// <param name="time">�ύX����</param>
    /// <param name="moveVelocity">�ړ����x</param>
    /// <param name="moveRotation">��]���</param>
    /// <returns></returns>
    IEnumerator MoveCoroutine(float time, Vector2 moveVelocity)
    {
        _t = 0f;
        _previousVelocity = _rigidbody.velocity;

        while (_t < 1f)
        {
            _t += Time.fixedDeltaTime / time;
            //t��0����1�̊Ԃ�velocity��␳������t��1�ɂȂ�ƍő呬�x�ɒB���܂�
            _rigidbody.velocity = Vector2.Lerp(_previousVelocity, moveVelocity, _t);
            //�������Z�����邩��FixedUpdate�g���Đ��x���グ��
            yield return _waitForFixedUpdate;
        }
    }
    #endregion
}
