using UnityEngine;

public class HealthKit : Poolable, ICollectable
{
    [SerializeField] private int _healAmount;

    public int HealAmount => _healAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICollector collector))
            collector.Collect(this);

        if (collision.TryGetComponent(out Enemy _))
            return;

        ReturnToPool();
    }

    public void OnCollected(ICollector collector)
    {
        
    }
}
