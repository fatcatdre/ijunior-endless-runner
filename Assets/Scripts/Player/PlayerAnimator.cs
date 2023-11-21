using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private InputSource _inputSource;

    private Animator _animator;

    private int _isMoving = Animator.StringToHash("isMoving");
    private int _damaged = Animator.StringToHash("damaged");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(_isMoving, _inputSource.HasMoveInput);
    }

    public void TakeDamage()
    {
        _animator.SetTrigger(_damaged);
    }
}
