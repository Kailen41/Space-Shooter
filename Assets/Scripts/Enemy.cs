using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 4f;

    private Player _player;

    private Animator _animator;

    private BoxCollider2D _collider;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("_player is NULL!");
        }

        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.LogError("_animator is NULL!");
        }

        _collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        EnemyMovement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player _player = other.transform.GetComponent<Player>();

            if (_player != null)
            {
                _player.Damage();
            }

            _animator.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            _collider.enabled = false;
            Destroy(this.gameObject, 2.8f);
        }
        else if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            
            if (_player != null)
            {
                _player.AddScore(10);
            }

            _animator.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            _collider.enabled = false;
            Destroy(this.gameObject, 2.8f);
        }
    }

    private void EnemyMovement()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            float _randomXPos = Random.Range(-8.5f, 8.5f);
            transform.position = new Vector3(_randomXPos, 8f, 0);
        }
    }

} // Class Ends
