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
    bool isCLicked = false;
    [Range (0,1)][SerializeField] float volume = .15f;

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
    void Update()
    {
        aS.volume = volume;
        if ((Keyboard.current.anyKey.isPressed || Mouse.current.leftButton.isPressed) && !isCLicked)
            LoadGame();
    }

    private void LoadGame()
    {
        isCLicked = true;
        SceneManager.LoadScene(thisScene.buildIndex + 1);
    }
}
