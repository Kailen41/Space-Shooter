using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _powerUpSpeed = 3f;

    [SerializeField] private int _powerUpID; //0 = Trip Shot, 1 = Speed, 2 = Shield

    [SerializeField] private AudioClip _clip;

    void Update()
    {
        PowerupMovementAndBounds();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player _player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if (_player != null)
            {
                switch (_powerUpID)
                {
                    case 0:
                        _player.TripleshotActive();
                        break;
                    case 1:
                        _player.SpeedBoostActive();
                        break;
                    case 2:
                        _player.ShieldsActive();
                        break;
                    default:
                        break;
                }
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
