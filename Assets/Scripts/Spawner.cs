using UnityEngine;

[System.Serializable]
public class Spawner
{
    public int enemyCount;
    public Transform spawnPoint;
    public GameObject enemyPrefab;
    public Transform waypointParent;
    public float spawnInterval;
}