using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject musicPlayer;
    PlayerInputActions controls;
    Scene thisScene;
    bool isClicked = false;

    void Awake()
    {
        thisScene = SceneManager.GetActiveScene();
        controls = new PlayerInputActions();
    }

    private void InvokeScene()
    {
        SceneManager.LoadScene(thisScene.buildIndex + 1);
    }
    private void OnEnable()
    {
        controls.PlayerControls.StartGame.performed += TriggerNextScene;
        controls.PlayerControls.StartGame.Enable();
    }

    private void OnDisable()
    {
        controls.PlayerControls.StartGame.performed -= TriggerNextScene;
        controls.PlayerControls.StartGame.Disable();
    }

    void TriggerNextScene(InputAction.CallbackContext context)
    {
        if (!isClicked)
        {
            isClicked = true;
            musicPlayer.SendMessage("ChangeToInGameMusic");
            Invoke("InvokeScene", 2f);
        }
    }
}