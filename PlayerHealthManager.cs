using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;
    private bool canBeHit = true;
    public AudioSource HitSound;
    public GameObject YouDied;
    public GameObject Respawn;
    public GameObject TitleScreen;

    void Awake()
    {
        currentHealth = maxHealth;
        HitSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

           if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider != null)
                {
                    Debug.Log("Hit object: " + hit.collider.gameObject.name);
                    if (hit.collider.CompareTag("Respawn"))
                    {
                        YouDied.SetActive(false);
                        Debug.Log("Respawn");
                        currentHealth = 20;
                    }
                    else if (hit.collider.CompareTag("TitleScreen"))
                    {
                    Debug.Log("TitleScreen");
                    }
                }
            }

        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);
    }

    private void Die()
    {
        YouDied.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == ("creeper") && canBeHit)
        {
            TakeDamage(18);
            StartCoroutine(Cooldown());
            HitSound.Play();
        }
        else if (collision.gameObject.CompareTag("enemy") && canBeHit)
        {
            TakeDamage(5);
            StartCoroutine(Cooldown());
            HitSound.Play();
        }
    }

    private IEnumerator Cooldown()
    {
        canBeHit = false;
        yield return new WaitForSeconds(1.0f);
        canBeHit = true;
    }
}
