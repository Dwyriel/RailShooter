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
    bool isCLicked = false, inGame = false, inMainMenu = true, inGameMusic = false;
    [Range(0, 1)] [SerializeField] float volume = .2f;
    [SerializeField] AudioClip gameMusic;
    private void Awake()
    {
        Object.DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
        thisScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update() // TODO separate scene loader and music 
    {
        aS.volume = volume;
        if ((Keyboard.current.anyKey.isPressed) && !isCLicked)
        {
            isCLicked = true;
            Invoke("InvokeScene", 2f);
        }
        if (isCLicked && inMainMenu)
            ChangeMainMenuThemeVolume();
        if (inGame && !inGameMusic)
        {
            inGameMusic = true;
            Invoke("PlayGameMusic", 1f);
        }
    }

    private void ChangeMainMenuThemeVolume()
    {
        volume = Mathf.Clamp(volume - (.1f * Time.deltaTime), 0f, 1f);
        aS.volume = volume;
    }

    private void InvokeScene()
    {
        inMainMenu = false;
        inGame = true; 
        SceneManager.LoadScene(thisScene.buildIndex + 1);
    }

    private void PlayGameMusic()
    {
        volume = .2f;
        aS.clip = gameMusic;
        aS.Play();
    }
}
