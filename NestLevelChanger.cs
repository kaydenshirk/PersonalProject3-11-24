using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NestLevelChanger : MonoBehaviour
{
    public int LevelIndex;
    private SceneLoader sceneLoader;
    private PlayerController playerController;
    public GameObject LevelChanger;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = LevelChanger.GetComponent<SceneLoader>();
        playerController = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && playerController.GoldenEggCount >= 5)
            {
                sceneLoader.LevelLoading(LevelIndex);
                Destroy(gameObject);
            }
        }
}
