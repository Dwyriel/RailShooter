using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerInputActions controls;
    [Tooltip("in ms^-1")] [SerializeField] float xSpeed = 6f, ySpeed = 6f;
    [Tooltip("in ms")] [SerializeField] float xPRange = 8f, xNRange = -8f, yPRange = 7f, yNRange = -6f; //(P)ositive and (N)egative movement range for x and y
    float xOffSet, yOffset, curXspeed, curYspeed;
    Vector2 direct;
    void Awake()
    {
        controls = new PlayerInputActions();
        // the first part of the code shows the same path like in the Input Action Window, the scecond code part reads the input value
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ShipMovement();
    }

    private void ShipMovement()
    {
        
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x + xOffSet, xNRange, xPRange), Mathf.Clamp(transform.localPosition.y + yOffset, yNRange, yPRange), transform.localPosition.z);
    }

    void Movement(InputAction.CallbackContext context)//gets input values 
    {
        direct = context.ReadValue<Vector2>();
        Debug.Log("Axis" + direct);
        xOffSet = xSpeed * Time.deltaTime * direct.x;
        yOffset = ySpeed * Time.deltaTime * direct.y;
    }

    private void OnEnable()
    {
        controls.PlayerControls.Horizontal.performed += Movement;
        controls.PlayerControls.Horizontal.Enable();
    }

    private void OnDisable()
    {
        controls.PlayerControls.Horizontal.performed -= Movement;
        controls.PlayerControls.Horizontal.Disable();
    }
}
