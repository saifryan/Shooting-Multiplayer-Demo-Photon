using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
    public static SpawnManagerScript Instance;
    [SerializeField] SpawnPointScript[] spawnPointScript;

    private void Awake()
    {
        Instance = this;
    }

    public Transform GetSpawnPoint()
    {
        return spawnPointScript[Random.Range(0, spawnPointScript.Length)].transform;
    }
}
