using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class EyeBlink : MonoBehaviour
{
    [SerializeField] private Sprite _closedEye;
    [SerializeField] private float _closedTime;
    [SerializeField] private float _minTimeBeforeClosing;
    [SerializeField] private float _maxTimeBeforeClosing;

    private SpriteRenderer _renderer;
    private Sprite _originalEye;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();

        _originalEye = _renderer.sprite;
    }

    private void OnEnable()
    {
        StartCoroutine(BlinkWithDelay());
    }

    private IEnumerator BlinkWithDelay()
    {
        float delay = Random.Range(_minTimeBeforeClosing, _maxTimeBeforeClosing);
        float elapsedTime = 0f;

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        float elapsedTime = 0f;

        _renderer.sprite = _closedEye;

        while (elapsedTime < _closedTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _renderer.sprite = _originalEye;

        StartCoroutine(BlinkWithDelay());
    }
}
