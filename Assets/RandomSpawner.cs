using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs; // Foto, radio, buku, bendera
    public Transform[] spawnPoints;    // Titik-titik spawn

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        // Clone list agar tidak ada double spawn di titik yang sama
        var availablePoints = new List<Transform>(spawnPoints);

        // Acak urutan objek
        List<GameObject> shuffledObjects = new List<GameObject>(objectPrefabs);
        ShuffleList(shuffledObjects); // Acak urutan objek agar random

        // Spawn setiap objek unik di titik acak
        int spawnCount = Mathf.Min(shuffledObjects.Count, availablePoints.Count);
        for (int i = 0; i < spawnCount; i++)
        {
            int randomSpawnIndex = Random.Range(0, availablePoints.Count);

            // Spawn objek
            Instantiate(shuffledObjects[i], availablePoints[randomSpawnIndex].position, Quaternion.identity);

            // Hapus titik spawn yang sudah dipakai
            availablePoints.RemoveAt(randomSpawnIndex);
        }
    }

    // Fungsi utilitas untuk mengacak urutan elemen dalam list
    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}
