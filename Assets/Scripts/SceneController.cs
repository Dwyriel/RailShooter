using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    Scene thisScene;

    void Start()
    {
        thisScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (Keyboard.current.anyKey.isPressed)
        {
            InvokeScene();
        }
    }
    private void InvokeScene()
    {
        SceneManager.LoadScene(thisScene.buildIndex + 1);
    }

}
