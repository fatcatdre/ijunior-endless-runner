using UnityEngine;

public class PlayerMover : CharacterMover
{
    [SerializeField] private float _minPositionX;
    [SerializeField] private float _maxPositionX;

    protected override void CalculateMovement()
    {
        if (HasMoveInput)
        {
            Accelerate();

            if (Mathf.Sign(_moveInput.x) != Mathf.Sign(_targetVelocity.x))
                Decelerate();
        }
        else
        {
            Decelerate();
        }
    }

    protected override void Move()
    {
        base.Move();

        var clampedPosition = _rigidbody.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, _minPositionX, _maxPositionX);

        _rigidbody.position = clampedPosition;
    }
}
