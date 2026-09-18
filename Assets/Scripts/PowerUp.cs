using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Kind { Mango, Durian }

    [Header("Type")]
    public Kind kind = Kind.Mango;

    [Header("Motion")]
    public float fallSpeed = 3.5f;
    public float wobbleAngle = 15f;
    public float wobbleSpeed = 4f;
    public float killHeight = -7.5f;

    Rigidbody2D body;
    float wobbleOffset;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        wobbleOffset = Random.value * Mathf.PI * 2f;
    }

    void Start()
    {
        if (body != null) body.linearVelocity = Vector2.down * fallSpeed;
    }

    void Update()
    {
        float angle = Mathf.Sin(Time.time * wobbleSpeed + wobbleOffset) * wobbleAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (transform.position.y < killHeight) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.Instance != null) GameManager.Instance.CollectPowerUp(kind);
        Destroy(gameObject);
    }
}
