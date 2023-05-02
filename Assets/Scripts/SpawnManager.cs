using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    private bool _isSpawnning = false;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        while (_isSpawnning ==  false) 
        {
            Vector3 _posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 8, 0);
            GameObject _newEnemy = Instantiate(_enemyPrefab, _posToSpawn, Quaternion.identity);
            _newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2f);
        }
    }

    public void OnPlayerDeath()
    {
        _isSpawnning = true;
    }

} // Class Ends
