using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollector : MonoBehaviour
{
    [SerializeField]
    public GamePercent gamePercent;
    public PlayerHealth playerHealth;

    public void OnTriggerEnter(Collider other)
    {
        if (playerHealth.hasDiedBefore == false)
        {
            if (other.CompareTag("Helmet") || other.CompareTag("Leggings") || other.CompareTag("ChestPlate")
                || other.CompareTag("Boots") || other.CompareTag("Firework3") || other.CompareTag("Firework2")
                || other.CompareTag("Firework1") || other.CompareTag("GoldenEgg") || other.CompareTag("egg")
                || other.CompareTag("bread") || other.CompareTag("elytra"))
            {
                gamePercent.objectscollected++;
                gamePercent.UpdateGamePercentage();
                CalculateCompletion();
            }
        }
    }

    public void CalculateCompletion()
    {
        gamePercent.percentCompletion = (float)gamePercent.objectscollected / gamePercent.totalObjects * 100f;
    }
}
