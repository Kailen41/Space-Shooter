using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private Text _scoreText;

    [SerializeField] private Sprite[] _livesSprite;

    [SerializeField] private Image _livesImage;

    [SerializeField] private GameObject _gameOverText;
    [SerializeField] private GameObject _restartText;

    private GameManager _gameManager;
    #endregion

    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.SetActive(false);
        _restartText.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is NULL!");
        }
    }

    public void UpdateScore(int _playerScore)
    {
        _scoreText.text = "Score: " + _playerScore.ToString();   
    }

    public void UpdateLives(int _currentLives)
    {
        _livesImage.sprite = _livesSprite[_currentLives];

        if (_currentLives == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.SetActive(true);
        _restartText.SetActive(true);
    }

} // Class Ends
