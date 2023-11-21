using UnityEngine;
using UnityEngine.Events;

public class ObjectDestroyer : MonoBehaviour
{
    public event UnityAction<int> ObjectDestroyed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            ObjectDestroyed?.Invoke(enemy.ScoreValue);
        }

        if (collision.TryGetComponent(out HealthKit healthKit))
        {
            ObjectDestroyed?.Invoke(-healthKit.HealAmount);
        }
    }
}
