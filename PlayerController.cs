using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private int maxEggStorage = 16;
    private int maxRocket1Storage = 64;
    private int maxRocket2Storage = 64;
    private int maxRocket3Storage = 64;
    public float speed = 15.0f;
    private float GroundTimer = 0f;
    private float horizontalInput;
    public bool isGliding = false;
    public bool iswalking = false;
    private Rigidbody rb;
    private Vector3 forceDirection;
    private ConstantForce cForce;

    private AudioSource playerAudio;
    public Animator PoultrymanAnimationController;
    private Animator ElytraAnimationController;
    private Animator ElytraAnimationController2;
    public GameObject LeftWing;
    public GameObject RightWing;
    public float projectileSpeed = 10f;
    public float maxFallSpeed = 50f;
    public float decreaseRate = 20f;
    public float jumpForce;
    public bool isOnGround = true;
    public bool GameOver = false;
    public bool CanGlide = true;
    public int eggStorage = 0;
    public int Rocket1Storage = 0;
    public int Rocket2Storage = 0;
    public int Rocket3Storage = 0;
    public GameObject eggProjectilePrefab;
    public GameObject HudEgg;
    public GameObject HudRocket1;
    public GameObject HudRocket2;
    public GameObject HudRocket3;
    public GameObject ElytraWing1;
    public GameObject ElytraWing2;
    public TextMeshPro HudEggCounter;
    public TextMeshPro HudRocket1Counter;
    public TextMeshPro HudRocket2Counter;
    public TextMeshPro HudRocket3Counter;
    public ParticleSystem Rocket1Particle;
    public ParticleSystem Rocket2Particle;
    public ParticleSystem Rocket3Particle;
    public AudioClip eggCollectSound;
    public AudioClip Elytra;
    public AudioClip MinecraftFirework;
    public AudioClip jump;
    public AudioClip coinCollect;
    public Transform Player;
    public int GoldenEggCount;
    
    SceneLoader sceneLoader;
    public GameObject LevelChanger;
    void Awake()
    {
        sceneLoader = LevelChanger.GetComponent<SceneLoader>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ElytraAnimationController = LeftWing.GetComponent<Animator>();
        ElytraAnimationController2 = RightWing.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cForce = GetComponent<ConstantForce>();
        playerAudio = GetComponent<AudioSource>();         
        Player = GetComponent<Transform>();

        if(eggStorage > 0)
        {
            HudEgg.SetActive(true);
            HudEggCounter.text = eggStorage.ToString();
        }
        if(Rocket1Storage > 0)
        {
            HudRocket1.SetActive(true);
            HudRocket1Counter.text = Rocket1Storage.ToString();
        }

        if (Rocket2Storage > 0)
        {
            HudRocket2.SetActive(true);
            HudRocket2Counter.text = Rocket2Storage.ToString();
        }
        if(Rocket3Storage > 0)
        {
            HudRocket3.SetActive(true);
            HudRocket3Counter.text = Rocket3Storage.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (rb.velocity.y < -maxFallSpeed)
        {
            Vector3 newVelocity = rb.velocity;
            newVelocity.y = -maxFallSpeed;
            rb.velocity = newVelocity;
        }

        if (isOnGround)
        {
            GroundTimer += Time.deltaTime;
            speed -= decreaseRate * Time.deltaTime;
            speed = Mathf.Max(speed, 15.0f);
            Rocket1Particle.Stop();
            Rocket2Particle.Stop();
            Rocket3Particle.Stop();
            isGliding = false;

            if(ElytraWing1 != null && ElytraWing2 != null)
            {
                ElytraAnimationController.SetBool("IsWalking", false);
                ElytraAnimationController2.SetBool("IsWalking", false);
            }
            PoultrymanAnimationController.SetBool("IsWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.Z) && eggStorage > 0)
        {
            GameObject eggProjectile = Instantiate(eggProjectilePrefab, Player.position, Player.rotation);
            
            eggProjectile.transform.parent = transform;
            
            Rigidbody projectileRigidbody = eggProjectile.GetComponent<Rigidbody>();
            projectileRigidbody.velocity = transform.forward * projectileSpeed;

            eggStorage--;
            HudEggCounter.text = eggStorage.ToString();
        } 

        if(CanGlide == true && Rocket1Storage > 0)
        {
            if(Input.GetKeyDown(KeyCode.X))
            {   jumpForce = 60.0f;
                speed = 20.0f;
                rb.drag = 10;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                Rocket1Storage --;
                HudRocket1Counter.text = Rocket1Storage.ToString();
                playerAudio.PlayOneShot(MinecraftFirework, 1.0f);
                Rocket1Particle.Play();
                Rocket2Particle.Stop();
                Rocket3Particle.Stop();
            }
        }

        if(Input.GetKeyUp(KeyCode.X))
        {
            jumpForce = 15.0f;
            isGliding = true;
            if (isOnGround == false)
            {
                playerAudio.PlayOneShot(Elytra, 0.3f);
                if(ElytraWing1 != null && ElytraWing2 != null)
                {
                    ElytraAnimationController.SetBool("IsGliding", true);
                    ElytraAnimationController2.SetBool("IsGliding", true);
                }
                PoultrymanAnimationController.SetBool("IsGliding", true);

            }
        }

        if(CanGlide == true && Rocket2Storage > 0)
        {
            if(Input.GetKeyDown(KeyCode.C))
            {   jumpForce = 80.0f;
                speed = 30.0f;
                rb.drag = 10;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                Rocket2Storage --;
                HudRocket2Counter.text = Rocket2Storage.ToString();
                playerAudio.PlayOneShot(MinecraftFirework, 1.0f);
                Rocket2Particle.Play();
                Rocket1Particle.Stop();
                Rocket3Particle.Stop();
            }
        }

        if(Input.GetKeyUp(KeyCode.C))
        {
            jumpForce = 15.0f;
            isGliding = true;
            if (isOnGround == false)
            {
                playerAudio.PlayOneShot(Elytra, 0.3f);
                if(ElytraWing1 != null && ElytraWing2 != null)
                {
                    ElytraAnimationController.SetBool("IsGliding", true);
                    ElytraAnimationController2.SetBool("IsGliding", true);
                }
                PoultrymanAnimationController.SetBool("IsGliding", true);

            }
        }

        if(CanGlide == true && Rocket3Storage > 0)
        {
            if(Input.GetKeyDown(KeyCode.V))
            {   jumpForce = 90.0f;
                speed = 40.0f;
                rb.drag = 10;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                Rocket3Storage --;
                HudRocket3Counter.text = Rocket3Storage.ToString();
                playerAudio.PlayOneShot(MinecraftFirework, 1.0f);
                Rocket3Particle.Play();
                Rocket1Particle.Stop();
                Rocket2Particle.Stop();
            }
        }

        if(Input.GetKeyUp(KeyCode.V))
        {
            jumpForce = 15.0f;
            isGliding = true;
            if (isOnGround == false)
            {
                playerAudio.PlayOneShot(Elytra, 0.3f);
                if(ElytraWing1 != null && ElytraWing2 != null)
                {
                    ElytraAnimationController.SetBool("IsGliding", true);
                    ElytraAnimationController2.SetBool("IsGliding", true);
                }
                PoultrymanAnimationController.SetBool("IsGliding", true);
                ElytraAnimationController.SetBool("IsGliding", true);
                ElytraAnimationController2.SetBool("IsGliding", true);
            }
        }

        if (eggStorage < 1)
        {
            HudEgg.SetActive(false);
        }

        if (Rocket1Storage < 1)
        {
            HudRocket1.SetActive(false);
        }

        if (Rocket2Storage < 1)
        {
            HudRocket2.SetActive(false);
        }

        if (Rocket3Storage < 1)
        {
            HudRocket3.SetActive(false);
        }
            // Left & right input
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(0f, 0f, horizontalInput); 
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, movement.z * speed); 

        if (horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            iswalking = true;
            if(ElytraWing1 != null && ElytraWing2 != null)
            {
                ElytraAnimationController.SetBool("IsWalking", true);  
                ElytraAnimationController2.SetBool("IsWalking", true);
            }
            PoultrymanAnimationController.SetBool("IsWalking", true);
        }
        else if (horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            if(ElytraWing1 != null && ElytraWing2 != null)
            {
                ElytraAnimationController.SetBool("IsWalking", true);
                ElytraAnimationController2.SetBool("IsWalking", true);
            }
            PoultrymanAnimationController.SetBool("IsWalking", true);
            iswalking = true;
        }

        if (horizontalInput == 0)
        {
            iswalking = false;
        }

        if(iswalking == false)
        {
            if(ElytraWing1 != null && ElytraWing2 != null)
            {
                ElytraAnimationController.SetBool("IsWalking", false);
                ElytraAnimationController2.SetBool("IsWalking", false);
            }
            PoultrymanAnimationController.SetBool("IsWalking", false);

        }

        if (eggStorage > maxEggStorage)
        {
            eggStorage = 16;
        }
        
         if (Rocket1Storage > maxRocket1Storage)
        {
            eggStorage = 64;
        }

        if (Rocket2Storage > maxRocket2Storage)
        {
            eggStorage = 64;
        }

        if (Rocket3Storage > maxRocket3Storage)
        {
            eggStorage = 64;
        }


        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            rb.drag = 0;
            speed = 15.0f;
            playerAudio.PlayOneShot(jump, 1.0f);
        }

        // Gliding
        if (CanGlide && Input.GetKeyUp(KeyCode.Space))
        {
            rb.drag = 10;
            isGliding = true;
            if (isOnGround == false)
            {
                playerAudio.PlayOneShot(Elytra, 0.3f);
                if(ElytraWing1 != null && ElytraWing2 != null)
                {
                    ElytraAnimationController.SetBool("IsGliding", true);
                    ElytraAnimationController2.SetBool("IsGliding", true);
                }
                PoultrymanAnimationController.SetBool("IsGliding", true);
            }
        }

        if (isOnGround == false)
        {
            GroundTimer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGliding)
        {
            isGliding = false;
            if(ElytraWing1 != null && ElytraWing2 != null)
            {
                ElytraAnimationController.SetBool("IsGliding", false);
                ElytraAnimationController2.SetBool("IsGliding", false); 
            }
            PoultrymanAnimationController.SetBool("IsGliding", false);
            rb.drag = 0;
            playerAudio.Stop();
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            rb.drag = 0;
            isOnGround = true;
            isGliding = false;
            if(ElytraWing1 != null && ElytraWing2 != null)
            {
                ElytraAnimationController.SetBool("IsGliding", false);
                ElytraAnimationController2.SetBool("IsGliding", false);
            }
            PoultrymanAnimationController.SetBool("IsGliding", false);
            GroundTimer = 0f;
            playerAudio.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("elytra"))
        {
            CanGlide = true;
            Destroy(other.gameObject);
            ElytraWing1.SetActive(true);
            ElytraWing2.SetActive(true);
        }

         if (other.CompareTag("egg"))
        {
            eggStorage += 16;
            Destroy(other.gameObject);
            HudEgg.SetActive(true);
            HudEggCounter.text = eggStorage.ToString();
            playerAudio.PlayOneShot(eggCollectSound, 1.0f);
            return;
        }

        if(other.CompareTag("GoldenEgg"))
        {
            Destroy(other.gameObject);
            GoldenEggCount++;
            playerAudio.PlayOneShot(coinCollect, 1.0f);
        }

        if (other.CompareTag("Firework1"))
        {
            Destroy(other.gameObject);
            HudRocket1.SetActive(true);
            Rocket1Storage += 3;
            HudRocket1Counter.text = Rocket1Storage.ToString();
            playerAudio.PlayOneShot(eggCollectSound, 1.0f);
        }

        if (other.CompareTag("Firework2"))
        {
            Destroy(other.gameObject);
            HudRocket2.SetActive(true);
            Rocket2Storage += 3;
            HudRocket2Counter.text = Rocket2Storage.ToString();
            playerAudio.PlayOneShot(eggCollectSound, 1.0f);
        }

        if (other.CompareTag("Firework3"))
        {
            Destroy(other.gameObject);
            HudRocket3.SetActive(true);
            Rocket3Storage += 3;
            HudRocket3Counter.text = Rocket3Storage.ToString();
            playerAudio.PlayOneShot(eggCollectSound, 1.0f);
        }
    }
}