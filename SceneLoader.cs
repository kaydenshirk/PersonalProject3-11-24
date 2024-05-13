using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator loadAnimation;
    public int levelToLoad;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            LevelLoading(1);  
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            LevelLoading(sceneIndex);  
        }
    }

    public void LevelLoading(int levelIndex)
    {
        levelToLoad = levelIndex;
        loadAnimation.SetTrigger("fadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
