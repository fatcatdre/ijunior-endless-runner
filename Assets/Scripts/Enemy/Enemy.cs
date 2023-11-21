using UnityEngine;

public class Enemy : Poolable
{
    [SerializeField] private int _damage;
    [SerializeField] private int _scoreValue;

    public int ScoreValue => _scoreValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(_damage);

        if (collision.TryGetComponent(out Enemy _))
            return;

        ReturnToPool();
    }
}
