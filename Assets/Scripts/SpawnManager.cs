using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _powerups;

    private bool _isEnemySpawnning = false;
    private bool _isPowerupSpawning = false;
    #endregion

    #region Custom Functions
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

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
        yield return new WaitForSeconds(3f);

        while (_isPowerupSpawning == false)
        {
            Vector3 _posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 8, 0);
            int _randomPowerup = Random.Range(0, 3);
            Instantiate(_powerups[_randomPowerup], _posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5f, 15f));
        }
    }

    public void OnPlayerDeath()
    {
        _isEnemySpawnning = true;
        _isPowerupSpawning = true;
        Destroy(this.gameObject);
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    #endregion

} // Class Ends
