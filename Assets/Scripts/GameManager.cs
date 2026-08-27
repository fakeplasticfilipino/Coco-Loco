using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Scene References")]
    public PlayerController player;
    public CoconutSpawner spawner;

    [Header("UI")]
    public Text scoreText;
    public Text finalScoreText;
    public Image[] hearts;
    public GameObject gameOverPanel;

    [Header("Rules")]
    public int startingHealth = 3;
    public float pointsPerSecond = 10f;

    int health;
    float score;
    bool isOver;
    float endedAt;

    Transform cam;
    Vector3 camHome;
    Coroutine shakeRoutine;

    public bool IsOver { get { return isOver; } }
    public float Elapsed { get; private set; }

    void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;

        if (Camera.main != null)
        {
            cam = Camera.main.transform;
            camHome = cam.position;
        }
    }

    void Start()
    {
        Begin();
    }

    void Update()
    {
        if (isOver)
        {
            if (Time.time - endedAt > 0.5f && RestartPressed())
            {
                Restart();
            }
            return;
        }

        Elapsed += Time.deltaTime;
        score += pointsPerSecond * Time.deltaTime;
        DrawScore();
    }

    public void TakeDamage()
    {
        if (isOver) return;

        health--;
        DrawHearts();

        if (cam != null)
        {
            if (shakeRoutine != null) StopCoroutine(shakeRoutine);
            shakeRoutine = StartCoroutine(ShakeCamera());
        }

        if (health <= 0) EndRun();
    }

    void Begin()
    {
        health = startingHealth;
        score = 0f;
        Elapsed = 0f;
        isOver = false;

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (player != null) player.ResetToStart();
        if (spawner != null) spawner.ResetSpawner();

        DrawHearts();
        DrawScore();
    }

    void EndRun()
    {
        isOver = true;
        endedAt = Time.time;

        if (finalScoreText != null)
        {
            finalScoreText.text = "SCORE  " + Mathf.FloorToInt(score) +
                                  "\nSURVIVED  " + Elapsed.ToString("0.0") + "s";
        }
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    void Restart()
    {
        Coconut[] leftovers = FindObjectsByType<Coconut>(FindObjectsSortMode.None);
        for (int i = 0; i < leftovers.Length; i++)
        {
            Destroy(leftovers[i].gameObject);
        }
        Begin();
    }

    bool RestartPressed()
    {
        Keyboard k = Keyboard.current;
        if (k != null && (k.spaceKey.wasPressedThisFrame ||
                          k.rKey.wasPressedThisFrame ||
                          k.enterKey.wasPressedThisFrame))
        {
            return true;
        }

        Pointer p = Pointer.current;
        return p != null && p.press.wasPressedThisFrame;
    }

    void DrawScore()
    {
        if (scoreText != null) scoreText.text = "SCORE  " + Mathf.FloorToInt(score);
    }

    void DrawHearts()
    {
        if (hearts == null) return;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] == null) continue;
            hearts[i].color = i < health ? Color.white : new Color(1f, 1f, 1f, 0.15f);
        }
    }

    IEnumerator ShakeCamera()
    {
        const float duration = 0.2f;
        float left = duration;

        while (left > 0f)
        {
            left -= Time.deltaTime;
            Vector2 offset = Random.insideUnitCircle * 0.15f * (left / duration);
            cam.position = camHome + new Vector3(offset.x, offset.y, 0f);
            yield return null;
        }

        cam.position = camHome;
        shakeRoutine = null;
    }
}
