using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _fireRate = 0.5f;
    private float _nextFire = -1f;

    [SerializeField] private int _lives = 3;

    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;

    private SpawnManager _spawnManager;

    [SerializeField] private bool _isTripleShotActive = false;
    #endregion

    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL!");
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
        }
    }

    public void Damage()
    {
        _lives -= 1;

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
    #endregion

} // Class Ends
