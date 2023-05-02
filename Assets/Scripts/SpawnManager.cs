using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _tripleshotPowerupPrefab;

    private bool _isEnemySpawnning = false;
    private bool _isPowerupSpawning = false;
    #endregion

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    #region Enumerators
    IEnumerator SpawnEnemyRoutine()
    {
        while (_isEnemySpawnning ==  false) 
        {
            Vector3 _posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 8, 0);
            GameObject _newEnemy = Instantiate(_enemyPrefab, _posToSpawn, Quaternion.identity);
            _newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_isPowerupSpawning == false)
        {
            Vector3 _posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 8, 0);
            Instantiate(_tripleshotPowerupPrefab, _posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(10f, 30f));
        }
    }
    #endregion

    public void OnPlayerDeath()
    {
        _isEnemySpawnning = true;
        _isPowerupSpawning = true;
    }

} // Class Ends
