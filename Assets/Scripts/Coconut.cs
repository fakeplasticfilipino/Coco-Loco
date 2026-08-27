using UnityEngine;

public class Coconut : MonoBehaviour
{
    [Header("Motion")]
    public float fallSpeed = 5f;
    public float spinSpeed = 120f;
    public float killHeight = -7.5f;

    Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spinSpeed *= Random.value < 0.5f ? -1f : 1f;
    }

    void Start()
    {
        if (body != null) body.linearVelocity = Vector2.down * fallSpeed;
    }

    void Update()
    {
        transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);

        if (transform.position.y < killHeight) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.Instance != null) GameManager.Instance.TakeDamage();
        Destroy(gameObject);
    }
}
