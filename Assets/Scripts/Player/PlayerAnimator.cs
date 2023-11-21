using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private InputSource _inputSource;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("isMoving", _inputSource.HasMoveInput);
    }

    public void TakeDamage()
    {
        _animator.SetTrigger("damaged");
    }
}
