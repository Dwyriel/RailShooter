using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionController : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 2f;
    [Tooltip("Death Particle Effect")][SerializeField] GameObject DeathFX;
    Scene thisScene;
    // Start is called before the first frame update
    void Start()
    {
        thisScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        SendMessage("PlayerDeath");
        DeathFX.SetActive(true);
        Invoke("ReloadScene", LevelLoadDelay);
    }

    private void ReloadScene() //called by string
    {
        SceneManager.LoadScene(thisScene.buildIndex);
    }
}