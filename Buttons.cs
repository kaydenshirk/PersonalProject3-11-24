using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    private Button button;
    public int LevelIndex;
    private SceneLoader sceneLoader;
    public GameObject LevelChanger;
    // Start is called before the first frame update
    void Start()
    {
        button  = GetComponent<Button>();
        button.onClick.AddListener(ButtonLevelChanger);
        sceneLoader = LevelChanger.GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonLevelChanger()
    {
        sceneLoader.LevelLoading(LevelIndex);
    }
}
