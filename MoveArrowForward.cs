using UnityEngine;

public class MoveArrowForward : MonoBehaviour
{
    public float speed = 5f;
    public ParticleSystem smokeParticle;

    void Start()
    {
        PlayEffect();
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void PlayEffect()
    {
        smokeParticle.Play();
    }
}