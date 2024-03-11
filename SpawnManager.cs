using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private Vector3 spawnPos = new Vector3(0, -2, -6);
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(playerPrefab, spawnPos, playerPrefab.transform.rotation);    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
