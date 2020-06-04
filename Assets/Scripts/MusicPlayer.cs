using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class MusicPlayer : MonoBehaviour
{
    Scene thisScene;
    AudioSource aS;
    bool reducingVol = false, translated = false;
    [Range(0, 1)] [SerializeField] float volume = .2f;
    [SerializeField] AudioClip gameMusic;

    private void Awake()
    {
        Object.DontDestroyOnLoad(this);
    }

    void Start()
    {
        aS = GetComponent<AudioSource>();
    }

    void Update() // TODO separate scene loader and music 
    {
        aS.volume = volume;
        thisScene = SceneManager.GetActiveScene();
        if (reducingVol)
        {
            ChangeMainMenuThemeVolume();
        }
        if (thisScene.buildIndex == 1 && !translated)
        {
            translated = true;
            reducingVol = true;
            Invoke("PlayGameMusic", 1.8f);
        }
    }

    private void ChangeMainMenuThemeVolume()
    {
        volume = Mathf.Clamp(volume - (.2f * Time.deltaTime), 0f, 1f);
        aS.volume = volume;
        if (volume < 0f || volume == 0)
            reducingVol = false;
    }

    private void PlayGameMusic()
    {
        volume = .2f;
        aS.clip = gameMusic;
        aS.Play();
    }
}
