using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreDisplay;
    [SerializeField] private ObjectDestroyer _objectDestroyer;
    [SerializeField] private string _scoreFormat = "000000";

    private int _currentScore;

    private void Awake()
    {
        UpdateScore(0);
    }

    private void OnEnable()
    {
        _objectDestroyer.ObjectDestroyed += UpdateScore;
    }

    private void OnDisable()
    {
        _objectDestroyer.ObjectDestroyed -= UpdateScore;
    }

    private void UpdateScore(int score)
    {
        _currentScore += score;
        _scoreDisplay.text = _currentScore.ToString(_scoreFormat);
    }
}
