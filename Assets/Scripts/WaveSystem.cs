using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] List<Wave> waves;
    [SerializeField] TextMeshProUGUI waveCountdownText;
    int currentWaveIndex = 0;

    void Start()
    {
        StartCoroutine(BeginWaves());
    }

    IEnumerator BeginWaves()
    {
        while (currentWaveIndex < waves.Count)
        {
            yield return StartNextWave();
        }
    }

    IEnumerator StartNextWave()
    {
        Wave currentWave = waves[currentWaveIndex];
        yield return WaveCountdown();

        foreach (Spawner spawner in currentWave.spawners)
        {
            StartCoroutine(SpawnEnemies(spawner));
        }

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
            }
            yield return new WaitForSeconds(spawner.spawnInterval);
        }
    }
}