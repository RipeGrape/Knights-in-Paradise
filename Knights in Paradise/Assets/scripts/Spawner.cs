using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public Transform[] spawnLocations;
    public GameObject[] itemSpawn;
    public GameObject[] itemClone;
    public float spawnTime = 5f;
    public float spawnDelay = 3f;
    public float existTime = 2f;
    public float existDelay = 0f;

    private void Start()
    {
        InvokeRepeating("spawnObject", spawnDelay, spawnTime);
    }

    void spawnObject()
    {
        for (var i = 0; i < itemClone.Length; i++)
        {
            Destroy(itemClone[i]);
        }
        for (int i = 0; i < itemSpawn.Length; i++)
        {
            itemClone[i] = Instantiate(itemSpawn[i], spawnLocations[i].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
    }
}
