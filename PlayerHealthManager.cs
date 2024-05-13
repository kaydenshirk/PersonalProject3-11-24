using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 20;
    public int currentHealth;
    private bool canBeHit = true;
    public GameObject DiamondHelmet;
    public GameObject DiamondChestPlate;
    public GameObject LeftSleeve;
    public GameObject RightSleeve;
    public GameObject RightBoot;
    public GameObject LeftBoot;
    public GameObject DiamondLeftLeg;
    public GameObject DiamondRightLeg;
    public GameObject DiamondWaist;
    public GameObject Hud; 
    private float DiamondChestPlatePercentReduction  = 1.0f;
    private float DiamondHelmetPercentReduction  = 1.0f;
    private float DiamondBootsPercentReduction  = 1.0f;
    private float DiamondLeggingsPercentReduction  = 1.0f;
    public bool HasDiamondArmor = false;
    private int maxBreadStorage = 64;
    public int BreadStorage = 0;
    public AudioSource playerAudio;
    public AudioClip Equip;
    public AudioClip Eating;
    public AudioClip HitSound;
    public AudioClip Explosion;
    public AudioClip FireHurt;
    public AudioClip eggCollectSound;
    public GameObject YouDied;
    public GameObject Respawn;
    public GameObject TitleScreen;
    public GameObject HudBread;
    public TextMeshPro HudBreadCounter;
    public TextMeshPro Health;
    public int DeathNumber;
    [SerializeField]
    public DeathTracker deathTracker;
    public bool hasDiedBefore= false;

    SceneLoader sceneLoader;
    public GameObject LevelChanger;
    void Awake()
    {
        sceneLoader = LevelChanger.GetComponent<SceneLoader>();
        //ex.sceneloader.levelIndex = 1;
    }
    void Start()
    {
        currentHealth = maxHealth;
        Health.text = "Hearts: " + currentHealth.ToString();
        playerAudio = GetComponent<AudioSource>();
        GetComponent<PlayerController>().enabled = true;
        if(BreadStorage > 0)
        {
            HudBread.SetActive(true);
            HudBreadCounter.text = BreadStorage.ToString();
        }
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
                    if (hit.collider.CompareTag("Respawn"))
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        YouDied.SetActive(false);
                        currentHealth = 20;
                        Health.text = "Hearts: " + currentHealth.ToString();                    
                    }
                    else if (hit.collider.CompareTag("TitleScreen"))
                    {
                        sceneLoader.LevelLoading(1);
                    }
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && BreadStorage > 0 && currentHealth !=20)
        {
            BreadStorage--;
            HudBreadCounter.text = BreadStorage.ToString();
            currentHealth += 8;
            playerAudio.PlayOneShot(Eating, 1.0f);
            Health.text = "Hearts: " + currentHealth.ToString();
            if(currentHealth > maxHealth)
            {
                currentHealth = 20;
                Health.text = "Hearts: " + currentHealth.ToString();
            }
        }

        if(BreadStorage <= 0)
        {
            HudBread.SetActive(false);
        }
    }

    public void TakeDamage(int FinalDamage)
    {
        currentHealth -= FinalDamage;
        Health.text = "Hearts: " + currentHealth.ToString();
        if (currentHealth <= 0)
        {
            Die();
            Health.text = "Hearts: 0";
            GetComponent<PlayerController>().enabled = false;
            Hud.SetActive(false);
            StartCoroutine(SFXWait());
            hasDiedBefore = true;
        }
    }

    private void Die()
    {
        YouDied.SetActive(true);
        deathTracker.deathNumber ++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == ("Creeper") && canBeHit)
        {
            CalculateDamageAmount(22);
            StartCoroutine(Cooldown());
            if (currentHealth > 0)
            playerAudio.PlayOneShot(HitSound, 1.0f);
            playerAudio.PlayOneShot(Explosion, 1.0f);
        }

        if (collision.gameObject.CompareTag("enemy") && canBeHit)
        {
            CalculateDamageAmount(3);
            StartCoroutine(Cooldown());
            playerAudio.PlayOneShot(HitSound, 1.0f);
        }

        if (collision.gameObject.CompareTag("Enderman") && canBeHit)
        {
            CalculateDamageAmount(7);
            StartCoroutine(Cooldown());
            playerAudio.PlayOneShot(HitSound, 1.0f);
        }

        if (collision.gameObject.CompareTag("PiglinBrute") && canBeHit)
        {
            CalculateDamageAmount(13);
            StartCoroutine(Cooldown());
            playerAudio.PlayOneShot(HitSound, 1.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyProjectile"))
        {
            if(HasDiamondArmor)
            {
            CalculateDamageAmount(3);
            }
            else
            {
                TakeDamage(3);
            }
            
            CalculateDamageAmount(3);
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(FireHurt, 1.0f);
        }

        if(other.gameObject.CompareTag("bread"))
        {
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(eggCollectSound, 1.0f);
            HudBread.SetActive(true);
            BreadStorage += 3;
            HudBreadCounter.text = BreadStorage.ToString();
            if(BreadStorage > maxBreadStorage)
            {
                BreadStorage = 64;
                HudBreadCounter.text = BreadStorage.ToString();
            }
        }

        if (other.gameObject.CompareTag("Helmet"))
        {
            DiamondHelmetPercentReduction  = 0.9f;
            playerAudio.PlayOneShot(Equip, 1.0f);
            DiamondHelmet.SetActive(true);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Leggings"))
        {
            DiamondLeggingsPercentReduction  = 0.7f;
            playerAudio.PlayOneShot(Equip, 1.0f);
            DiamondLeftLeg.SetActive(true);
            DiamondRightLeg.SetActive(true);
            DiamondWaist.SetActive(true);
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("ChestPlate"))
        {
            DiamondChestPlatePercentReduction = 0.7f;
            playerAudio.PlayOneShot(Equip, 1.0f);
            DiamondChestPlate.SetActive(true);
            LeftSleeve.SetActive(true);
            RightSleeve.SetActive(true);
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Boots"))
        {
            DiamondBootsPercentReduction = 0.9f;
            playerAudio.PlayOneShot(Equip, 1.0f);
            RightBoot.SetActive(true);
            LeftBoot.SetActive(true);
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("KillPlane"))
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            sceneLoader.LevelLoading(sceneIndex);  
        }
    }
    private IEnumerator Cooldown()
    {
        canBeHit = false;
        yield return new WaitForSeconds(0.4f);
        canBeHit = true;
    }

    private IEnumerator SFXWait()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<PlayerController>().enabled = false;

        GameObject Zombie = GameObject.Find("Zombie");
        if (Zombie != null)
        {
        GameObject.Find("Zombie").GetComponent<SimpleEnemyAI>().enabled = false;
        }

        GameObject enderman = GameObject.Find("enderman");
        if (enderman != null)
        {
        GameObject.Find("enderman").GetComponent<EndermanAi>().enabled = false;
        }

        GameObject blaze = GameObject.Find("blaze");
        if (blaze != null)
        {
        blaze.GetComponent<ProjectileSpawner>().enabled = false;
        blaze.GetComponent<SimpleEnemyAIBlaze>().enabled = false;
        }

        GameObject creeper = GameObject.Find("Creeper");
        if(creeper != null)
        {
        GameObject.Find("Creeper").GetComponent<SimpleEnemyAICreeper>().enabled = false; 
        }
    }

    private void CalculateDamageAmount(int damage)
    {
        int FinalDamage = (int)(damage * DiamondChestPlatePercentReduction * DiamondLeggingsPercentReduction * DiamondBootsPercentReduction * DiamondHelmetPercentReduction);
        TakeDamage(FinalDamage);
    }
}
