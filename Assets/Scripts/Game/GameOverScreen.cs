using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button _playAgainButton;
    [SerializeField] private Button _quitGameButton;
    [SerializeField] private Player _player;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
    }

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;

        _playAgainButton.onClick.AddListener(OnPlayAgainButtonClick);
        _quitGameButton.onClick.AddListener(OnQuitGameButtonClick);
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;

        _playAgainButton.onClick.RemoveListener(OnPlayAgainButtonClick);
        _quitGameButton.onClick.RemoveListener(OnQuitGameButtonClick);
    }

    private void OnPlayerDied()
    {
        Time.timeScale = 0f;
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
    }

    private void OnPlayAgainButtonClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private void OnQuitGameButtonClick()
    {
        Application.Quit();
    }
}
