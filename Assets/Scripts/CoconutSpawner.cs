using UnityEngine;

public class CoconutSpawner : MonoBehaviour
{
    [Header("What To Drop")]
    public GameObject coconutPrefab;

    [Header("Difficulty Curve")]
    public float startInterval = 1.1f;
    public float fastestInterval = 0.3f;
    public float startFallSpeed = 4.5f;
    public float topFallSpeed = 13f;
    public float rampSeconds = 90f;

    [Header("Placement")]
    public float spawnHeight = 6.5f;
    public float edgePadding = 0.7f;

    float timer;
    float halfWidth;

    void Start()
    {
        Camera cam = Camera.main;
        float halfScreen = cam != null ? cam.orthographicSize * cam.aspect : 8.9f;
        halfWidth = Mathf.Max(0.5f, halfScreen - edgePadding);
        ResetSpawner();
    }

    void Update()
    {
        if (coconutPrefab == null) return;
        if (GameManager.Instance != null && GameManager.Instance.IsOver) return;

        timer -= Time.deltaTime;
        if (timer > 0f) return;

        float t = GameManager.Instance != null
            ? Mathf.Clamp01(GameManager.Instance.Elapsed / rampSeconds)
            : 0f;

        timer = Mathf.Lerp(startInterval, fastestInterval, t) * Random.Range(0.85f, 1.15f);

        Vector3 where = new Vector3(Random.Range(-halfWidth, halfWidth), spawnHeight, 0f);
        GameObject dropped = Instantiate(coconutPrefab, where, Quaternion.identity);

        Coconut coconut = dropped.GetComponent<Coconut>();
        if (coconut != null)
        {
            coconut.fallSpeed = Mathf.Lerp(startFallSpeed, topFallSpeed, t) * Random.Range(0.9f, 1.15f);
        }
    }

    public void ResetSpawner()
    {
        timer = 0.8f;
    }
}
