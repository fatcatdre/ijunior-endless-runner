using UnityEngine;

public class PlayerInput : InputSource
{
    private Vector2 _movement;

    public override Vector2 Movement => _movement;

    private void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = 0f;
    }
}
