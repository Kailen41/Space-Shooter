using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;

    void Start()
    {
        transform.position = Vector3.zero;
    }

    void Update()
    {
        PlayerMovement();

        PlayerBoundsOnXAndYAxis();
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

} // End Class
