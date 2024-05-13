using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsScreen : MonoBehaviour
{
    [SerializeField]
    public DeathTracker deathTracker;
    public GamePercent gamePercent;
    public TextMeshProUGUI deathCounterText;
    public TextMeshProUGUI gamePercentText;
    public GameObject secretCode;
    public GameObject levelSelect;
    void Start()
    {
        deathCounterText.text = "Deaths: " + deathTracker.deathNumber.ToString();
        gamePercentText.text = "Collection: " + gamePercent.percentCompletion.ToString() + "%";

        if (deathTracker.deathNumber <=3 && gamePercent.percentCompletion >= 95)
        {
            secretCode.SetActive(true);
        }

        if(deathTracker.deathNumber <=10 && gamePercent.percentCompletion >= 50)
        {
            levelSelect.SetActive(true);
        }


    }
}
