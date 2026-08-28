using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    const string HighScoreKey = "FallingBuko.HighScore";

    [Header("Scene References")]
    public PlayerController player;
    public CoconutSpawner spawner;

    [Header("UI")]
    public Text scoreText;
    public Text multiplierText;
    public Text highScoreText;
    public Text finalScoreText;
    public Image[] hearts;
    public GameObject gameOverPanel;

    [Header("Rules")]
    public int startingHealth = 3;
    public float pointsPerSecond = 10f;
    public float multiplierEvery = 30f;

    int health;
    int highScore;
    int shownMultiplier;
    float score;
    bool isOver;
    float endedAt;

    Transform cam;
    Vector3 camHome;
    Coroutine shakeRoutine;
    Coroutine popRoutine;

    public bool IsOver { get { return isOver; } }
    public float Elapsed { get; private set; }

    public int Multiplier
    {
        get { return 1 + Mathf.FloorToInt(Elapsed / Mathf.Max(1f, multiplierEvery)); }
    }

    void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;

        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);

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
        score += pointsPerSecond * Multiplier * Time.deltaTime;

        if (Multiplier != shownMultiplier) RaiseMultiplier();

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
        shownMultiplier = 0;

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (player != null) player.ResetToStart();
        if (spawner != null) spawner.ResetSpawner();

        RaiseMultiplier();
        DrawHearts();
        DrawScore();
        DrawHighScore();
    }

    void EndRun()
    {
        isOver = true;
        endedAt = Time.time;

        int final = Mathf.FloorToInt(score);
        bool beaten = final > highScore;

        if (beaten)
        {
            highScore = final;
            PlayerPrefs.SetInt(HighScoreKey, highScore);
            PlayerPrefs.Save();
        }

        DrawHighScore();

        if (finalScoreText != null)
        {
            finalScoreText.text = "SCORE  " + final +
                                  "\nSURVIVED  " + Elapsed.ToString("0.0") + "s\n" +
                                  (beaten ? "NEW BEST" : "BEST  " + highScore);
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

    void RaiseMultiplier()
    {
        bool climbed = Multiplier > shownMultiplier && shownMultiplier > 0;
        shownMultiplier = Multiplier;

        if (multiplierText == null) return;

        multiplierText.text = "x" + shownMultiplier;
        multiplierText.enabled = shownMultiplier > 1;
        multiplierText.transform.localScale = Vector3.one;

        if (!climbed || !multiplierText.enabled) return;

        if (popRoutine != null) StopCoroutine(popRoutine);
        popRoutine = StartCoroutine(PopMultiplier());
    }

    void DrawScore()
    {
        if (scoreText != null) scoreText.text = "SCORE  " + Mathf.FloorToInt(score);
    }

    void DrawHighScore()
    {
        if (highScoreText != null) highScoreText.text = "BEST  " + highScore;
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

    IEnumerator PopMultiplier()
    {
        const float duration = 0.35f;
        float left = duration;
        Transform t = multiplierText.transform;

        while (left > 0f)
        {
            left -= Time.deltaTime;
            float grow = 1f + 0.6f * (left / duration);
            t.localScale = new Vector3(grow, grow, 1f);
            yield return null;
        }

        t.localScale = Vector3.one;
        popRoutine = null;
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
