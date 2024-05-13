using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[CreateAssetMenu(fileName = "DeathTracker", menuName = "DeathTracker")]
public class DeathTracker : ScriptableObject
{
    public int deathNumber = 0;
    public TextMeshProUGUI deathCounterText;

    public void UpdateDeathCounter()
    {
        if (deathCounterText != null)
        {
            deathCounterText.text = "Deaths: " + deathNumber.ToString();
        }
    }
}
