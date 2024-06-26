using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    [SerializeField] private float _speed = 3.5f;
    private float _speedMultiplier = 2;
    [SerializeField] private float _fireRate = 0.5f;
    private float _nextFire = -1f;

    [SerializeField] private int _lives = 3;
    [SerializeField] private int _score;

    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldVisualizer;
    [SerializeField] private GameObject _rightEngine, _leftEngine;
 
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _laserSoundClip;

    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldsActive = false;
    #endregion

    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL!");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UIManager is NULL!");
        }

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on the player is NULL!");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }

    void Update()
    {
        PlayerMovement();
        PlayerBoundsOnXAndYAxis();
        FiringLaser();
    }

    #region Custom Functions
    IEnumerator TripleshotPowerDown()
    {
        yield return new WaitForSeconds(5f);
        _isTripleShotActive = false;
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    private void PlayerMovement()
    {
        float _horizontalInput = Input.GetAxis("Horizontal");
        float _verticalInput = Input.GetAxis("Vertical");

        Vector3 _direction = new Vector3(_horizontalInput, _verticalInput);

        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void PlayerBoundsOnXAndYAxis()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.0f, 0), 0);

        if (transform.position.x >= 11.5f)
        {
            transform.position = new Vector3(-11.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.5f)
        {
            transform.position = new Vector3(11.5f, transform.position.y, 0);
        }
    }

    private void FiringLaser()
    {
        Vector3 _laserOffset = new Vector3(0f, 1.05f, 0f);

        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;

            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity);
            }

            _audioSource.Play();
        }
    }

    public void Damage()
    {
        if (_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives -= 1;

        switch (_lives)
        {
            case 2:
                _rightEngine.SetActive(true); 
                break;
            case 1:
                _leftEngine.SetActive(true);
                break;
            default:
                break;
        }
        
        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleshotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleshotPowerDown());
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
    #endregion

} // Class Ends
