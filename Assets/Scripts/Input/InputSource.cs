using UnityEngine;

public abstract class InputSource : MonoBehaviour
{
    public virtual Vector2 Movement { get; } = Vector2.zero;

    public bool HasMoveInput => Movement == Vector2.zero == false;
}
