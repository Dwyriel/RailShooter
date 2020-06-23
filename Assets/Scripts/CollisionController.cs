using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionController : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 2f;
    [Tooltip("Death Particle Effect")] [SerializeField] GameObject DeathFX;
    [Tooltip("Afterburner Particle Effect")] [SerializeField] GameObject Afterburner;
    [Tooltip("Hit Sound Effect")] [SerializeField] AudioClip hitSound;
    PlayerHP playerHP;
    Player player;
    AudioSource hitAS;
    bool isDestroyed = false;

    Scene thisScene;
    // Start is called before the first frame update
    void Start()
    {
        thisScene = SceneManager.GetActiveScene();
        playerHP = FindObjectOfType<PlayerHP>();
        player = GetComponent<Player>();
        hitAS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDestroyed)
            TriggerPlayerDeath();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!isDestroyed)
        {
            playerHP.ModifyHP();
            hitAS.PlayOneShot(hitSound);
            if (player.HP <= 0)
            {
                TriggerPlayerDeath();
            }
        }
    }

    private void TriggerPlayerDeath()
    {
        SendMessage("PlayerDeath");
        DeathFX.SetActive(true);
        Afterburner.SetActive(false);
        this.GetComponent<MeshRenderer>().enabled = false;
        isDestroyed = true;
        Invoke("ReloadScene", LevelLoadDelay);
    }
    private void ReloadScene() //called by string
    {
        SceneManager.LoadScene(thisScene.buildIndex);
    }
}