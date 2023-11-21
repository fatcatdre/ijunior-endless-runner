using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour, IDamageable, ICollector
{
    [SerializeField] private int _maxHealth;

    private PlayerAnimator _animator;
    private int _currentHealth;

    public event UnityAction<int> HealthChanged;
    public event UnityAction Died;

    private void Awake()
    {
        _animator = GetComponent<PlayerAnimator>();

        _currentHealth = _maxHealth;
    }

    private void OnEnable()
    {
        HealthChanged?.Invoke(_currentHealth);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Min(_currentHealth, _maxHealth);

        HealthChanged?.Invoke(_currentHealth);

        if (damage > 0)
            _animator.TakeDamage();

        if (_currentHealth <= 0)
            Die();
    }

    public void Heal(int health)
    {
        TakeDamage(-health);
    }

    public void Collect(ICollectable collectable)
    {
        collectable.OnCollected(this);

        if (collectable is HealthKit)
        {
            Heal((collectable as HealthKit).HealAmount);
        }
    }

    private void Die()
    {
        Died?.Invoke();
    }
}
