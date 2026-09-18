using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("What To Drop")]
    public GameObject[] powerUpPrefabs;

    [Header("Timing")]
    public float firstDelay = 10f;
    public float minInterval = 8f;
    public float maxInterval = 14f;

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
        if (powerUpPrefabs == null || powerUpPrefabs.Length == 0) return;
        if (GameManager.Instance != null && GameManager.Instance.IsOver) return;

        timer -= Time.deltaTime;
        if (timer > 0f) return;

        timer = Random.Range(minInterval, maxInterval);

        GameObject prefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
        if (prefab == null) return;

        Vector3 where = new Vector3(Random.Range(-halfWidth, halfWidth), spawnHeight, 0f);
        Instantiate(prefab, where, Quaternion.identity);
    }

    public void ResetSpawner()
    {
        timer = firstDelay;
    }
}
