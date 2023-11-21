using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] protected InputSource _inputSource;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _acceleration;
    [SerializeField] protected float _deceleration;

    protected Rigidbody2D _rigidbody;

    protected Vector2 _targetVelocity;
    protected Vector2 _moveInput;

    protected bool HasMoveInput => _inputSource.HasMoveInput;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        ReadInput();
        CalculateMovement();
        Move();
    }

    protected virtual void ReadInput()
    {
        _moveInput = _inputSource.Movement;
    }

    protected virtual void CalculateMovement()
    {
        if (HasMoveInput)
            Accelerate();
        else
            Decelerate();
    }

    protected virtual void Accelerate()
    {
        _targetVelocity = Vector2.MoveTowards(_targetVelocity, _moveInput * _moveSpeed, _acceleration * Time.deltaTime);
    }

    protected virtual void Decelerate()
    {
        _targetVelocity = Vector2.MoveTowards(_targetVelocity, Vector2.zero, _deceleration * Time.deltaTime);
    }

    protected virtual void Move()
    {
        _rigidbody.velocity = _targetVelocity;
    }
}
