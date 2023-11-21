using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private HealthUnit _healthUnit;

    [Header("Animation Options")]
    [SerializeField] private Color _visibleColor;
    [SerializeField] private Color _fadeoutColor;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;

    private int _trackedHealth;

    private Stack<HealthUnit> _lives = new();

    private void OnEnable()
    {
        _player.HealthChanged += OnPlayerHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnPlayerHealthChanged;
    }

    private void OnPlayerHealthChanged(int health)
    {
        while (health > _trackedHealth)
        {
            SpawnLifeObject();
            _trackedHealth++;
        }

        while (health < _trackedHealth)
        {
            DestroyLifeObject();
            _trackedHealth--;
        }
    }

    private void SpawnLifeObject()
    {
        var lifeObject = Instantiate(_healthUnit, transform);
        lifeObject.Image.color = _fadeoutColor;

        Animate(lifeObject.Image, _visibleColor);

        _lives.Push(lifeObject);
    }

    private void DestroyLifeObject()
    {
        if (_lives.Count <= 0)
            return;

        var lifeObject = _lives.Pop();

        Animate(lifeObject.Image, _fadeoutColor);
    }

    private void Animate(Image image, Color color)
    {
        image
            .DOColor(color, _duration)
            .SetEase(_ease)
            .SetUpdate(true)
            .OnComplete(() => { if (color == _fadeoutColor) Destroy(image.gameObject); });
    }
}
