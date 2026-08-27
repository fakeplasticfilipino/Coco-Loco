using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float keyboardSpeed = 9f;
    public float dragSpeed = 28f;

    Camera cam;
    SpriteRenderer sprite;
    Vector3 startPosition;
    float limitX;

    void Awake()
    {
        cam = Camera.main;
        sprite = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
    }

    void Start()
    {
        UpdateBounds();
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsOver) return;

        UpdateBounds();

        float x = transform.position.x;
        Pointer pointer = Pointer.current;

        if (pointer != null && pointer.press.isPressed && cam != null)
        {
            Vector2 screen = pointer.position.ReadValue();
            Vector3 world = cam.ScreenToWorldPoint(
                new Vector3(screen.x, screen.y, -cam.transform.position.z));
            x = Mathf.MoveTowards(x, world.x, dragSpeed * Time.deltaTime);
        }
        else
        {
            float direction = 0f;
            Keyboard k = Keyboard.current;

            if (k != null)
            {
                if (k.leftArrowKey.isPressed || k.aKey.isPressed) direction -= 1f;
                if (k.rightArrowKey.isPressed || k.dKey.isPressed) direction += 1f;
            }

            x += direction * keyboardSpeed * Time.deltaTime;
        }

        transform.position = new Vector3(
            Mathf.Clamp(x, -limitX, limitX),
            transform.position.y,
            transform.position.z);
    }

    public void ResetToStart()
    {
        transform.position = startPosition;
    }

    void UpdateBounds()
    {
        float halfWidth = sprite != null ? sprite.bounds.extents.x : 0.5f;
        float halfScreen = cam != null ? cam.orthographicSize * cam.aspect : 8.9f;
        limitX = Mathf.Max(0.1f, halfScreen - halfWidth);
    }
}
