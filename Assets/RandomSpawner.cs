using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs; // Foto, radio, buku, bendera
    public Transform[] spawnPoints;    // 20 titik spawn

    public int totalObjectsToSpawn = 10; // Berapa objek yang mau kamu spawn (bisa disesuaikan)

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        // Clone list agar tidak double spawn di titik yang sama
        var availablePoints = new System.Collections.Generic.List<Transform>(spawnPoints);

        for (int i = 0; i < totalObjectsToSpawn && availablePoints.Count > 0; i++)
        {
            // Random objek dan spawn point
            int randomObjIndex = Random.Range(0, objectPrefabs.Length);
            int randomSpawnIndex = Random.Range(0, availablePoints.Count);

            // Spawn objek
            Instantiate(objectPrefabs[randomObjIndex], availablePoints[randomSpawnIndex].position, Quaternion.identity);

            // Hapus titik agar tidak dipakai lagi
            availablePoints.RemoveAt(randomSpawnIndex);
        }
    }
}
