using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[CreateAssetMenu(fileName = "GamePercent", menuName = "Game%")]
public class GamePercent : ScriptableObject
{
    public int objectscollected;
    public int mobsKilled;
    public int mobsTotal;
    public int totalObjects = 120;
    public float percentCompletion;
    public float defeatCompletion;
    public TextMeshProUGUI CollectPercentText;
    public TextMeshProUGUI EnemyPercentText;

    public void UpdateGamePercentage()
    {
        if (CollectPercentText != null && totalObjects != 0) // Check for null and zero division
        {
            percentCompletion = (float)objectscollected / totalObjects * 100f;
            CollectPercentText.text = "Items Collected: " + percentCompletion.ToString("F2") + "%";
        }

        if(EnemyPercentText != null && mobsTotal != 0)
        {
            defeatCompletion = (float)mobsKilled / mobsTotal * 100f;
            EnemyPercentText.text = "Enemies Defeated: " + defeatCompletion.ToString("F2") + "%";
        }
    }
}
