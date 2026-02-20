using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static event Action OnPlayerJumped;
    public static event Action OnPlayerDied;
    public static event Action<Collider2D> OnPlayerGrounded;

    public PlayerConfig PlayerConfig;

    [Header("Ground Check")]
    public LayerMask GroundMask;
    public float CheckGroundDistance = 1f;
    public float CheckGroundRadius = 1f;

    [Header("Enemy Check")]
    public LayerMask EnemyMask;
    public Vector3 CheckEnemyOffset;
    public float CheckEnemyRadius;

    private Rigidbody2D _rigidbody2D;
    private Input _input;
    private Camera _camera;

    private float _jumpForce;

    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _camera = Camera.main;

        _input = new();
        _input.Enable();

        _jumpForce = PlayerConfig.JumpForce;
    }

    private void OnEnable()
    {
        _input.Player.Press.performed += JumpPerformed;
        _input.Player.Press.canceled += JumpCanceled;
    }

    private void FixedUpdate()
    {
        CheckGround();

        var targetCameraYPosition = Mathf.Lerp(_camera.transform.position.y,
                                                transform.position.y + 1f,
                                                Time.fixedDeltaTime * 10f);

        _camera.transform.position = new Vector3(_camera.transform.position.x,
                                                targetCameraYPosition,
                                                _camera.transform.position.z);

        CheckEnemy();
    }

    private void CheckEnemy()
    {
        var enemyCollider = Physics2D.OverlapCircle(transform.position + CheckEnemyOffset,
                                                        CheckEnemyRadius,
                                                        EnemyMask);

        if (enemyCollider)
        {
            OnPlayerDied?.Invoke();
            Destroy(gameObject);
        }
    }

    private void CheckGround()
    {
        var platformCollider = Physics2D.OverlapCircle(transform.position +
            Vector3.down * CheckGroundDistance,
            CheckGroundRadius,
            GroundMask);
        _isGrounded = platformCollider ? true : false;

        OnPlayerGrounded?.Invoke(platformCollider);
    }

    private void JumpPerformed(InputAction.CallbackContext callback)
    {
        if (_isGrounded)
        {
            _rigidbody2D.AddForceY(_jumpForce);
            OnPlayerJumped?.Invoke();
        }
    }

    private void JumpCanceled(InputAction.CallbackContext callback)
    {
        if (!_isGrounded)
        {
            if(_rigidbody2D.linearVelocityY > 0f)
            {
                _rigidbody2D.linearVelocityY = 0f;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + Vector3.down * CheckGroundDistance, CheckGroundRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + CheckEnemyOffset, CheckEnemyRadius);
    }

    private void OnDisable()
    {
        _input.Player.Press.performed -= JumpPerformed;
        _input.Player.Press.canceled -= JumpCanceled;
        _input.Dispose();
    }
}
