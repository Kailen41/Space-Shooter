using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _powerUpSpeed = 3f;

    void Update()
    {
        PowerupMovementAndBounds();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player _player = other.transform.GetComponent<Player>();
            if (_player != null)
            {
                _player.TripleshotActive();
            }

            Destroy(this.gameObject);
        }
    }

    private void PowerupMovementAndBounds()
    {
        transform.Translate(Vector3.down * _powerUpSpeed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

} // Class Ends
