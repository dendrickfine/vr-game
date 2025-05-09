using System.Collections.Generic;
using UnityEngine;

public class PhotoSpawner : MonoBehaviour
{
    public GameObject photoPrefab; // drag prefab here
    public Transform[] spawnPoints; // assign spawn locations in inspector

    void Start()
    {
        SpawnPhotoRandomly();
    }

    void SpawnPhotoRandomly()
    {
        if (spawnPoints.Length == 0 || photoPrefab == null)
        {
            Debug.LogWarning("SpawnPoints or PhotoPrefab not set!");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(photoPrefab, spawnPoints[randomIndex].position, spawnPoints[randomIndex].rotation);
    }
}
