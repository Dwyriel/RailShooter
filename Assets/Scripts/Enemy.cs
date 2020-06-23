using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("HP and Score")]
    [SerializeField] int hitsToDie = 2;
    [SerializeField] int scorePerKill = 20;
    [SerializeField] GameObject explosionFX;
    [SerializeField] Transform parent;
    bool isDestroyed = false;

    private void OnParticleCollision(GameObject other)
    {
        hitsToDie--;
        if (!isDestroyed && hitsToDie <= 0)
        {
            TriggerDeath();
        }
    }

    private void TriggerDeath()
    {
        isDestroyed = true;
        Destroy(gameObject);
        GameObject deathFX = Instantiate(explosionFX, transform.position, Quaternion.identity);
        deathFX.transform.parent = parent;
        ScoreBoard sb = FindObjectOfType<ScoreBoard>();
        Announcer mp = FindObjectOfType<Announcer>();
        mp.AnnouncePwnage();
        sb.ScoreKill(scorePerKill);
    }

    [Header("Movement && Shooting")]
    [SerializeField] float minSpeed = 16f;
    [SerializeField] float maxSpeed = 12f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float angleToShoot = 40f;
    [SerializeField] ParticleSystem[] bulletsParticle;
    Player player;
    float speed;
    private void Start()
    {
        speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        transform.Translate(0, 0, Time.deltaTime * speed);

        float angle = Vector3.Angle(direction, this.transform.forward);
        if (angle < angleToShoot)
        {
            ActivateShooting(true);
        }else
        {
            ActivateShooting(false);
        }
    }

    private void ActivateShooting(bool activate)
    {
        if (activate)
        {
            foreach (ParticleSystem bPar in bulletsParticle)
            {
                if (!bPar.isPlaying)
                {
                    bPar.Play();
                }
            }
        }
        else
        {
            foreach (ParticleSystem bPar in bulletsParticle)
            {
                if (bPar.isPlaying)
                {
                    bPar.Stop();
                }
            }
        }
    }
}
