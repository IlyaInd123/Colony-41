using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] List<Wave> waves;
    [SerializeField] TextMeshProUGUI waveCountdownText;
    List<GameObject> enemies = new();
    int currentWaveIndex = 0;

    void Start()
    {
        StartCoroutine(BeginWaves());
    }

    void Update()
    {
        GetEnemies();
    }

    public List<GameObject> GetEnemies()
    {
        if (enemies.Count > 0)
        {
            enemies.RemoveAll(enemy => enemy == null);
        }
        return enemies;
    }

    IEnumerator BeginWaves()
    {
        while (currentWaveIndex < waves.Count)
        {
            yield return StartNextWave();
        }

        GameManager.Instance.LevelClear();
    }

    IEnumerator StartNextWave()
    {
        Wave currentWave = waves[currentWaveIndex];
        yield return WaveCountdown();

        foreach (Spawner spawner in currentWave.spawners)
        {
            StartCoroutine(SpawnEnemies(spawner));
        }

        yield return new WaitUntil(() => enemies.Count == 0);

        currentWaveIndex++;
    }

    IEnumerator WaveCountdown()
    {
        Wave currentWave = waves[currentWaveIndex];
        float timer = currentWave.preparationTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            waveCountdownText.text = $"Wave Starts in: {Mathf.RoundToInt(timer)}";
            yield return null;
        }
        waveCountdownText.text = "";
    }

    IEnumerator SpawnEnemies(Spawner spawner)
    {
        for (int i = 0; i < spawner.enemyCount; i++)
        {
            GameObject newEnemy = Instantiate(spawner.enemyPrefab, spawner.spawnPoint.position, spawner.spawnPoint.rotation);
            if (newEnemy.TryGetComponent(out Enemy enemy))
            {
                enemy.SetWayPointParent(spawner.waypointParent);
                enemies.Add(newEnemy);
            }
            yield return new WaitForSeconds(spawner.spawnInterval);
        }
    }
}