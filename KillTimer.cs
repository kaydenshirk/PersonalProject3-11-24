using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class KillTimer : MonoBehaviour
{
    public float timer = 500f;
    public int killTime;
    private SceneLoader sceneLoader;
    public GameObject LevelChanger;
    public TextMeshPro TimerTxt;
    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = LevelChanger.GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        updateTimer(timer);

        if(timer <= killTime)
        {
            Kill();
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

        public void Kill()
    {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            sceneLoader.LevelLoading(sceneIndex);
    }
}
