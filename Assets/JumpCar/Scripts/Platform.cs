using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Platform : MonoBehaviour
{
    public static event Action OnPlayerLanded;

    public Color ActivatedColor;
    public Color DeactivatedColor;

    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;

        _spriteRenderer.color = DeactivatedColor;
    }

    private void Start()
    {
        Invoke(nameof(InitializeBounds), 0.1f);
    }

    private void InitializeBounds()
    {
        CameraBounds.CalculateBounds(Camera.main, out var leftBound, out var rightBound, out _, out _);

        transform.localScale = new Vector3(Mathf.Abs(rightBound - leftBound),
                                            transform.localScale.y,
                                            transform.localScale.z);
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerGrounded += SetColliderSolid;
    }

    static bool firstPlatform = true;
    private void SetColliderSolid(Collider2D collider)
    {
        if(_collider.isTrigger && collider == _collider)
        {
            _collider.isTrigger = false;
            _spriteRenderer.color = ActivatedColor;
            if (firstPlatform)
            {
                firstPlatform = false;
            } else
            {
                OnPlayerLanded?.Invoke();
            }
        }
    }

    public void Reset()
    {
        _collider.isTrigger = true;
        _spriteRenderer.color = DeactivatedColor;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerGrounded -= SetColliderSolid;
        firstPlatform = true;
    }
}
