using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MusicPlayer : MonoBehaviour
{
    AudioSource aS;
    bool reducingVol = false, transition = false;
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
        if (reducingVol)
        {
            ChangeMainMenuThemeVolume();
        }
        if (transition)
        {
            transition = false;
            Invoke("PlayGameMusic", 2.5f);
        }
    }

    private void ChangeToInGameMusic() //called by string
    {
        transition = true;
        reducingVol = true;
    }
    private void ChangeMainMenuThemeVolume()
    {
        volume = Mathf.Clamp(volume - (.2f * Time.deltaTime), 0f, 1f);
        aS.volume = volume;
        if (volume <= 0f)
            reducingVol = false;
    }

    private void PlayGameMusic() //called by string
    {
        volume = .2f;
        aS.clip = gameMusic;
        aS.Play();
    }
}
