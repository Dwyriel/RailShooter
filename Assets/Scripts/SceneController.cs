using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject musicPlayer;
    Scene thisScene;
    bool isClicked = false;

    void Start()
    {
        thisScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (Keyboard.current.anyKey.isPressed && !isClicked)
        {
            isClicked = true;
            musicPlayer.SendMessage("ChangeToInGameMusic");
            Invoke("InvokeScene", 2f);
        }
    }
    private void InvokeScene()
    {
        SceneManager.LoadScene(thisScene.buildIndex + 1);
    }

}
