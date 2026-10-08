
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 8f;
    public float speedMultiplier = 1f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    [Header("Directional Sprites")]
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    public Rigidbody2D rb { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startingPosition = transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        speedMultiplier = 1f;
        direction = initialDirection;
        nextDirection = Vector2.zero;

        transform.position = startingPosition;
        rb.bodyType = RigidbodyType2D.Dynamic;

        enabled = true;
        UpdateSprite();
    }

    private void Update()
    {
        if (nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;

        Vector2 translation =
            speed * speedMultiplier *
            Time.fixedDeltaTime * direction;

        rb.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 newDirection, bool forced = false)
    {
        if (forced || !Occupied(newDirection))
        {
            direction = newDirection;
            nextDirection = Vector2.zero;

            UpdateSprite();
        }
        else
        {
            nextDirection = newDirection;
        }
    }

    private void UpdateSprite()
    {
        if (direction == Vector2.up && upSprite != null)
            spriteRenderer.sprite = upSprite;

        else if (direction == Vector2.down && downSprite != null)
            spriteRenderer.sprite = downSprite;

        else if (direction == Vector2.left && leftSprite != null)
            spriteRenderer.sprite = leftSprite;

        else if (direction == Vector2.right && rightSprite != null)
            spriteRenderer.sprite = rightSprite;
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position,
            Vector2.one * 0.3f,
            0f,
            direction,
            0.5f,
            obstacleLayer
        );

        return hit.collider != null;
    }
}
