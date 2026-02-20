using System;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public SpriteRenderer GraySpriteRenderer;

    public CarConfig CarConfig;
    private Camera _camera;

    private float _speed;
    private bool _outOfBounds;
    private float _leftBound;
    private float _rightBound;
    private float _rotationSpeed;
    private float _direction = 1f;

    private void Awake()
    {
        _camera = Camera.main;

        GraySpriteRenderer.color = CarConfig.Color;
        _speed = CarConfig.Speed;
        _outOfBounds = CarConfig.OutOfBounds;
        _rotationSpeed = CarConfig.RotationSpeed;

        var rotationY = _direction < 0f ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);

    }

    private void Start()
    {
        Invoke(nameof(InitializeBounds), 0.1f);
    }

    private void InitializeBounds()
    {
        CameraBounds.CalculateBounds(_camera, out _leftBound, out _rightBound, out _, out _);
    }

    private void Update()
    {
        if (!_outOfBounds)
        {
            SetDirection();
            SetRotation();
        }
        else
        {
            SetBoundPosition();
        }

        Move();
    }

    private void SetBoundPosition()
    {
        if(transform.position.x > _rightBound - GraySpriteRenderer.size.x / 2f)
        {
            transform.position = new Vector2(_leftBound + GraySpriteRenderer.size.x / 2f, transform.position.y);
        }
        if(transform.position.x < _leftBound - GraySpriteRenderer.size.x / 2f)
        {
            transform.position = new Vector2(_rightBound - GraySpriteRenderer.size.x / 2f, transform.position.y);
        }
    }

    private void SetDirection()
    {
        _direction = transform.position.x > _rightBound - GraySpriteRenderer.size.x / 2f ?
                -1f : transform.position.x < _leftBound + GraySpriteRenderer.size.x / 2f ?
                1f : _direction;
    }

    private void SetRotation()
    {
        float rotationY = transform.rotation.eulerAngles.y;
        float targetRotation = _direction < 0f ? 0f : 180f;

        rotationY = Mathf.Lerp(rotationY, targetRotation, Time.deltaTime * _rotationSpeed);

        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }

    public void Move()
    {
        transform.position = new Vector2(transform.position.x + _speed * Time.deltaTime * _direction, transform.position.y);
    }
}
