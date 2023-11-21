using UnityEngine;

public class Vector2Input : InputSource
{
    [SerializeField] private Vector2 _movement;

    public override Vector2 Movement => _movement;
}
