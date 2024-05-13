using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenLevelActivator : MonoBehaviour
{
    public GameObject finalLevel;
    public GameObject LevelSelect;
    public AudioClip Success;
    private AudioSource audiosource;
    private KeyCode[] FinalLevelSequence = { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.B, KeyCode.A, KeyCode.Return};
    private KeyCode[] LevelSelectSequence = {KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow};
    private int FinalIndex = 0;
    private int LevelSelectIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(FinalLevelSequence[FinalIndex]))
            {
                FinalIndex++;
                if (FinalIndex >= FinalLevelSequence.Length)
                {
                    UnlockFinalLevel();
                    FinalIndex = 0;
                }
            }
            else
            {
                FinalIndex = 0;
            }
        }

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(LevelSelectSequence[LevelSelectIndex]))
            {
                LevelSelectIndex++;
                if (LevelSelectIndex >= LevelSelectSequence.Length)
                {
                    UnlockLevelSelect();
                    LevelSelectIndex = 0;
                }
            }
            else
            {
                LevelSelectIndex = 0;
            }
        }
    }

    void UnlockFinalLevel()
    {
        finalLevel.SetActive(true);
        audiosource.PlayOneShot(Success, 1.0f);
    }

    void UnlockLevelSelect()
    {
        LevelSelect.SetActive(true);
        audiosource.PlayOneShot(Success, 1.0f);
    }
}
